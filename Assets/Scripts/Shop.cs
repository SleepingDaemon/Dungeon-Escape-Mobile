using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject uiShop;
    private bool _openShop = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player _player = other.GetComponent<Player>();
            if(_player != null)
            {
                int playerGemAmount = _player.GetDiamondAmount();
                UIManager.Instance.OpenShop(playerGemAmount);
            }
            uiShop.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiShop.SetActive(false);
        }
    }
}
