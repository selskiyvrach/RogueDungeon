using System;
using System.Collections;
using System.Collections.Generic;
using Common.Unity;
using UnityEngine;

namespace Common.UI
 {
     public class SiblingsList<T> : ISiblingsList<T> where T : Component
     {
         private readonly Func<T> _creator;
         private readonly List<T> _activeItems = new();
         private readonly Queue<T> _inactiveItems = new();

         public int Count => _activeItems.Count;

         public T this[int index] => _activeItems[index];

         public SiblingsList(T prefab, Transform parent, bool worldPosStays = false)
         {
             if(prefab == null)
                 throw new ArgumentNullException(nameof(prefab));
            
             if (parent == null)
                 throw new ArgumentNullException(nameof(parent));

             _creator = () => UnityEngine.Object.Instantiate(prefab, parent, worldPosStays);
            
             foreach (var child in parent.GetDirectChildren<T>())
                 if (child.gameObject.activeSelf)
                     _activeItems.Add(child);
                 else
                     _inactiveItems.Enqueue(child);
         }

         public void SetActiveItemsCount(int count)
         {
             while(_activeItems.Count < count) 
                 AddItemToActiveItems();

             while (count < _activeItems.Count) 
                 RemoveItemFromActiveItems();
         }

         public IEnumerator<T> GetEnumerator() => 
             _activeItems.GetEnumerator();

         IEnumerator IEnumerable.GetEnumerator() => 
             GetEnumerator();

         private void RemoveItemFromActiveItems()
         {
             var item = _activeItems[^1];
             item.gameObject.SetActive(false);
             _activeItems.RemoveAt(_activeItems.Count - 1);
             _inactiveItems.Enqueue(item);
         }

         private void AddItemToActiveItems()
         {
             var item = _inactiveItems.TryDequeue(out var dequeued) 
                 ? dequeued 
                 : _creator?.Invoke();

             _activeItems.Add(item);
             item.transform.SetAsLastSibling();
             item.gameObject.SetActive(true);
         }
     }
 }