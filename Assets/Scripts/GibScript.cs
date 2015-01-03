using UnityEngine;
using System.Collections;

public class GibScript : MonoBehaviour {
	public Sprite[] Sprites;
	void Start () {
		Invoke ("Die", 0.5f);
		rigidbody2D.AddForce(new Vector2(
			Random.Range(0, 100) * (Random.Range(0, 2) == 1 ? 1 : -1),
			Random.Range(0, 100) * (Random.Range(0, 2) == 1 ? 1 : -1)
		));
		transform.eulerAngles = new Vector3 (0, 0, Random.Range (0, 360));
		GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length)];
	}
	void Update () {
		
	}
	void Die() {
		Destroy (this.gameObject);
	}
}
