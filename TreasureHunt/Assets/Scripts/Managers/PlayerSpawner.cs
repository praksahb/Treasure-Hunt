using Cinemachine;
using UnityEngine;

namespace TreasureHunt.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerView playerPrefab;

        [SerializeField] private PlayerData playerData;

        [SerializeField] private GameObject mainCamera;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        [SerializeField] private Transform spawnPosition = null;

        private GameManager gameManager;

        private PlayerController playerController;

        private void Awake()
        {
            gameManager = GetComponent<GameManager>();
        }

        private void Start()
        {
            PlayerModel pModel = new PlayerModel(playerData);
            playerController = new PlayerController(pModel, playerPrefab, mainCamera);
            virtualCamera.Follow = playerController.GetFollowCamera();
            playerController.TakeDamage(45);
            playerController.TakeDamage(45);
            playerController.TakeDamage(45);
        }
    }
}