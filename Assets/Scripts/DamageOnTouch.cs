using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private int _damageAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            Enemy enemy = this.GetComponent<Enemy>();
            if (player != null)
            {
                if(enemy != null)
                {
                    player.OnDamage(_damageAmount);
                }
                else
                {
                    player.InstantDeath();
                }
            }
        }
    }
}
