using System;
using UnityEngine;

public class ActiveAbility : Ability {
	public delegate void OnCast(PlayerControllerScript player);
	public float Cooldown;
	public float CurrentCooldown;
	OnCast onCast;
	public ActiveAbility (float cooldown, OnCast onCast, string info) : base(info) {
		Cooldown = cooldown;
		CurrentCooldown = 0;
		this.onCast = onCast;
	}
	public override void Update(PlayerControllerScript player) {
		base.Update (player);
		if (CurrentCooldown <= 0) {
			SetIconState(player, IconState.None);
			if (Input.GetKey (this.GetKey (player))) {
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