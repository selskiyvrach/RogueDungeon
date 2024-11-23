using System.Collections.Generic;
using UnityEngine;

namespace RogueDungeon.Services.DebugTools
{
    public class DebugHUD : MonoBehaviour
    {
        private static DebugHUD _instance;
        private static DebugHUD Instance
        {
            get
            {
                if (_instance != null) 
                    return _instance;
                
                _instance = new GameObject("[DebugHud]").AddComponent<DebugHUD>();
                DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }

        private readonly Dictionary<object, string> _debugDataEntries = new();
        private static Vector2 _scrollPosition = Vector2.zero;

        public static void SetData(object sender, string info) => 
            Instance._debugDataEntries[sender] = info;
        
        public static void ClearData(object sender) => 
            Instance._debugDataEntries.Remove(sender);

        private void OnGUI()
        {
            var yOffset = 10f;
            var width = 800f;
            var height = 75f;

            _scrollPosition = GUI.BeginScrollView(
                new Rect(10, 10, width + 20, Screen.height - 20),
                _scrollPosition,
                new Rect(0, 0, width, height * _debugDataEntries.Count)
            );

            foreach (var entry in _debugDataEntries)
            {
                GUI.Label(new Rect(10, yOffset, width, height), $"{entry.Key.GetType().Name}: {entry.Value}", style: new GUIStyle {fontSize = 30});
                yOffset += height;
            }

            GUI.EndScrollView();
        }
    }
}