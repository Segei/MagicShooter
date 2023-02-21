namespace Script.Interfaces
{
    public interface IHealth
    {
        void UpdateHealth();
        void AddHealth(float health);
        void TakeDamage(float damage);
    }
}