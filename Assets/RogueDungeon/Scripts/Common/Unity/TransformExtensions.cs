using System.Collections.Generic;
using UnityEngine;

namespace Common.Unity
{
    public static class TransformExtensions
    {
        public static IEnumerable<T> GetDirectChildren<T>(this Transform transform) where T : Component
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var item = transform.GetChild(i).GetComponentOrLogError<T>();
                if (item != null)
                    yield return item;
            }
        }

        private static T GetComponentOrLogError<T>(this Transform transform) where T : Component
        {
            var item = transform.GetComponent<T>();
            if (item == null) 
                Debug.LogError($"Component of type '{typeof(T).Name}' is missing on '{transform.name}'");
            
            return item;
        }

        public static IEnumerable<Transform> FindChildrenRecursive(this Transform root, string name)
        {
            if (root.name == name)
                yield return root;
		
            for (var i = 0; i < root.childCount; i++)
                foreach (var grandChild in root.GetChild(i).FindChildrenRecursive(name))
                    yield return grandChild;
        }
    }
}