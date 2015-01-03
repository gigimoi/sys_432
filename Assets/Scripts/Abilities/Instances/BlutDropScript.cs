using UnityEngine;
using System.Collections;

public class BlutDropScript : Photon.MonoBehaviour {
	Transform players;
	void Start () {
		if (photonView.isMine) {
			Invoke ("DestroySelf", 10);
		}
		players = GameObject.Find ("/Game/Players").transform;
	}
	void Update () {
		if (photonView.isMine) {
			var closestRange = float.MaxValue;
			Transform target = null;
			for (int i = 0; i < players.childCount; i++) {
				var player = players.GetChild(i);
				var dist = Vector3.Distance(player.position, transform.position);
				if(dist < closestRange) {
					closestRange = dist;
					target = player;
				}
			}
			var step = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (target.position.x, target.position.y), 0.05f);
			var newPos = new Vector3 (step.x, step.y, transform.position.z);
			var velocity = transform.position - newPos;
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Rad2Deg * Mathf.Atan2 (velocity.y, velocity.x)));
			transform.position = newPos;
		}
	}
	void DestroySelf() {
		PhotonNetwork.Destroy (gameObject);
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (photonView.isMine) {
			var team = collider.GetComponent<TeamScript>();
			if(team != null && team.Team == "Players") {
				collider.gameObject.GetPhotonView().RPC ("Heal", PhotonTargets.AllBufferedViaServer, 5);
				PhotonNetwork.Destroy(gameObject);
			}
		}
	}
}
