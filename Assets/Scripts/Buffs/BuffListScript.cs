using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffListScript : MonoBehaviour {
	List<Buff> buffs = new List<Buff>();
	List<float> timers = new List<float>();
	void Start () {
	}
	void Update () {
		for (int i = 0; i < timers.Count; i++) {
			if(timers[i] != -1) {
				timers[i] -= Time.deltaTime;
				if(timers[i] < 0) {
					buffs.RemoveAt(i);
					timers.RemoveAt(i);
					i--;
				}
			}
		}
	}
	public void AddBuff(Buff b) {
		if (!buffs.Contains (b)) {
			buffs.Add(b);
			timers.Add(b.time);
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
