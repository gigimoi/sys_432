using UnityEngine;
using System.Collections;
using Pathfinding;

public class ZombieAI : Photon.MonoBehaviour {
	private Seeker seeker;
	public Transform Game;

	public Path path;
	public float speed = 100;
	int repathTicker = 0;
	public float nextWaypointDistance = 3;
	private int currentWaypoint = 0;
	Transform playerTarget;
	
	public void Start () {
		if (photonView.isMine) {
			seeker = GetComponent<Seeker>();
			repathTicker = 50;
			var players = Game.GetComponentsInChildren<PlayerControllerScript>();
			playerTarget = players [Random.Range(0, players.Length)].transform;
			repath();
		}
	}
	private bool noRot = false;
	private GameObject collidingWith;
	private int hitTimer = 0;
	public void Update() {
		GetComponent<HealthScript> ().OnTakeDamage = TakeDamage;
		if (!noRot) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Rad2Deg * Mathf.Atan2 (rigidbody2D.velocity.y, rigidbody2D.velocity.x)));
		}
		if (hitTimer == 0 && collidingWith != null && collidingWith.GetComponent<HealthScript> () != null &&
			collidingWith.GetComponent<TeamScript> ().Team != "Zombies") {
			hitTimer = 70;
			if(photonView.isMine) {
				collidingWith.GetComponent<PhotonView> ().RPC ("TakeDamage", PhotonTargets.All, 10);
			}
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Rad2Deg * Mathf.Atan2 (rigidbody2D.velocity.y, rigidbody2D.velocity.x)));
		} else {
			if (hitTimer > 0) {
				hitTimer--;
			}
		}
	}
	public void OnCollisionEnter2D(Collision2D collision) {
		noRot = true;
		collidingWith = collision.gameObject;
	}
	public void OnCollisionExit2D(Collision2D collision) {
		noRot = false;
		collidingWith = null;
	}
	public void OnPathComplete (Path p) {
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}
	}
	
	public void FixedUpdate () {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Zombie"), LayerMask.NameToLayer ("ZombieMoveThrough"));
		repathTicker--;
		if (repathTicker == 0) {
			repathTicker = 20;
			repath ();
		}
		if (path == null) {
			//We have no path to move after yet
			return;
		}
		
		if (currentWaypoint >= path.vectorPath.Count) {
			return;
		}
		
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		rigidbody2D.AddForce (dir * 150 * GetComponent<BuffListScript>().GetMoveSpeedMultiplier());
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
	void repath() {
		seeker.StartPath(transform.position, playerTarget.position, OnPathComplete);
	}

	void TakeDamage (int amnt) {
		for (int i = 0; i < amnt; i++) {
			PhotonNetwork.Instantiate("Gib", transform.position, transform.rotation, 0);
		}
	}
}