using UnityEngine;
using System.Collections;

public class CharClassAbilityUIScript : MonoBehaviour {
	Transform Tooltip;
	void Start() {
		Tooltip = GameObject.Find ("/UI/Canvas/Tooltip").transform;
	}
	public void PointerEnter() {
		Tooltip.gameObject.SetActive (true);
		Tooltip.GetComponent<DisableAfter> ().Pause ();
		Tooltip.GetComponent<DisableAfter> ().SetTime (0.5f);
	}
	public void PointerExit() {
		Tooltip.GetComponent<DisableAfter> ().Resume ();
		Tooltip.GetComponent<DisableAfter> ().SetTime (0.5f);
	}
}
