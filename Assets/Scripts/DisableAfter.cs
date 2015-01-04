using UnityEngine;
using System.Collections;

public class DisableAfter : MonoBehaviour {
	public float TimeToDisable;
	public bool Paused;
	void Start () {
	
	}
	void Update () {
		if (!Paused) {
			TimeToDisable -= Time.deltaTime;
		}
		if (TimeToDisable <= 0) {
			gameObject.SetActive(false);
		}
	}
	public void Pause() {
		Paused = true;
	}
	public void Resume() {
		Paused = false;
	}
	public void SetTime(float time) {
		TimeToDisable = time;
	}
}
