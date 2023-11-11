using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunt
{
    public class GenericPool<T> where T : Component
    {
        private T item;
        private Stack<T> poolList;

        public GenericPool(int poolLength, T item, Transform parentTransform)
        {
            this.item = item;
            InitializePool(poolLength, parentTransform);
        }

        private void InitializePool(int poolLength, Transform parent)
        {
            poolList = new Stack<T>();
            for (int i = 0; i < poolLength; i++)
            {
                T newItem = NewItem();
                newItem.transform.parent = parent;
            }
        }

        private T NewItem()
        {
            T newItem = Object.Instantiate(item);
            poolList.Push(newItem);
            return newItem;
        }

        public T GetItem()
        {
            if (poolList.Count > 0)
            {
                return poolList.Pop();
            }
            return NewItem();
        }

        public void FreeItem(T item)
        {
            poolList.Push(item);
        }
    }
}