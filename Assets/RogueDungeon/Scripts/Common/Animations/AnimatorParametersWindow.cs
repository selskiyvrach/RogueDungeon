using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Common.Animations
{
    public class AnimatorParametersWindow : EditorWindow
    {
        private List<Type> markedClasses;
        private Vector2 scrollPosition;

        [MenuItem("Tools/Animator Parameters")]
        public static void ShowWindow()
        {
            var window = GetWindow<AnimatorParametersWindow>("Animator Parameters");
            window.LoadMarkedClasses();
        }

        private void LoadMarkedClasses()
        {
            markedClasses = AnimatorParameterUtility.GetMarkedClasses();
        }

        private void OnGUI()
        {
            if (markedClasses == null || markedClasses.Count == 0)
            {
                EditorGUILayout.LabelField("No marked classes found.");
                return;
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            foreach (var type in markedClasses)
            {
                if (GUILayout.Button(type.Name))
                {
                    ShowClassDetails(type);
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private void ShowClassDetails(Type type)
        {
            var triggers = AnimatorParameterUtility.GetParametersByAttribute<AnimatorTriggerAttribute>(type);
            var floats = AnimatorParameterUtility.GetParametersByAttribute<AnimatorFloatParameterAttribute>(type);

            var controller = Selection.activeObject as AnimatorController;
            if (controller == null)
            {
                Debug.LogError("No AnimatorController selected.");
                return;
            }

            Undo.RegisterCompleteObjectUndo(controller, "Add Animator Parameters");

            foreach (var trigger in triggers)
            {
                if (!controller.parameters.Any(p => p.name == trigger))
                {
                    controller.AddParameter(trigger, AnimatorControllerParameterType.Trigger);
                }
            }

            foreach (var floatParam in floats)
            {
                if (!controller.parameters.Any(p => p.name == floatParam))
                {
                    controller.AddParameter(floatParam, AnimatorControllerParameterType.Float);
                }
            }

            Debug.Log($"Added {triggers.Count} triggers and {floats.Count} float parameters to AnimatorController.");
        }
    }
}
