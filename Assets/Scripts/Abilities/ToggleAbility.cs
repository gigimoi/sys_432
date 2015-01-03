using System;
public class ToggleAbility : ActiveAbility {
	public OnCast Enable;
	public OnCast Disable;
	bool enabled;
	public ToggleAbility (OnCast onEnable, OnCast onDisable, string info) : base(0, onEnable, info) {
		Enable = onEnable;
		Disable = onDisable;
	}
	public override void Update (PlayerControllerScript player)
	{
		onCast = enabled ? Disable : Enable;
		onCast += toggle;
		base.Update (player);
		SetIconState(player, enabled ? IconState.Passive : IconState.None);
	}
	void toggle(PlayerControllerScript player) {
		enabled = !enabled;
	}
}