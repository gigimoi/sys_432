using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(HealthScript))]
public class BarricadeScript : Photon.MonoBehaviour {
	Transform text;
	void Start () {
		text = GameObject.Find ("/UI/Canvas/BottomPanel/BottomText").transform;
		text.parent.GetComponent<Image> ().enabled = false;
	}
	Transform triggeringPlayer;
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.GetComponent<PlayerControllerScript> () != null) {
			if(collider.gameObject.GetComponent<PhotonView>().isMine) {
				triggeringPlayer = collider.gameObject.transform;
			}
		}

	}
	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.GetComponent<PlayerControllerScript> () != null) {
			if(collider.gameObject.GetComponent<PhotonView>().isMine) {
				text.GetComponent<Text>().text = "";
				text.parent.GetComponent<Image> ().enabled = false;
				triggeringPlayer = null;
			}
		}
	}
	int repairticker = 0;
	void Update () {
		GetComponent<HealthScript> ().OnDeath = OnBroken;
		if (triggeringPlayer != null) {
			if (GetComponent<HealthScript>().GetHealth() < GetComponent<HealthScript>().MaxHealth) {
				text.GetComponent<Text>().text = "Hold F to Repair";
				text.parent.GetComponent<Image> ().enabled = true;
				triggeringPlayer = triggeringPlayer.gameObject.transform;
			} else {
				text.GetComponent<Text>().text = "";
				text.parent.GetComponent<Image> ().enabled = false;
				triggeringPlayer = null;
			}
			if (repairticker == 0 && Input.GetKey (KeyCode.F)) {
				photonView.RPC ("Heal", PhotonTargets.All, 1);
				repairticker = 8;
			}
		}
		if (repairticker > 0) {
			repairticker--;
		}
		if (photonView.isMine) {
			if (GetComponent<HealthScript> ().GetHealth () >= 10) {
				photonView.RPC ("Resolidify", PhotonTargets.All);
			}
		}
	}
	[RPC] void Resolidify() {
		this.gameObject.layer = LayerMask.NameToLayer("ZombiePathThrough");
	}
	void FixedUpdate() {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Zombie"), LayerMask.NameToLayer ("ZombieMoveThrough"));
	}
	void OnBroken() {
		gameObject.layer = 10;//LayerMask.GetMask ("ZombieMoveThrough");
	}
}
