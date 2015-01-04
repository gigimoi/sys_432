using UnityEngine;
using System.Collections;

public class DestroyAfter : Photon.MonoBehaviour {
	public int Time;
	void Start () {
		if (photonView.isMine) {
			Invoke ("DestroySelf", Time);
		}
	}
	void DestroySelf () {
		PhotonNetwork.Destroy (gameObject);
	}
}
