using System;
using UnityEngine;

public class Dreiad : CharClassScript.CharClass {
	public Dreiad () {
		this.Name = "Dreiad";
		this.Abilities [0] = new ActiveAbility (0.5f, "Lightning Strike", LightningStrike, "Lightning arcs out at random");
		this.Abilities [1] = new PassiveAbility ("Irradiated Soul", "Pulse out 3 radiation waves each second", IrradiatedSoul);
	}
	void LightningStrike (PlayerControllerScript player) {
		PhotonNetwork.Instantiate(
			"Dreiad Lightning Strike",
			player.transform.position, 
			player.transform.rotation,
			0
		);
	}
	float irradiatedSoulTicker = 0;
	bool irradiatedSoulBool = false;
	void IrradiatedSoul (PlayerControllerScript player) {
		irradiatedSoulTicker -= Time.deltaTime;
		if (irradiatedSoulTicker <= 0) {
			for(int i = 0; i < 3; i++) {
				PhotonNetwork.Instantiate(
					"Dreiad Radiation Wave",
					player.transform.position, 
					Quaternion.Euler(player.transform.rotation.eulerAngles + new Vector3(0, 0, i * (360f / 3f) + (irradiatedSoulBool ? 90 : 0))),
					0
				);
			}
			irradiatedSoulTicker += 1;
			irradiatedSoulBool = !irradiatedSoulBool;
		}
	}
}
