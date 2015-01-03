using UnityEngine;
using System.Collections;

public class RotateConstantly : MonoBehaviour {
	public float Speed;
	void Update () {
		transform.Rotate (0, 0, Speed);
	}
}
