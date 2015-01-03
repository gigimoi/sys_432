using System;
using UnityEngine;

public class ActiveAbility : Ability {
	public delegate void OnCast(PlayerControllerScript player);
	public float Cooldown;
	public float CurrentCooldown;
	OnCast onCast;
	public ActiveAbility (float cooldown, OnCast onCast) {
		Cooldown = cooldown;
		CurrentCooldown = 0;
		this.onCast = onCast;
	}
	public override void Update(PlayerControllerScript player) {
		if (CurrentCooldown <= 0) {
			SetIconState(player, IconState.None);
			if (Input.GetKey (this.GetKey (player))) {
				onCast(player);
				CurrentCooldown = Cooldown;
			}
		} else {
			CurrentCooldown -= 0.01f;
			SetIconState(player, IconState.Cooldown);
		}
	}
}