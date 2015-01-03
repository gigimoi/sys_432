using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlutCleaveScript : Photon.MonoBehaviour {
	List<Transform> blacklist = new List<Transform>();
	void Start () {
		transform.localRotation = Quaternion.Euler (0, 0, 45);
	}
	void Update () {
		transform.Rotate (0, 0, -3);
		if (transform.localRotation.eulerAngles.z <= 315 && transform.localRotation.eulerAngles.z >= 50) {
			if(photonView.isMine) {
				PhotonNetwork.Destroy(gameObject);
			} else {
				Destroy (gameObject);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if(photonView.isMine) {
			if (!blacklist.Contains(collider.transform) && collider.GetComponent<TeamScript> () != null) {
				if(collider.GetComponent<TeamScript> ().Team == "Zombies") {
					collider.GetComponent<HealthScript> ().photonView.RPC("TakeDamage", PhotonTargets.All, 3);
					blacklist.Add (collider.transform);
				}
			}
		}
	}
	[RPC] void SetPlayer(string player) {
		var searchRoot = GameObject.Find ("/Game/Players").transform;
		for(int i = 0; i < searchRoot.childCount; i++) {
			if(searchRoot.GetChild(i).GetComponent<PlayerControllerScript>().name == player) {
				this.transform.parent = searchRoot.GetChild(i);
				break;
			}
		}
	}
}
