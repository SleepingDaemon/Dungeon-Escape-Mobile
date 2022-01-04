public interface IDamageable
{
    int Health { get; set; }
    void OnDamage(int amount);
}
