using Cinemachine;
using UnityEngine;

namespace TreasureHunt.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Player Spawn Data")]
        [Tooltip("Reference of playerPrefab which has playerView script attached")]
        [SerializeField] private PlayerView playerPrefab;
        [Tooltip("Virtual Camera reference for when player is alive, should have higher priority")]
        [SerializeField] private CinemachineVirtualCamera aliveCamera;
        [Tooltip("Virtual Camera reference for when player is dead")]
        [SerializeField] private CinemachineVirtualCamera deadCamera;
        [Tooltip("Where the player will be spawning at start of game")]
        [SerializeField] private Transform spawnPosition;
        [Tooltip("Reference for collected keys to be displayed in pause menu screen")]
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
            aliveCamera.Follow = playerController.GetFollowTarget_Alive();
            deadCamera.Follow = playerController.GetFollowTarget_Dead();
            playerController.PlayerDied += ChangeVCam;
            keyInventoryUI.SetPlayerController(playerController);
        }

        private void ChangeVCam()
        {
            // make deadCamera priority higher
            deadCamera.Priority = aliveCamera.Priority + 1;
            playerController.PlayerDied -= ChangeVCam;
        }
    }
}