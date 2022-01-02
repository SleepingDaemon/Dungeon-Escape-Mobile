using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int damageAmount = 2;
    private bool _onHit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.gameObject.name);
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
