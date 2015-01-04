using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterClassManager : MonoBehaviour {
	public List<CharClassScript.CharClass> Classes;
	public Transform UIPrefab;
	public string SelectedClass;
	void Start() {
		Classes = new List<CharClassScript.CharClass> ();
		Classes.Add(new BlutSauger());
		Classes.Add(new Hexer());
		var classInfos = GameObject.Find ("/Game/Classes").GetComponentsInChildren<CharClassScript> ();
		var root = GameObject.Find("/UI/Canvas/MainMenuPanel/CharSelectPanel/CharSelectArea");
		var rootrect = root.GetComponent<RectTransform>();
		for (int i = 0; i < Classes.Count; i++) {
			CharClassScript script = null;
			for(int j = 0; j < classInfos.Length; j++) {
				if(classInfos[j].name == Classes[i].Name) {
					script = classInfos[j];
					break;
				}
			}
			var ui = (Transform)Instantiate (UIPrefab);
			ui.SetParent (root.transform);
			ui.GetComponent<RectTransform> ().SetInsetAndSizeFromParentEdge (RectTransform.Edge.Left, 4, -8);
			ui.GetComponent<RectTransform> ().SetInsetAndSizeFromParentEdge (RectTransform.Edge.Top, 4 + i * 70, 56);
			ui.GetComponent<RectTransform> ().anchorMax = new Vector2 (1, 1);
			ui.GetComponent<RectTransform> ().anchorMin = new Vector2 (0, 1);
			ui.GetComponentInChildren<Toggle> ().group = root.transform.parent.GetComponent<ToggleGroup> ();
			if (i > 0) {
				ui.GetComponentInChildren<Toggle> ().isOn = false;
			}
			for(int j = 0; j < 4; j++) {
				ui.GetChild(j).GetComponent<Image>().sprite = script.Abilities[j];
				Classes[i].AbilitySprites[j] = script.Abilities[j];
			}
			ui.GetChild(4).GetComponent<Text>().text = script.name;
			ui.gameObject.AddComponent (typeof(CharClassScript));
			ui.GetComponent<CharClassScript>().Clazz = Classes[i];
		}
		rootrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Classes.Count * 70 + 4);
		GameObject.Find ("/UI/Canvas/MainMenuPanel/CharSelectPanel/Scrollbar").GetComponent<Scrollbar> ().value = 1;
		SelectedClass = Classes [0].Name;
	}
}