using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlutDropAreaScript : Photon.MonoBehaviour {
	int lifetime;
	List<Transform> blacklist = new List<Transform>();
	void Start () {
		lifetime = 0;
	}
	void Update () {
		lifetime++;
		if (photonView.isMine) {
			if (lifetime > 60) {
				PhotonNetwork.Destroy(gameObject);
			}
		}
		if (lifetime < 20) {
			transform.localScale *= 1.02f;
		} else {
			transform.localScale *= 0.96f;
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (photonView.isMine) {
			if(!blacklist.Contains(collider.transform)) {
				var team = collider.GetComponent<TeamScript>();
				if(team != null) {
					if(team.Team == "Zombies") { 
						collider.GetComponent<PhotonView>().RPC ("TakeDamage", PhotonTargets.AllBufferedViaServer, 2);
						PhotonNetwork.Instantiate("Blut Drop", collider.transform.position, new Quaternion(), 0);
						blacklist.Add(collider.transform);
					}
				}
			}
		}
	}
}
