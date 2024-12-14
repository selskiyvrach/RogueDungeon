using UnityEditor;
using UnityEngine;

namespace Common.Animations
{
    [InitializeOnLoad]
    public static class AnimatorWindowExtension
    {
        static AnimatorWindowExtension()
        {
            EditorApplication.update += UpdateAnimatorWindow;
        }

        private static void UpdateAnimatorWindow()
        {
            var animatorWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.AnimatorControllerTool");
            var activeWindow = EditorWindow.focusedWindow;

            if (activeWindow && activeWindow.GetType() == animatorWindowType)
            {
                var toolbarRect = new Rect(10, 10, 120, 20);
                if (GUI.Button(toolbarRect, "Load Triggers"))
                {
                    AnimatorParametersWindow.ShowWindow();
                }
            }
        }
    }
}