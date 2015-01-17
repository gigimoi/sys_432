using UnityEngine;
using System.Collections;

public class LightningStrikeScript : MonoBehaviour {
	public Transform particleSys;
	void Start () {
		Branch (2, 0f);
		transform.position = new Vector3 (0, 0, 0);
		transform.rotation = new Quaternion ();
	}
	void Branch(int chance, float rotation) {
		while (rotation < -70) {
			rotation += Random.Range(0, 45f);
		}
		while (rotation > 70) {
			rotation -= Random.Range(0, 45f);
		}
		transform.Rotate (0, 0, rotation);
		var index = 0;
		for (int i = 0; i < Random.Range(5, 7); i++) {
			var particle = (Transform)Instantiate(particleSys);
			particle.parent = transform;
			particle.localPosition = transform.position;
			transform.Translate(0, 0.35f, 0, Space.Self);
			particle.particleSystem.startSize += Random.Range(-0.5f, 1f);
			if(Random.Range(0, chance) == 0) {
				Branch (chance * 2, rotation + Random.Range(-35f, 35f));
			}
			index++;
		}
		transform.Translate (0, -0.35f * index, 0);
		transform.Rotate (0, 0, -rotation);
	}
	void Update () {
		
	}
}
