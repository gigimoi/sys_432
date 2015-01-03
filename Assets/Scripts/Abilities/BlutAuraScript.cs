using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlutAuraScript : Photon.MonoBehaviour {
	public Buff buff = new Buff() {
		time = 0.5f,
		moveSpeed = 0.4f
	};
	private List<Transform> within = new List<Transform>();
	void Start () {
	}
	void Update () {
		for (int i = 0; i < within.Count; i++) {
			if(within[i] != null && within[i].GetComponent<BuffListScript>() != null) {
				within[i].GetComponent<BuffListScript>().AddBuff(buff);
			}
			else if(within[i] == null) {
				within.RemoveAt(i);
				i--;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (photonView.isMine) {
			var team = collider.GetComponent<TeamScript>();
			if(team != null) {
				if(team.Team == "Zombies") { 
					collider.GetComponent<BuffListScript>().AddBuff(buff);
					within.Add(collider.transform);
				}
			}
		}
	}
	void OnTriggerExit2D(Collider2D collider) {
		if (collider != null && collider.transform != null && within.Contains (collider.transform)) {
			within.Remove(collider.transform);
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
