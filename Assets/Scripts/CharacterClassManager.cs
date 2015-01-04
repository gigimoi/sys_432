using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterClassManager : MonoBehaviour {
	public List<CharClassScript> Classes;
	public Transform UIPrefab;
	void Start() {
		Classes = new List<CharClassScript> ();
		Classes.Add(new BlutSauger());
		Classes.Add(new BlutSauger());
		Classes.Add(new BlutSauger());
		var root = GameObject.Find("/UI/Canvas/MainMenuPanel/CharSelectPanel/CharSelectArea");
		var rootrect = root.GetComponent<RectTransform>();
		for(int i = 0; i < Classes.Count; i++) {
			var ui = (Transform)Instantiate(UIPrefab);
			ui.SetParent(root.transform);
			ui.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 4, -8);
			ui.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 4 + i * 70, 56);
			ui.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
			ui.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
			ui.GetComponentInChildren<Toggle>().group = root.GetComponent<ToggleGroup>();
			if(i > 0) {
				ui.GetComponentInChildren<Toggle>().isOn = false;
			}
		}
		rootrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Classes.Count * 70 + 4);
		GameObject.Find ("/UI/Canvas/MainMenuPanel/CharSelectPanel/Scrollbar").GetComponent<Scrollbar> ().value = 1;
	}
}