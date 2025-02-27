using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Common.Unity
{
    public class ScriptableObjectSearchWindow : EditorWindow
    {
        private Action<Type> onTypeSelected;
        private List<Type> allScriptableObjectTypes;
        private List<Type> filteredTypes;
        private string searchQuery = "";

        public static void Show(Action<Type> onTypeSelected)
        {
            var window = GetWindow<ScriptableObjectSearchWindow>("Select ScriptableObject Type");
            window.onTypeSelected = onTypeSelected;
            window.Initialize();
        }

        private void Initialize()
        {
            // Find all non-abstract types that derive from ScriptableObject and exclude Unity types
            allScriptableObjectTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                    type.IsSubclassOf(typeof(ScriptableObject)) &&
                    !type.IsAbstract &&
                    !type.IsGenericType &&
                    !IsUnityType(type))
                .OrderBy(type => type.Name)
                .ToList();

            filteredTypes = new List<Type>(allScriptableObjectTypes);
        }

        private bool IsUnityType(Type type)
        {
            // Exclude Unity types by namespace
            string ns = type.Namespace ?? "";
            return ns.StartsWith("UnityEngine") || ns.StartsWith("UnityEditor");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Search for a ScriptableObject type:", EditorStyles.boldLabel);

            // Search bar
            EditorGUI.BeginChangeCheck();
            searchQuery = EditorGUILayout.TextField("Search", searchQuery);
            if (EditorGUI.EndChangeCheck())
            {
                UpdateFilteredList();
            }

            EditorGUILayout.Space();

            // Display filtered results
            foreach (var type in filteredTypes)
            {
                if (GUILayout.Button(type.Name, EditorStyles.objectField))
                {
                    onTypeSelected?.Invoke(type);
                    Close();
                }
            }

            if (filteredTypes.Count == 0)
            {
                EditorGUILayout.HelpBox("No matching ScriptableObject types found.", MessageType.Warning);
            }
        }

        private void UpdateFilteredList()
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                filteredTypes = new List<Type>(allScriptableObjectTypes);
            }
            else
            {
                filteredTypes = allScriptableObjectTypes
                    .Where(type => type.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }
        }
    }
}
