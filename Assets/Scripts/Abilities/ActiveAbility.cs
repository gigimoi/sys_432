using System;
using UnityEngine;

public class ActiveAbility : Ability {
	public delegate void OnCast(PlayerControllerScript player);
	public float Cooldown;
	public float CurrentCooldown;
	protected OnCast onCast;
	public ActiveAbility (float cooldown, string title, OnCast onCast, string info) : base(info, title) {
		Cooldown = cooldown;
		CurrentCooldown = 0;
		this.onCast = onCast;
	}
	public override void Update(PlayerControllerScript player) {
		base.Update (player);
		if (CurrentCooldown <= 0) {
			SetIconState(player, IconState.None);
			if (Input.GetKeyDown (this.GetKey (player))) {
				onCast(player);
				CurrentCooldown = Cooldown;
			}
		} else {
			CurrentCooldown -= 0.01f;
			SetIconState(player, IconState.Cooldown);
			if(CurrentCooldown <= 0) {
				SetHelperText(player, "");
			} else {
				SetHelperText(player, "" + Mathf.Round(CurrentCooldown + 0.5f));
			}
		}
	}
}