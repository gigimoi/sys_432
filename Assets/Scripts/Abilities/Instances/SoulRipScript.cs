using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoulRipScript : Photon.MonoBehaviour {
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
					transform.parent.GetComponent<HealthScript>().photonView.RPC ("Heal", PhotonTargets.All, 1);
					blacklist.Add (collider.transform);
				}
		   	}
		}
	}
}
