namespace TreasureHunt
{
    public interface IDamageable
    {
        public void StartDamage(int dps, float timeInterval);
        public void StopDamage();
    }
}
