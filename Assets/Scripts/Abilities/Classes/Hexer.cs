using System;
using UnityEngine;

public class Hexer : CharClassScript.CharClass {
	public Hexer () {
		this.Name = "Hexer";
		Abilities [0] = new PassiveAbility("Stone Gaze", "Passively slows all enemies in a 60 degree arc in front of you by 15%", StoneGaze);
		Abilities [1] = new ActiveAbility (14, "Blink", Blink, "Teleport forward 3 meters");
		Abilities [2] = new ActiveAbility (12, "Eye Shield", EyeShield, "Block all damage taken from the front for 2 seconds");
	}
	bool hasCreatedStoneGazeObject = false;
	void StoneGaze (PlayerControllerScript player) {
		if (!hasCreatedStoneGazeObject) {
			var gaze = PhotonNetwork.Instantiate("Hexer Stone Gaze", player.transform.position, player.transform.rotation, 0);
			gaze.GetPhotonView().RPC ("SetPlayer", PhotonTargets.AllBufferedViaServer, player.name);
			hasCreatedStoneGazeObject = true;
		}
	}
	void Blink(PlayerControllerScript player) {
		PhotonNetwork.Instantiate ("Hexer Blink Particle", player.transform.position, Quaternion.Euler (0, 0, 0), 0);
		var oldLayer = player.gameObject.layer;
		player.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
		float angle = player.transform.localEulerAngles.z + 90;
		var vec = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
		var ray = Physics2D.Raycast (
			new Vector2 (player.transform.position.x, player.transform.position.y),
			vec,
			3
		);
		player.gameObject.layer = oldLayer;
		if (ray.collider != null) {
			player.transform.position = new Vector3 (ray.point.x, ray.point.y, player.transform.position.z);
		} else {
			player.transform.Translate(vec * 3, Space.World); 
		}
	}
	void EyeShield (PlayerControllerScript player) {
	}
}
