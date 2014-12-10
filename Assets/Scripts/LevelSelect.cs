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

	/*int Level1;
	int Level2;
	int Level3;
	int Level4;
	int Level5;
	int Level6;
	int Level7;
	int Level8;
	int Level9;
	int Level10;*/

}
