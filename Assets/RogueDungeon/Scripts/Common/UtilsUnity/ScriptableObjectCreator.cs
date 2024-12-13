using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Common.UnityUtils
{
    public class ScriptableObjectCreator : EditorWindow
    {
        private List<Type> scriptableObjectTypes = new List<Type> { null };
        private string directoryPath;

        [MenuItem("Assets/Create/Scriptable Object Instance", false, 0)]
        private static void OpenWindow()
        {
            var window = GetWindow<ScriptableObjectCreator>("Create ScriptableObjects");
            window.Initialize();
        }

        private void Initialize()
        {
            directoryPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(directoryPath))
            {
                directoryPath = "Assets";
            }
            else if (!AssetDatabase.IsValidFolder(directoryPath))
            {
                directoryPath = Path.GetDirectoryName(directoryPath);
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Add ScriptableObject types to create:", EditorStyles.boldLabel);

            for (int i = 0; i < scriptableObjectTypes.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                string typeName = scriptableObjectTypes[i]?.Name ?? "None (Click ... to select)";
                EditorGUILayout.LabelField(typeName, EditorStyles.objectField);

                if (GUILayout.Button("...", GUILayout.Width(25)))
                {
                    int index = i; // Capture index for the lambda
                    ScriptableObjectSearchWindow.Show(type =>
                    {
                        scriptableObjectTypes[index] = type;
                    });
                }

                if (GUILayout.Button("+", GUILayout.Width(25)))
                {
                    scriptableObjectTypes.Insert(i + 1, null);
                }

                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    scriptableObjectTypes.RemoveAt(i);
                    i--;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Create"))
            {
                CreateScriptableObjects();
            }
        }

        private void CreateScriptableObjects()
        {
            if (scriptableObjectTypes.Count == 0 || scriptableObjectTypes.TrueForAll(type => type == null))
            {
                Debug.LogWarning("No ScriptableObject types selected for creation.");
                return;
            }

            foreach (var type in scriptableObjectTypes)
            {
                if (type == null) continue;

                // Create an instance of the ScriptableObject
                ScriptableObject instance = ScriptableObject.CreateInstance(type);

                // Save it as an asset in the selected directory
                string assetPath = AssetDatabase.GenerateUniqueAssetPath(
                    Path.Combine(directoryPath, $"{type.Name}.asset"));
                AssetDatabase.CreateAsset(instance, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Close();

            Debug.Log("ScriptableObject instances created successfully.");
        }
    }
}
