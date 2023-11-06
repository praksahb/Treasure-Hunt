namespace TreasureHunt.Player
{
    public class PlayerModel
    {
        // Variables - can be used to set speed values from here or can be set directly from 
        // FPScontroller script attached to player prefab
        private float moveSpeed;
        private float sprintSpeed;

        // public access Properties
        public BaseHealth Health { get; set; }
        public KeyInventory KeyInventory { get; set; }

        // public read-only Properties
        public float MoveSpeed { get { return moveSpeed; } }
        public float SprintSpeed { get { return sprintSpeed; } }

        // constructors for playerModel
        public PlayerModel()
        {
            Health = new BaseHealth();
            KeyInventory = new KeyInventory();
        }

        public PlayerModel(int currHealth, int maxHealth)
        {
            Health = new BaseHealth(currHealth, maxHealth);
            KeyInventory = new KeyInventory();
        }

        public PlayerModel(PlayerData playerData)
        {
            Health = new BaseHealth(playerData.currentHealth, playerData.maxHealth);
            KeyInventory = new KeyInventory();
            moveSpeed = playerData.moveSpeed;
            sprintSpeed = playerData.sprintSpeed;
        }
    }
}
