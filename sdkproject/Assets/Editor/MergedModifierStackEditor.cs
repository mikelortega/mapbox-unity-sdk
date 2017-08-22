﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using System.ComponentModel;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.MeshGeneration.Interfaces;
using Mapbox.Unity.MeshGeneration.Modifiers;

namespace Mapbox.NodeEditor
{
	[CustomEditor(typeof(MergedModifierStack))]
	public class MergedModifierStackEditor : UnityEditor.Editor
	{
		private MonoScript script;
		private void OnEnable()
		{
			script = MonoScript.FromScriptableObject((MergedModifierStack)target);
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			GUI.enabled = false;
			script = EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false) as MonoScript;
			GUI.enabled = true;

			EditorGUILayout.Space();

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Mesh Modifiers");
			var facs = serializedObject.FindProperty("MeshModifiers");
			for (int i = 0; i < facs.arraySize; i++)
			{
				var ind = i;
				EditorGUILayout.BeginHorizontal();
				GUI.enabled = false;
				facs.GetArrayElementAtIndex(ind).objectReferenceValue = EditorGUILayout.ObjectField(facs.GetArrayElementAtIndex(i).objectReferenceValue, typeof(MeshModifier)) as ScriptableObject;
				GUI.enabled = true;

				if (GUILayout.Button(new GUIContent("E"), GUILayout.Width(20)))
				{
					Mapbox.NodeEditor.ScriptableCreatorWindow.Open(typeof(MeshModifier), facs, ind);
				}
				if (GUILayout.Button(new GUIContent("-"), GUILayout.Width(20)))
				{
					facs.DeleteArrayElementAtIndex(ind);
				}
				EditorGUILayout.EndHorizontal();
			}

			if (GUILayout.Button(new GUIContent("Add New")))
			{
				Mapbox.NodeEditor.ScriptableCreatorWindow.Open(typeof(MeshModifier), facs);
			}

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Game Object Modifiers");
			var facs2 = serializedObject.FindProperty("GoModifiers");
			for (int i = 0; i < facs2.arraySize; i++)
			{
				var ind = i;
				EditorGUILayout.BeginHorizontal();
				GUI.enabled = false;
				facs2.GetArrayElementAtIndex(ind).objectReferenceValue = EditorGUILayout.ObjectField(facs2.GetArrayElementAtIndex(i).objectReferenceValue, typeof(GameObjectModifier)) as ScriptableObject;
				GUI.enabled = true;

				if (GUILayout.Button(new GUIContent("E"), GUILayout.Width(20)))
				{
					Mapbox.NodeEditor.ScriptableCreatorWindow.Open(typeof(GameObjectModifier), facs2, ind);
				}
				if (GUILayout.Button(new GUIContent("-"), GUILayout.Width(20)))
				{
					facs2.DeleteArrayElementAtIndex(ind);
				}
				EditorGUILayout.EndHorizontal();
			}

			if (GUILayout.Button(new GUIContent("Add New")))
			{
				Mapbox.NodeEditor.ScriptableCreatorWindow.Open(typeof(GameObjectModifier), facs2);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}