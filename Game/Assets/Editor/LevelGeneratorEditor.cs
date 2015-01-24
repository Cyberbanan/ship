using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor {
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		LevelGenerator generator = (LevelGenerator)target;
		if(GUILayout.Button("Generate Level"))
		{
			generator.Generate();
		}
	}
}
