using System;
public class BlutSauger {
	public void SoulRip(PlayerControllerScript player) {
		var rip = PhotonNetwork.Instantiate ("Soul Rip", player.transform.position, player.transform.rotation, 0);
		rip.GetPhotonView ().RPC ("SetPlayer", PhotonTargets.All, player.name);
	}
	public void BlutSauge (PlayerControllerScript player) {
		var saug = PhotonNetwork.Instantiate ("Blut Sauge Area", player.transform.position, player.transform.rotation, 0);
	}
}