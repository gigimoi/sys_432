using System;

public class Hexer : CharClassScript.CharClass {
	public Hexer () {
		this.Name = "Hexer";
		Abilities [0] = new PassiveAbility("Stone Gaze", "Slows all enemies in a 60 degree arc in front of you by 15%", StoneGaze);
	}
	bool hasCreatedStoneGazeObject = false;
	void StoneGaze (PlayerControllerScript player) {
		if (!hasCreatedStoneGazeObject) {
			var gaze = PhotonNetwork.Instantiate("Hexer Stone Gaze", player.transform.position, player.transform.rotation, 0);
			gaze.GetPhotonView().RPC ("SetPlayer", PhotonTargets.AllBufferedViaServer, player.name);
			hasCreatedStoneGazeObject = true;
		}
	}
}
