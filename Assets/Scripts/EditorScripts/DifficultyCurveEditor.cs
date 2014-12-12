using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DifficultyCurve))]

public class DifficultyCurveEditor : Editor{

	//allows variables from the editor to be used from any location in the script
	public static DifficultyCurveEditor instance;

	private LevelSelect current = null;
	private string fileName = "";
	private string fileLoc = "";
	private string LfileName = "";

	//Checks to see if a level has been loaded
	private bool loaded = false;

	//Changed to just reference LevelSelect variables
	/*//Creates the boolean values for whether a block should appear in each area.
	private bool topLeft = false;
	private bool topMiddle = false;
	private bool topRight = false;
	private bool bottomLeft = false;
	private bool bottomMiddle = false;
	private bool bottomRight = false;

	private int gameSpeed = 0;
	private int prevGameSpeed = 0;*/

	public override void OnInspectorGUI(){

		//Initially user can choose between loading a file or creating a new one

		//Row for creation
		GUILayout.BeginHorizontal ();

		//User can input a name for the new level file here
		fileName = EditorGUILayout.TextField ("New File Name", fileName);

		//When the "Create" button is pressed
		if (GUILayout.Button ("Create Difficulty File")) {
			//A new levelset is created as a list
			LevelSelect newLevels = ScriptableObject.CreateInstance<LevelSelect>();
			newLevels.LevelList = new List<LevelSelect.Level>();

			//Including the very first level - blocks and speed set to 0.
			newLevels.LevelList.Add (new LevelSelect.Level());
			//newLevels.LevelList[0].BlockList = new List<LevelSelect.Blocks>();
			//newLevels.LevelList[0].BlockList.Add (new LevelSelect.Blocks());

			newLevels.LevelList[0].Start = 0;

			newLevels.LevelList[0].bottomLeft = false;
			newLevels.LevelList[0].bottomMiddle = false;
			newLevels.LevelList[0].bottomRight = false;
			newLevels.LevelList[0].topLeft = false;
			newLevels.LevelList[0].topMiddle = false;
			newLevels.LevelList[0].topRight = false;

			//Then this is saved as a new asset ready to be edited
			AssetDatabase.CreateAsset (newLevels,"Assets/DifficultyCurves/" + fileName + ".asset");
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
			current = newLevels;

			loaded = true;
		}
		GUILayout.EndHorizontal ();
		//End creation row

		//Row for loading
		GUILayout.BeginHorizontal ();
		
		//User names file they wish to load
		LfileName = EditorGUILayout.TextField ("Load File Name", LfileName);
		fileLoc = "Assets/DifficultyCurves/" + LfileName + ".asset";

		//When the "Load" button is pressed
		if (GUILayout.Button ("Load Difficulty File")) {			
			//if user has typed in a path
			if (LfileName != ""){
				//Asset at path specified loaded into "current" variable
				current = AssetDatabase.LoadAssetAtPath ("Assets/DifficultyCurves/Test.asset"/*fileLoc*/, typeof(LevelSelect)) as LevelSelect;
				AssetDatabase.Refresh ();
				loaded = true;
			}
		}

		LfileName = "";
		fileLoc = "";

		GUILayout.EndHorizontal ();
		//End loading row

		//only show rest of options if there is a loaded level
		if (loaded) {

			//Saves all changes to the current asset
			if (GUILayout.Button ("Save Difficulty File")) {
					AssetDatabase.SaveAssets ();
					AssetDatabase.Refresh ();
			}

			//Allows the individual difficulty levels on file to be edited
			for(int i = 0; i < current.LevelList.Count; ++i){

				GUILayout.Label ("Level " + (i + 1), EditorStyles.boldLabel);

				GUILayout.Label ("Select Blocks to Appear", EditorStyles.boldLabel);

				//Top row
				GUILayout.BeginHorizontal ();

				current.LevelList[i].topLeft = GUILayout.Toggle (current.LevelList[i].topLeft, "");
				current.LevelList[i].topMiddle = GUILayout.Toggle (current.LevelList[i].topMiddle, "");
				current.LevelList[i].topRight = GUILayout.Toggle (current.LevelList[i].topRight, "");

				GUILayout.EndHorizontal ();
				//End of top row

				//Botoom row
				GUILayout.BeginHorizontal ();

				current.LevelList[i].bottomLeft = GUILayout.Toggle (current.LevelList[i].bottomLeft, "");
				current.LevelList[i].bottomMiddle = GUILayout.Toggle (current.LevelList[i].bottomMiddle, "");
				current.LevelList[i].bottomRight = GUILayout.Toggle (current.LevelList[i].bottomRight, "");

				GUILayout.EndHorizontal ();
				//End of bottom row

				//Slider to set game speed
				current.LevelList[i].Start = EditorGUILayout.IntSlider ("Game Speed:", current.LevelList[i].Start, 0, 1000);

				//Assigns game speed and block values to asset
				/*current.LevelList[i].Start = gameSpeed;
				current.LevelList[i].bottomLeft = bottomLeft;
				current.LevelList[i].bottomMiddle = bottomMiddle;
				current.LevelList[i].bottomRight = bottomRight;
				current.LevelList[i].topLeft = topLeft;
				current.LevelList[i].topMiddle = topMiddle;
				current.LevelList[i].topRight = topRight;*/

			}

			if (GUILayout.Button ("Add Level")) {
				//Adds a new level - blocks and speed set to 0.
				current.LevelList.Add (new LevelSelect.Level());

				current.LevelList[current.LevelList.Count-1].Start = 0;
				current.LevelList[current.LevelList.Count-1].bottomLeft = false;
				current.LevelList[current.LevelList.Count-1].bottomMiddle = false;
				current.LevelList[current.LevelList.Count-1].bottomRight = false;
				current.LevelList[current.LevelList.Count-1].topLeft = false;
				current.LevelList[current.LevelList.Count-1].topMiddle = false;
				current.LevelList[current.LevelList.Count-1].topRight = false;

			}

		}

	}
	void Awake(){
		//allows variables in the editor to be accessed from anywhere in the script
		instance = this;
		
	}

	public LevelSelect returnLevel(){
		return current;
	}
}
