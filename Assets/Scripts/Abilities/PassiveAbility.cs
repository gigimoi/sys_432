using System;
public class PassiveAbility : Ability {
	ActiveAbility.OnCast Step;
	public PassiveAbility (string title, string info, ActiveAbility.OnCast step) : base(info, title) {
		Step = step;
	}
	public override void Update(PlayerControllerScript player) {
		base.Update (player);
		Step (player);
		SetIconState (player, IconState.Passive);
	}
}