using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DifficultyCurve))]

public class DifficultyCurveEditor : Editor{

	LevelSelect current;

	public override void OnInspectorGUI(){
		if (GUILayout.Button ("Create Difficulty File")) {
			LevelSelect newLevels = ScriptableObject.CreateInstance<LevelSelect>();
			newLevels.LevelList = new List<LevelSelect.Level>();

			newLevels.LevelList.Add (new LevelSelect.Level());
			newLevels.LevelList[0].BlockList = new List<LevelSelect.Blocks>();
			newLevels.LevelList[0].Start = 0;

			AssetDatabase.CreateAsset (newLevels,"/DifficultyCurves/newLevels.asset");
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
			current = newLevels;
		}
		if (GUILayout.Button ("Save Difficulty File")) {
			
		}
		GUILayout.Label ("Select Blocks to Appear", EditorStyles.boldLabel);
		if (GUILayout.Toggle(false,"1")) {

		}
		if (GUILayout.Toggle(false,"2")) {
			
		}
		if (GUILayout.Toggle(false,"3")) {
			
		}
		if (GUILayout.Toggle(false,"4")) {
			
		}
	}
}
