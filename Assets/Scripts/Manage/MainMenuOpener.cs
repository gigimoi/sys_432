using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuOpener : MonoBehaviour {
	void Start () {
		for(int i = 0; i < transform.childCount; i++) {
			if(transform.GetChild(i).name == "MainMenuPanel") {
				transform.GetChild(i).gameObject.SetActive(true);
			}
			else if(transform.GetChild(i).name == "AbilityInfoBack") {
				transform.GetChild(i).GetComponent<Image>().enabled = false;
			}
		}
	}
}
