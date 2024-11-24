using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UiCommons
{
    public abstract class SiblingsListMonoBehaviour<T> : MonoBehaviour, ISiblingsList<T> where T : Component
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private bool _worldPosStays;

        private SiblingsList<T> _siblingsList;
        public int Count => _siblingsList.Count;
        public T this[int index] => _siblingsList[index];
        
        public void SetActiveItemsCount(int count) => 
            _siblingsList.SetActiveItemsCount(count);

        private void Awake() => 
            _siblingsList = new SiblingsList<T>(_prefab, _parent, _worldPosStays);
        
        public IEnumerator<T> GetEnumerator() => 
            _siblingsList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }
}