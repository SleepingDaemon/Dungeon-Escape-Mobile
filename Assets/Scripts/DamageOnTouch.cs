using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private int _damageAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if (hit != null)
                hit.OnDamage(_damageAmount);
        }
    }
}
