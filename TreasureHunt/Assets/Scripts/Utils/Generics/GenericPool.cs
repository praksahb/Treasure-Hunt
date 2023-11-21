using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunt
{
    public class GenericPool<T> where T : Component
    {
        private T item;
        private Stack<T> poolList;

        private Transform parentTransform;

        public GenericPool(int poolLength, T item, Transform parentTransform)
        {
            this.item = item;
            this.parentTransform = parentTransform;
            InitializePool(poolLength);
        }

        private void InitializePool(int poolLength)
        {
            poolList = new Stack<T>();
            for (int i = 0; i < poolLength; i++)
            {
                T newItem = NewItem(parentTransform);
            }
        }

        private T NewItem(Transform parent)
        {
            T newItem = Object.Instantiate(item, parent);
            poolList.Push(newItem);
            return newItem;
        }

        public T GetItem()
        {
            if (poolList.Count > 0)
            {
                return poolList.Pop();
            }
            return NewItem(parentTransform);
        }

        public void FreeItem(T item)
        {
            poolList.Push(item);
        }
    }
}
