namespace TreasureHunt.Player
{
    public class BaseHealth
    {
        private int maxHealth;
        private int currentHealth;

        // Properties

        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            set
            {
                if (value >= 0)
                {
                    currentHealth = (value <= maxHealth) ? value : maxHealth;
                }
                else
                {
                    currentHealth = 0;
                    // trigger health over action or event
                    // Or handle in player model or player controller
                }
            }
        }

        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }
            set
            {
                if (value > 0)
                {
                    maxHealth = value;
                }
                else
                {
                    // throw error, or set default max health
                    maxHealth = 100;
                }
            }
        }

        public BaseHealth()
        {
            MaxHealth = 100;
            CurrentHealth = 100;
        }

        public BaseHealth(int currHealth, int maxHealth)
        {
            this.MaxHealth = maxHealth;
            this.CurrentHealth = currHealth;
        }
    }
}
