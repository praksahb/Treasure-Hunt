namespace TreasureHunt.Player
{
    public class PlayerModel
    {
        public BaseHealth Health { get; set; }
        public KeyInventory KeyInventory { get; set; }

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
    }
}
