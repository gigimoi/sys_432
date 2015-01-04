using UnityEngine;
using System.Collections;

public class BulletScript : Photon.MonoBehaviour {
	int lifetime = 0;
	public string Team;
	private string username;
	void Start () {
		rigidbody2D.AddRelativeForce (new Vector2 (0, 650));
	}
	void Update () {
		if (lifetime > 200 && photonView.isMine) {
			PhotonNetwork.Destroy(gameObject);
		}
		lifetime++;
	}
	void FixedUpdate() {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("ZombiePathThrough"));
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("ZombieMoveThrough"));
	}
	void OnCollisionEnter2D(Collision2D collision) {
		if (photonView.isMine) {
			if (collision.gameObject.name == "Collision") {
				PhotonNetwork.Destroy(gameObject);
			}
			if (collision.gameObject.GetComponent<HealthScript> () != null && collision.gameObject.GetComponent<TeamScript>() != null) {
				var team = collision.gameObject.GetComponent<TeamScript>();
				if(team.Team != this.Team) {
					collision.gameObject.GetComponent<PhotonView>().RPC ("TakeDamage", PhotonTargets.All, 1);
					PhotonNetwork.Destroy(gameObject);
					var players = GameObject.Find("/Game").transform.GetChild(2).GetComponentsInChildren<PlayerControllerScript>();
					for(int i = 0; i < players.Length; i++) {
						if(players[i].Username == username) {
							players[i].photonView.RPC ("AddScore", PhotonTargets.AllBufferedViaServer, 10 + (collision.gameObject.GetComponent<HealthScript>().GetHealth() == 0 ? 50 : 0));
							break;
						}
					}
				}
			}
		}
	}
	[RPC] void SetPlayer(string username) {
		this.username = username;
	}
}
