using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunControllerScript : Photon.MonoBehaviour {
	bool gunInWall = false;
	int ticker = 0;

	public int MaxAmmo;
	public int MaxClip;
	bool reloading;
	int clip;
	int ammo;
	private Transform text;
	private PlayerControllerScript player;

	void Start () {
		ammo = MaxAmmo;
		clip = MaxClip;
		text = GameObject.Find ("/UI/Canvas/TopLeftPanel/TopLeftText").transform;
		player = transform.parent.GetComponent<PlayerControllerScript> ();
	}
	void Update () {
		if (transform.parent.GetComponent<PlayerControllerScript> ().photonView.isMine) {
			if(!reloading && clip > 0 && !gunInWall && Input.GetMouseButton(0) && ticker == 0) {
				var bullet = PhotonNetwork.Instantiate("Simple Bullet", transform.GetChild(0).position, transform.GetChild(0).rotation, 0);
				bullet.GetPhotonView().RPC ("SetPlayer", PhotonTargets.AllBuffered, player.Username);
				clip--;
				if(clip == 0 && ammo > 0) {
					photonView.RPC ("Reload", PhotonTargets.All, true);
				}
				ticker = 10;
			}
			if (ticker > 0) {
				ticker--;
			}
			if(!reloading && Input.GetKeyDown(KeyCode.R) && ammo > 0 && clip != MaxClip) {
				photonView.RPC ("Reload", PhotonTargets.All, true);
			}
			text.GetComponent<Text>().text += "\nClip: " + clip + "\nAmmo: " + ammo;
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Collision") {
			gunInWall = true;
		}
	}
	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.name == "Collision") {
			gunInWall = false;
		}
	}
	[RPC] void Reload() {
		while (clip < MaxClip) {
			if(ammo > 0) {
				clip++;
				ammo--;
			} else {
				break;
			}
		}
		transform.Rotate (0, 0, -25);
		reloading = false;
	}
	[RPC] void Reload(bool rpc) {
		Invoke ("Reload", 1f);
		transform.Rotate (0, 0, 25);
		reloading = true;
	}
}
