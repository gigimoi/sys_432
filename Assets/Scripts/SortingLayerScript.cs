using UnityEngine;
using System.Collections;

public class SortingLayerScript : MonoBehaviour {
	public int Layer;
	void Update ()
	{
		//Change Foreground to the layer you want it to display on 
		//You could prob. make a public variable for this
		particleSystem.renderer.sortingLayerID = Layer;
	}
}
