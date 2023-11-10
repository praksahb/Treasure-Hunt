using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunt
{
    public class GenericPooling<T> where T : Component
    {
        private T item;
        private Stack<T> poolList;

        public GenericPooling(int poolLength, T item)
        {
            this.item = item;
            InitializePool(poolLength);
        }

        private void InitializePool(int poolLength)
        {
            poolList = new Stack<T>();
            for (int i = 0; i < poolLength; i++)
            {
                T newItem = NewItem();
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
