using UnityEngine;

namespace TreasureHunt.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerView playerPrefab;

        [SerializeField] private Transform spawnPosition = null;

        private PlayerController playerController;

        private void Start()
        {
            PlayerModel pModel = new PlayerModel();

            playerController = new PlayerController(pModel, playerPrefab);
            playerController.TakeDamage(50); // test
        }
    }
}