using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum IconState { None, Passive, Active, Cooldown }
public class Ability {
	private AbilitiesScript abil;
	private Transform aback;
	public string Info;
	public string Title;
	public Ability(string info, string title) {
		this.Info = info;
		this.Title = title;
		abil = GameObject.Find ("/UI/Canvas/Abilities").GetComponent<AbilitiesScript> ();
		aback = GameObject.Find ("/UI/Canvas/AbilityInfoBack").transform;
	}
	public virtual void Update (PlayerControllerScript player) {
		if(Input.GetKey(KeyCode.Tab)) {
			abil.transform.GetChild(GetIndex(player) * 2 + 1).GetComponent<Text>().text = Info;
			aback.GetComponent<Image>().enabled = true;
		} else {
			abil.transform.GetChild(GetIndex(player) * 2 + 1).GetComponent<Text>().text = "";
			aback.GetComponent<Image>().enabled = false;
		}
	}
	public void SetIconState(PlayerControllerScript player, IconState state) {
		var icon = abil.transform.GetChild (GetIndex (player) * 2).GetChild(0).GetComponent<Image>();
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
	public void SetHelperText(PlayerControllerScript player, string text) {
		abil.transform.GetChild (GetIndex (player) * 2).GetChild (0).GetChild (0).GetComponent<Text> ().text = text;
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
