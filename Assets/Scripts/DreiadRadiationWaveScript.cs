using UnityEngine;
using System.Collections;

public class DreiadRadiationWaveScript : MonoBehaviour {
	void Start () {
	
	}
	void Update () {
		transform.Translate (0, 1f * Time.deltaTime, 0, Space.Self);
		var col = transform.GetChild(0).GetComponent<SpriteRenderer> ().color;
		transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b, col.a - Time.deltaTime);
	}
}
