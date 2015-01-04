using System;
using UnityEngine;


public class CharClassScript : MonoBehaviour {
	public class CharClass {
		public Ability[] Abilities = new Ability[4];
		public Sprite[] AbilitySprites = new Sprite[4];
		public string Name;
	}
	public CharClass Clazz;
	public string Name;
	public Sprite[] Abilities = new Sprite[4];
}