using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharClassAbilityUIScript : MonoBehaviour {
	Transform Tooltip;
	void Start() {
		Tooltip = GameObject.Find ("/UI/Canvas/Tooltip").transform;
	}
	public void PointerEnter() {
		Tooltip.gameObject.SetActive (true);
		Tooltip.GetComponent<DisableAfter> ().Pause ();
		Tooltip.GetComponent<DisableAfter> ().SetTime (0.5f);
		for (int i = 0; i < transform.parent.childCount; i++) {
			if(transform.parent.GetChild (i) == transform) {
				Tooltip.GetChild (0).GetComponent<Text>().text = GetComponentInParent<CharClassScript>().Clazz.Abilities[i].Title;
				Tooltip.GetChild (1).GetComponent<Text>().text = GetComponentInParent<CharClassScript>().Clazz.Abilities[i].Info;
			}
		}
	}
	public void PointerExit() {
		Tooltip.GetComponent<DisableAfter> ().Resume ();
		Tooltip.GetComponent<DisableAfter> ().SetTime (0.5f);
	}
}
