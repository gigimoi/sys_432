using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConnectScript : Photon.MonoBehaviour {
	public GameObject PlayerPrefab;
	Transform text;
	Transform username;
	Transform scoreboard;
	Transform game;
	void Start () {
		text = GameObject.Find ("/UI/Canvas/TopLeftPanel/TopLeftText").transform;
		username = GameObject.Find ("/UI/Canvas/MainMenuPanel/NameInput/Text").transform;
		scoreboard = GameObject.Find ("/UI/Canvas/ScoreboardText").transform;
		game = GameObject.Find ("/Game").transform;
	}
	public void Play() {
		PhotonNetwork.ConnectUsingSettings("v4.2");
	}
	void OnLeftRoom() {
	}
	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom("Main Room");
	}
	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnJoinedRoom() {
		int spawnIndex = Random.Range (0, transform.GetChild(0).childCount);
		var player = PhotonNetwork.Instantiate (
			PlayerPrefab.name, 
			transform.GetChild (0).GetChild (spawnIndex).position,
		    transform.GetChild (0).GetChild (spawnIndex).rotation,
			0
		);
		player.GetPhotonView ().RPC ("SetUsername", PhotonTargets.AllBuffered, username.GetComponent<Text> ().text);
	}
	void Update() {
		if (PhotonNetwork.connectionState.ToString () != "Connected") {
			text.GetComponent<Text> ().text = PhotonNetwork.connectionStateDetailed.ToString () + "\n";
		} else {
			text.GetComponent<Text> ().text = "";
		}
		scoreboard.GetComponent<Text>().text = "";
		scoreboard.GetChild (0).GetComponent<Text>().text = "";
		var comp = game.GetChild (2).GetComponentsInChildren<PlayerControllerScript> ();
		for(int i = 0; i < comp.Length; i++) {
			var player = comp[i];
			scoreboard.GetComponent<Text>().text += player.Username + ":" + player.Score + "\n";
			scoreboard.GetChild (0).GetComponent<Text>().text += player.Username + ":" + player.Score +  "\n";
		}
	}
}
