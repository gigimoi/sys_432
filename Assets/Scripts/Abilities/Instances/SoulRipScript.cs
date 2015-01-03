using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoulRipScript : Photon.MonoBehaviour {
	Transform player;
	List<Transform> blacklist = new List<Transform>();
	void Start () {
		Invoke ("DestroySelf", 0.2f);
	}
	void DestroySelf() {
		if (photonView.isMine) {
			PhotonNetwork.Destroy (gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if(photonView.isMine) {
			if (!blacklist.Contains(collider.transform) && collider.GetComponent<TeamScript> () != null) {
				if(collider.GetComponent<TeamScript> ().Team == "Zombies") {
					collider.GetComponent<HealthScript> ().photonView.RPC("TakeDamage", PhotonTargets.All, 2);
					player.GetComponent<HealthScript>().photonView.RPC ("Heal", PhotonTargets.All, 1);
					blacklist.Add (collider.transform);
				}
		   	}
		}
	}
	[RPC] void SetPlayer(string player) {
		var searchRoot = GameObject.Find ("/Game/Players").transform;
		for(int i = 0; i < searchRoot.childCount; i++) {
			if(searchRoot.GetChild(i).GetComponent<PlayerControllerScript>().name == player) {
				this.player = searchRoot.GetChild(i);
				this.transform.parent = this.player;
				break;
			}
		}
	}
}
