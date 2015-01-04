using System;
using UnityEngine;

public class ParentedScript : Photon.MonoBehaviour
{
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