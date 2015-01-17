using System;
using UnityEngine;

public class Dreiad : CharClassScript.CharClass {
	public Dreiad () {
		this.Name = "Dreiad";
		this.Abilities [0] = new ActiveAbility (0.5f, "Lightning Strike", LightningStrike, "Lightning arcs between 3 targets");
	}
	void LightningStrike (PlayerControllerScript player) {
		PhotonNetwork.Instantiate(
			"Dreiad Lightning Strike",
			player.transform.position, 
			player.transform.rotation,
			0
		);
	}
}
