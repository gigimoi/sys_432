using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EyeOfIgnitionScript : Photon.MonoBehaviour {
	List<Transform> inGaze = new List<Transform>();
	void Update() {
		for(int i = 0; i < inGaze.Count; i++) {
			if(inGaze[i] == null) {
				inGaze.RemoveAt (i);
				i--;
			} else {
				inGaze[i].GetComponent<BuffListScript>().AddBuff(StaticBuffs.Ignited);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.GetComponent<BuffListScript>() != null && 
		    collider.GetComponent<TeamScript>() != null && 
		    collider.GetComponent<TeamScript>().Team == "Zombies") 
		{
			inGaze.Add(collider.transform);
		}
	}
	void OnTriggerExit2D(Collider2D collider) {
		if (collider.GetComponent<BuffListScript>() != null) {
			inGaze.Remove(collider.transform);
		}
	}
}
