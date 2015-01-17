using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnHitDamageScript : Photon.MonoBehaviour {
	public int Damage;
	List<Transform> blacklist = new List<Transform>();
	void OnTriggerEnter2D(Collider2D collider) {
		if (GetComponent<PhotonView>() == null || photonView.isMine) {
			if(!GetBlacklist().Contains(collider.transform)) {
				var team = collider.GetComponent<TeamScript>();
				if(team != null) {
					if(team.Team == "Zombies") { 
						collider.GetComponent<PhotonView>().RPC ("TakeDamage", PhotonTargets.AllBufferedViaServer, Damage);
						GetBlacklist().Add(collider.transform);
					}
				}
			}
		}
	}
	public List<Transform> GetBlacklist() {
		if (transform.parent == null || transform.parent.GetComponent<OnHitDamageScript> () == null) {
			return blacklist;
		} else {
			return transform.parent.GetComponent<OnHitDamageScript> ().GetBlacklist();
		}
	}
}
