using UnityEngine;
using System.Collections;

public class LightningStrikeScript : MonoBehaviour {
	public Transform particleSys;
	void Start () {
		for (int i = 0; i < 10; i++) {
			var particle = (Transform)Instantiate(particleSys);
			particle.position = transform.position;
			transform.Translate(0, 0.3f, 0, Space.Self);
			particle.particleSystem.startSize += Random.Range(0f, 1f);
		}
	}
	void Update () {
		
	}
}
