using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int damageAmount = 2;
    private bool _onHit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            if (!_onHit)
            {
                hit.OnDamage(damageAmount);
                _onHit = true;
            }

            StartCoroutine(AttackResetRoutine());
        }
    }

    IEnumerator AttackResetRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _onHit = false;
    }
}
