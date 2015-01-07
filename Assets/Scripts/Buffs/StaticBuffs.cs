using UnityEngine;
using System.Collections;

public class StaticBuffs : MonoBehaviour {
	public static Buff Ignited = new Buff () {
		time = 2,
		damageTickTime = 0.5f,
		damage = 1,
	};
	public Transform IgnitedParticleSystem;
	void Start() {
		Ignited.particleEffect = IgnitedParticleSystem;
	}
}
