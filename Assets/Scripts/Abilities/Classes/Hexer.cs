using System;

public class Hexer : CharClassScript.CharClass {
	public Hexer () {
		this.Name = "Hexer";
		Abilities [0] = new PassiveAbility("Stone Gaze", "Slows all enemies in a 60 degree arc in front of you", StoneGaze);
	}
	void StoneGaze (PlayerControllerScript player) {
	}
}
