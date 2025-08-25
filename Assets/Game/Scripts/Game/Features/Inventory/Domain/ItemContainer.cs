using System;
using System.Collections.Generic;
using Game.Libs.Items;
using Libs.Commands;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public abstract class ItemContainer
    {
        public event Action OnContentChanged;
        public ContainerId Id { get; }
        protected ItemContainer(ContainerId id) => 
            Id = id;

        public abstract IEnumerable<(IItem item, Vector2 posNormalized)> GetItems();
        public abstract ICommand GetExtractItemCommand(string itemId, IExtractedItemCaretaker extractedItemCaretaker);
        public abstract ItemPlacementResult GetItemPlacement(ItemPlacementProposition proposition);
        
        protected abstract class ItemOperationCommand : ICommand
        {
            private readonly ItemContainer _container;
            protected ItemOperationCommand(ItemContainer container) => 
                _container = container;

            public void Execute()
            {
                ExecuteInternal();
                _container.OnContentChanged?.Invoke();
            }

            public void Undo()
            {
                UndoInternal();
                _container.OnContentChanged?.Invoke();
            }

            protected abstract void ExecuteInternal();
            protected abstract void UndoInternal();
        }
    }
}