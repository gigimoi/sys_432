﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum IconState { None, Passive, Active, Cooldown }
public class Ability {
	private AbilitiesScript abil;
	public Ability() {
		abil = GameObject.Find ("/UI/Canvas/Abilities").GetComponent<AbilitiesScript> ();
	}
	public virtual void Update (PlayerControllerScript player) {


	}
	public void SetIconState(PlayerControllerScript player, IconState state) {
		var icon = abil.transform.GetChild (GetIndex (player)).GetChild(0).GetComponent<Image>();
		if (state == IconState.None) {
			icon.sprite = abil.NoHighlight;
		}
		else if (state == IconState.Passive) {
			icon.sprite = abil.Passive;
		}
		else if (state == IconState.Active) {
			icon.sprite = abil.Active;
		}
		else if (state == IconState.Cooldown) {
			icon.sprite = abil.Cooldown;
		}
	}
	public int GetIndex(PlayerControllerScript player) {
		for(int i = 0; i < player.Abilities.Length; i++) {
			if(player.Abilities[i] == this) {
				return i;
			}
		}
		throw new UnityException("Can't find ability on player but calling GetIndex()");
	}
	public KeyCode GetKey(PlayerControllerScript player) {
		var i = GetIndex (player);
		return i == 0 ? KeyCode.Alpha1 : i == 1 ? KeyCode.Alpha2 : i == 2 ? KeyCode.Alpha3 : KeyCode.Alpha4;
	}
}