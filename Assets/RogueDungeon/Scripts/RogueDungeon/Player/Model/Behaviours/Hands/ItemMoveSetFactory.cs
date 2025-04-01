using System;
using System.Collections.Generic;
using Common.MoveSets;
using RogueDungeon.Items;
using UnityEngine.Assertions;
using Zenject;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class ItemMoveSetFactory : MoveSetFactory
    {
        private readonly List<Type> _boundInterfaces = new();

        public ItemMoveSetFactory(DiContainer container) : base(container) { }

        public void BindItem(IItem item)
        {
            Assert.IsTrue(_boundInterfaces.Count == 0);
            
            foreach (var interfaceType in item.GetType().GetInterfaces())
            {
                Container.Bind(interfaceType).FromInstance(item).AsCached();
                _boundInterfaces.Add(interfaceType);
            }
        }

        public void Unbind()
        {
            foreach (var interfaceType in _boundInterfaces) 
                Container.Unbind(interfaceType);
            _boundInterfaces.Clear();
        }
    }
}