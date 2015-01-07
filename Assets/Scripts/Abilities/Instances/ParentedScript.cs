using System;
using UnityEngine;

public class ParentedScript : Photon.MonoBehaviour
{
	[RPC] void SetParent(int id) {
		transform.parent = PhotonView.Find (id).transform;
	}
	[RPC] void SetPlayer(string player) {
		SetPlayer(player, Vector3.zero);
	}
	[RPC] void SetPlayer(string player, Vector3 rotationOffset) {
		var searchRoot = GameObject.Find ("/Game/Players").transform;
		for(int i = 0; i < searchRoot.childCount; i++) {
			if(searchRoot.GetChild(i).GetComponent<PlayerControllerScript>().name == player) {
				this.transform.parent = searchRoot.GetChild(i);
				this.transform.Rotate(rotationOffset);
				break;
			}
		}
	}
}