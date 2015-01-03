using System;
using UnityEngine;


public class BlutSauger {
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