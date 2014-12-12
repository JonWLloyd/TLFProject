using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LevelSelect : ScriptableObject {

	[System.Serializable]
	public class Level{
		public int Start;

		public bool topLeft;
		public bool topMiddle;
		public bool topRight;
		public bool bottomLeft;
		public bool bottomMiddle;
		public bool bottomRight;

	}
	
	public List<Level> LevelList;

}
