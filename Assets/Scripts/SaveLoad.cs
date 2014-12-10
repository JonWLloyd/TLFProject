using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//This script to be used for saving and loading the difficulty data

public static class SaveLoad {

	public static List<DifficultyCurve> diffCurves = new List<DifficultyCurve>();

	//Function for saving the game
	public static void Save(){
		diffCurves.Add (DifficultyCurve.current);
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/diffCurves.dc");
		bf.Serialize (file, SaveLoad.diffCurves);
		file.Close ();
	}

	//Function for loading the game
	public static void Load(){

		//Checks to see if there is a file to load
		if (File.Exists (Application.persistentDataPath + "/diffCurves.dc")) {
		
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/diffCurves.dc", FileMode.Open);
			SaveLoad.diffCurves = (List<DifficultyCurve>)bf.Deserialize (file);
			file.Close ();
		}

	}

}