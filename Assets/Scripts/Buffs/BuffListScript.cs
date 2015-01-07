using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffListScript : Photon.MonoBehaviour {
	List<Buff> buffs = new List<Buff>();
	List<float> timers = new List<float>();
	List<float> damageTickTimers = new List<float>();
	List<Transform> particleEffects = new List<Transform>();
	void Start () {
	}
	void Update () {
		for (int i = 0; i < timers.Count; i++) {
			if(timers[i] != -1) {
				timers[i] -= Time.deltaTime;
				damageTickTimers[i] -= Time.deltaTime;
				if(damageTickTimers[i] < 0) {
					damageTickTimers[i] = buffs[i].damageTickTime;
					if(GetComponent<HealthScript>() != null && buffs[i].damage > 0) {
						photonView.RPC("TakeDamage", PhotonTargets.AllBuffered, buffs[i].damage);
					}
				}
				if(timers[i] < 0) {
					buffs.RemoveAt(i);
					timers.RemoveAt(i);
					damageTickTimers.RemoveAt (i);
					if(particleEffects[i] != null) {
						PhotonNetwork.Destroy(particleEffects[i].gameObject);
					}
					particleEffects.RemoveAt(i);
					i--;
				}
			}
		}
	}
	public void AddBuff(Buff b) {
		if (!buffs.Contains (b)) {
			buffs.Add(b);
			timers.Add(b.time);
			damageTickTimers.Add(b.damageTickTime);
			particleEffects.Add (b.particleEffect);
			if(b.particleEffect != null) {
				var particle = PhotonNetwork.Instantiate(b.particleEffect.name, transform.position + new Vector3(0, -0.1f, 0), b.particleEffect.rotation, 0);
				particle.GetPhotonView().RPC ("SetParent", PhotonTargets.All, photonView.viewID);
			}
		} else {
			timers[buffs.IndexOf(b)] = b.time;
		}
	}
	public float GetMoveSpeedMultiplier() {
		float ms = 1.0f;
		for (int i = 0; i < buffs.Count; i++) {
			ms *= buffs[i].moveSpeed;
		}
		return ms;
	}
}
