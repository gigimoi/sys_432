using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharSelectScript : MonoBehaviour {
	public void OnSelect() {
		var game = GameObject.Find ("/Game").GetComponent<CharacterClassManager> ();
		game.SelectedClass = transform.parent.GetChild (4).GetComponent<Text> ().text;
	}
}
