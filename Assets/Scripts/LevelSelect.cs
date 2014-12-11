using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LevelSelect : ScriptableObject {

	[System.Serializable]
	public class Blocks{
		public bool topLeft;
		public bool topMiddle;
		public bool topRight;
		public bool bottomLeft;
		public bool bottomMiddle;
		public bool bottomRight;
	}

	[System.Serializable]
	public class Level{
		public int Start;
		public List<Blocks> BlockList;
	}
	
	public List<Level> LevelList;

}
