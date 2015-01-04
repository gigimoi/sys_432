using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(HealthScript))]
public class PlayerControllerScript : Photon.MonoBehaviour {
	public float Speed = 0.01f;
	public int Score;
	Vector2 velocity = new Vector2();
	public Transform target;
	public Ability[] Abilities = new Ability[4];

	float stamina = 1f;
	bool sprinting;
	Transform text;
	public string Username;

	void Start () {
		Screen.lockCursor = true;
		if (photonView.isMine) {
			Camera.current.gameObject.SetActive(false);
			GetComponentInChildren<Camera> ().enabled = true;
			GetComponentInChildren<AudioListener> ().enabled = true;
			transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = true;
		}
		transform.parent = GameObject.Find ("Game").transform.GetChild(2);
		text = GameObject.Find ("/UI/Canvas/TopLeftPanel/TopLeftText").transform;
	}
	void Update () {
		GetComponent<HealthScript>().OnDeath = OnDeath;
		if (photonView.isMine) {
			for(int i = 0; i < Abilities.Length; i++) {
				if(Abilities[i] != null) {
					Abilities[i].Update(this);
				}
			}
			if(stamina < 1) {
				stamina += 0.005f;
				if(stamina > 1) {
					stamina = 1;
				}
			}
			if ((stamina > 0.25 || (sprinting && stamina > 0)) && Input.GetKey (KeyCode.LeftShift)) {
				sprinting = true;
				stamina -= 0.01f;
			} else {
				sprinting = false;
			}
			if(Input.GetMouseButton(0)) {
				Screen.lockCursor = true;
			}
			if(Input.GetKey(KeyCode.Escape)) {
				Screen.lockCursor = false;
			}
			if(Input.GetKey(KeyCode.S)) {
				velocity.y -= Speed * (sprinting ? 1.7f : 1f);
			}
			if(Input.GetKey(KeyCode.W)) {
				velocity.y += Speed * (sprinting ? 1.7f : 1f);
			}
			if(Input.GetKey(KeyCode.A)) {
				velocity.x -= Speed * (sprinting ? 1.7f : 1);
			}
			if(Input.GetKey(KeyCode.D)) {
				velocity.x += Speed * (sprinting ? 1.7f : 1f);
			}
			velocity /= 4;
			rigidbody2D.AddRelativeForce (velocity * 10000 * GetComponent<BuffListScript>().GetMoveSpeedMultiplier());
			var cam = GetComponentInChildren<Camera> ();
			transform.Rotate(0, 0, Input.GetAxis("Mouse X") * -11);
			target.Translate(0, Input.GetAxis("Mouse Y") * 0.32f, 0);
			if(target.localPosition.y < 0.5) {
				target.localPosition = new Vector3(0, 0.5f, 0);
			}
			if(target.localPosition.y > 8f) {
				target.localPosition = new Vector3(0, 8f, 0);
			}
			cam.transform.position = (transform.position + target.transform.position) / 2f + new Vector3(0, 0, -10);
			text.GetComponent<Text>().text += 
				"Health: " + GetComponent<HealthScript>().GetHealth() + "\n" + 
				"Stamina: " + Mathf.Round(stamina * 100);
			transform.GetChild(3).gameObject.SetActive(false);
		} else {
			GetComponentInChildren<Text>().text = Username;
			if(GetComponentInChildren<RectTransform>() != null && Camera.current != null) {
				GetComponentInChildren<RectTransform>().rotation = Camera.current.transform.rotation;
			}
		}
	}
	void OnDeath() {

	}
	[RPC] public void SetCharClass(string clazz) {
		var classes = GameObject.Find ("/Game/").GetComponent<CharacterClassManager> ().Classes;
		int classIndex = -1;
		for (int i = 0; i < classes.Count; i++) {
			if(classes[i].Name == clazz) {
				Abilities = classes[i].Abilities;
				classIndex = i;
				break;
			}
		}
		var abil = GameObject.Find ("UI/Canvas/Abilities").transform;
		for(int i = 0; i < abil.childCount; i++) {
			abil.GetChild(i).GetComponent<Image>().sprite = classes[classIndex].AbilitySprites[i];
		}
	}
	[RPC] public void SetUsername(string username) {
		Username = username;
		if (Username == "") {
			Username = "NoName" + Random.Range(100, 999);
		}
	}
	[RPC] public void AddScore(int score) {
		Score += score;
	}
}
