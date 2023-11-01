using UnityEngine;

namespace TreasureHunt.Player
{
    public class PlayerController
    {
        public PlayerView PlayerView { get; }
        public PlayerModel PlayerModel { get; }


        public PlayerController(PlayerModel playerModel, PlayerView playerPrefab)
        {
            PlayerModel = playerModel;
            PlayerView = Object.Instantiate(playerPrefab);
            PlayerView.PlayerController = this;
        }
    }
}
