using System.Collections.Generic;
using TreasureHunt.Interactions;

namespace TreasureHunt.Player
{
    public class KeyInventory
    {
        private List<KeyType> keyList;

        public KeyInventory()
        {
            keyList = new List<KeyType>();
        }

        public void AddKey(KeyType keyType)
        {
            keyList.Add(keyType);
        }

        public bool FindKey(KeyType keyType)
        {
            return keyList.Contains(keyType);
        }
    }
}
