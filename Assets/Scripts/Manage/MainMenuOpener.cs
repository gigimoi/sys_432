using UnityEngine;
using System.Collections;

public class MainMenuOpener : MonoBehaviour {
	void Start () {
		for(int i = 0; i < transform.childCount; i++) {
			if(transform.GetChild(i).name == "MainMenuPanel") {
				transform.GetChild(i).gameObject.SetActive(true);
				break;
			}
		}
	}
}
