using Cinemachine;
using UnityEngine;

namespace TreasureHunt.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Transform spawnPosition = null;
        [SerializeField] private KeysInventoryUI keyInventoryUI;

        private GameManager gameManager;
        private PlayerData playerData;

        private PlayerController playerController;

        private void Awake()
        {
            gameManager = GetComponent<GameManager>();
            playerData = Resources.Load<PlayerData>("ScriptableObjects/PlayerData");
        }

        private void Start()
        {
            PlayerModel pModel = new PlayerModel(playerData);
            playerController = new PlayerController(pModel, playerPrefab, spawnPosition.position);
            virtualCamera.Follow = playerController.GetFollowCamera();
            keyInventoryUI.SetPlayerController(playerController);
        }
    }
}