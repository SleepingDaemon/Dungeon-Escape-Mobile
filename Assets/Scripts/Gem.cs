using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private int _gems = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player _player = other.GetComponent<Player>();
            if(_player != null)
            {
                GameManager.Instance.AddGems(_gems);
                Destroy(gameObject);
            }
        }
    }

    public void SetGemAmount(int value) => _gems = value;
}
