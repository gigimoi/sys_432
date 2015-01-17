using UnityEngine;
using System.Collections;

public class DestroyAfter : Photon.MonoBehaviour {
	public float Time;
	bool photonDestroy = false;
	void Start () {
		if (photonView == null) {
			Invoke ("DestroySelf", Time);
		} else if (photonView.isMine) {
			Invoke ("DestroySelf", Time);
			photonDestroy = true;
		}
	}
	void DestroySelf () {
		if (photonDestroy) {
			PhotonNetwork.Destroy (gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
