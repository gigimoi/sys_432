using System;
using UnityEngine;

public class BlutSauger : CharClassScript {
	public BlutSauger() {;
		Abilities [0] = new ActiveAbility (3, SoulRip, "Deal 2 damage in a cone in front of you, stealing it to yourself");
		Abilities [1] = new ActiveAbility (3, BlutSauge, "Deal 2 damage in an area around you, stealing it to nearby players");
		Abilities [2] = new ActiveAbility (7, BlutCleave, "Deal 3 damage in a 90 arc in front of you, slowing enemies by 15% for 3 seconds");
		Abilities [3] = new ToggleAbility (BlutAuraOn, BlutAuraOff, "Toggleabe Aura around you that slows enemies at the cost of your life");
	}
	public void SoulRip(PlayerControllerScript player) {
		var rip = PhotonNetwork.Instantiate ("Soul Rip", player.transform.position, player.transform.rotation, 0);
		rip.GetPhotonView ().RPC ("SetPlayer", PhotonTargets.All, player.name);
	}
	public void BlutSauge (PlayerControllerScript player) {
		var saug = PhotonNetwork.Instantiate ("Blut Sauge Area", player.transform.position, player.transform.rotation, 0);
		saug.GetPhotonView ().RPC ("SetPlayer", PhotonTargets.All, player.name);
	}
	public void BlutCleave (PlayerControllerScript player) {
		var cleave = PhotonNetwork.Instantiate ("Blut Sauge Cleave", player.transform.position, player.transform.rotation, 0);
		cleave.GetPhotonView ().RPC ("SetPlayer", PhotonTargets.All, player.name);
	}
	GameObject blutAura;
	public void BlutAuraOn (PlayerControllerScript player)
	{
		blutAura = PhotonNetwork.Instantiate ("Blut Sauge Aura", player.transform.position, player.transform.rotation, 0);
		blutAura.GetPhotonView ().RPC ("SetPlayer", PhotonTargets.All, player.name);
	}
	public void BlutAuraOff(PlayerControllerScript player) {
		PhotonNetwork.Destroy (blutAura);
	}

}