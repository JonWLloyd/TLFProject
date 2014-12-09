using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DifficultyCurve))]

public class DifficultyCurveEditor : Editor{
	public override void OnInspectorGUI(){
		if (GUILayout.Button ("Open Difficulty File")) {
			
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
