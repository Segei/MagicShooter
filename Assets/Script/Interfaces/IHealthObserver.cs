namespace Script.Interfaces
{
    public interface IHealthObserver
    {
        void ChangeHealth(float currentHealth, float maxHealth);
    }
}