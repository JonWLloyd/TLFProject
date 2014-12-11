using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DifficultyCurve))]

public class DifficultyCurveEditor : Editor{

	LevelSelect current = null;
	private string fileName = "";
	private string fileLoc = "";

	//Checks to see if a level has been loaded
	private bool loaded = false;

	//Creates the boolean values for whether a block should appear in each area.
	private bool topLeft = false;
	private bool topMiddle = false;
	private bool topRight = false;
	private bool bottomLeft = false;
	private bool bottomMiddle = false;
	private bool bottomRight = false;

	public override void OnInspectorGUI(){

		//Initially user can choose between loading a file or creating a new one

		//Row for creation
		GUILayout.BeginHorizontal ();

		//User can input a name for the new level file here
		fileName = EditorGUILayout.TextField ("New File Name");

		//When the "Create" button is pressed
		if (GUILayout.Button ("Create Difficulty File")) {
			//A new levelset is created as a list
			LevelSelect newLevels = ScriptableObject.CreateInstance<LevelSelect>();
			newLevels.LevelList = new List<LevelSelect.Level>();

			//Including the very first level - blocks and speed set to 0.
			newLevels.LevelList.Add (new LevelSelect.Level());
			newLevels.LevelList[0].BlockList = new List<LevelSelect.Blocks>();
			newLevels.LevelList[0].Start = 0;

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
		
		//User can input a name for the new level file here
		fileLoc = EditorGUILayout.TextField ("");
		
		//When the "Load" button is pressed
		if (GUILayout.Button ("Load Difficulty File")) {			
			//if user has typed in a path
			if (fileLoc != ""){
				//Asset at path specified loaded into "current" variable
				current = AssetDatabase.LoadAssetAtPath (fileLoc, typeof(LevelSelect)) as LevelSelect;
				AssetDatabase.Refresh ();
				loaded = true;
			}
		}

		GUILayout.EndHorizontal ();
		//End loading row

		//only show rest of options if there is a loaded level
		if (loaded) {

			//Saves all changes to the current asset
			if (GUILayout.Button ("Save Difficulty File")) {
					AssetDatabase.SaveAssets ();
					AssetDatabase.Refresh ();
			}
			GUILayout.Label ("Select Blocks to Appear", EditorStyles.boldLabel);

			//Top row
			GUILayout.BeginHorizontal ();

			topLeft = GUILayout.Toggle (topLeft, "");
			topMiddle = GUILayout.Toggle (topMiddle, "");
			topRight = GUILayout.Toggle (topRight, "");

			GUILayout.EndHorizontal ();
			//End of top row

			//Botoom row
			GUILayout.BeginHorizontal ();

			bottomLeft = GUILayout.Toggle (bottomLeft, "");
			bottomMiddle = GUILayout.Toggle (bottomMiddle, "");
			bottomRight = GUILayout.Toggle (bottomRight, "");

			GUILayout.EndHorizontal ();
			//End of bottom row
		}

	}
}
