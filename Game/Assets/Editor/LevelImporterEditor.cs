using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LevelImporter))]
public class LevelImporterEditor : Editor {
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		LevelImporter importer = (LevelImporter)target;
		if(GUILayout.Button("Import Level"))
		{
			importer.Import();
		}
	}
}
