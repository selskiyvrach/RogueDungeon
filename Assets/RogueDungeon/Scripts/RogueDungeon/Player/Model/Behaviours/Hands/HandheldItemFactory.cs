using System;
using System.Collections.Generic;
using System.Linq;
using Common.MoveSets;
using RogueDungeon.Items;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using Object = UnityEngine.Object;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    /// <summary>
    /// Creates in-hand representation of the item as well as its moveset
    /// </summary>
    public class HandheldItemFactory 
    {
        private readonly MoveSetFactory _moveSetFactory;
        private readonly DiContainer _container;
        private readonly Transform _parent;
        
        private readonly List<Type> _boundInterfaces = new();
        
        private IItem _boundItem;

        public HandheldItemFactory(DiContainer container, MoveSetFactory moveSetFactory, Transform parent)
        {
            _container = container;
            _moveSetFactory = moveSetFactory;
            _parent = parent;
        }
        
        public void Create(IItem item)
        {
            Assert.IsTrue(_boundInterfaces.Count == 0, "Previous item has not been unbound!");
            Assert.IsNull(_boundItem);
            
            foreach (var interfaceType in item.GetType().GetInterfaces().Append(item.GetType()))
            {
                _container.Bind(interfaceType).FromInstance(item).AsCached();
                _boundInterfaces.Add(interfaceType);
            }

            var presenter = _container.InstantiatePrefab(item.Config.ItemPresenterPrefab, _parent).GetComponent<HandHeldItemPresenter>();
            item.Presenter = presenter;
            item.Moveset = _moveSetFactory.Create(item.Config);
            _boundItem = item;
        }

        public void Destroy(IItem item)
        {
            Assert.IsNotNull(item.Presenter);
            Assert.IsNotNull(item.Moveset);
            Assert.AreEqual(item, _boundItem);
            
            Object.Destroy(item.Presenter.gameObject);
            item.Presenter = null;
            item.Moveset = null;
            
            foreach (var interfaceType in _boundInterfaces) 
                _container.Unbind(interfaceType);
            _boundInterfaces.Clear();
            
            _boundItem = null;
        }
    }
}