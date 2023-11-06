using UnityEngine;

namespace TreasureHunt.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public int currentHealth;
        public int maxHealth;
        public float moveSpeed;
        public float sprintSpeed;
    }
}
