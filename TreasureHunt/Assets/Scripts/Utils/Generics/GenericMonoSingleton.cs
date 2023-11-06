using UnityEngine;

namespace TreasureHunt
{
    public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            //// 1. Persistent singleton behaviour
            if (Instance == null)
            {
                instance = (T)this;
                DontDestroyOnLoad(this as T);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
