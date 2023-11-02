namespace TreasureHunt.Player
{
    public class PlayerModel
    {
        public BaseHealth Health { get; set; }

        // constructors for playerModel
        public PlayerModel()
        {
            Health = new BaseHealth();
        }

        public PlayerModel(int currHealth, int maxHealth)
        {
            Health = new BaseHealth(currHealth, maxHealth);
        }
    }
}
