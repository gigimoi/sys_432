using UnityEngine;
using System.Collections;

public class ZombieSpawnManager : Photon.MonoBehaviour {
	public Transform Zombie;
	int ticker = 0;
	void Start () {
		ticker = 200;
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine && PhotonNetwork.connectedAndReady) {
			int spawnIndex = Random.Range (0, transform.childCount);
			if (ticker > 400) {
				var zombie = PhotonNetwork.Instantiate (Zombie.name,
				                           transform.GetChild (spawnIndex).position, 
				                           transform.GetChild (spawnIndex).rotation, 
				                           0);
				zombie.GetComponent<ZombieAI>().Game = transform.parent;
				ticker = 0;
			}
			ticker++;
		}
	}
}
