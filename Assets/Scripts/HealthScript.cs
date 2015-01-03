using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HealthScript : Photon.MonoBehaviour {
	public int MaxHealth;
	int Health;
	public Sprite[] TexturesAtHealth;
	public Action OnDeath;
	void Start () {
		Health = MaxHealth;
		OnDeath = this.Die;
	}
	void Update () {
		if (PhotonNetwork.connectedAndReady) {
			if (TexturesAtHealth.Length != 0) {
				int index = TexturesAtHealth.Length - 1 - (int)(((float)Health) / ((float)MaxHealth) * ((float)TexturesAtHealth.Length - 1));
				GetComponent<SpriteRenderer>().sprite = TexturesAtHealth [index];
			}
		}
	}
	public void Die() {
		Destroy(gameObject);
	}

	[RPC]public void Heal (int i)
	{
		Health += i;
		if(Health > MaxHealth) {
			Health = MaxHealth;
		}
	}

	[RPC]public void TakeDamage (int i)
	{
		Health -= i;
		if (Health <= 0) {
			Health = 0;
			OnDeath();
		}
	}
	public int GetHealth() {
		return Health;
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isReading) {
			Health = (int)stream.ReceiveNext ();
		} else {
			stream.SendNext(Health);
		}
	}
}
