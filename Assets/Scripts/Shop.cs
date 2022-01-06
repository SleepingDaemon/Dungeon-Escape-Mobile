using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private int flameSwordCost = 200;
    [SerializeField] private int bootsOfFlightCost = 400;
    [SerializeField] private int keyToCastleCost = 100;
    private int _currentItemCost;
    private int _currentSelectedItem;
    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
            if(_player != null)
            {
                UIManager.Instance.OpenShop(GameManager.Instance.GetGemsAmount(), keyToCastleCost, bootsOfFlightCost, flameSwordCost);
            }

            UIManager.Instance.EnableShop(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.CloseShop();
        }
    }

    public void SelectItem(int item)
    {
        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(-58);
                _currentSelectedItem = item;
                _currentItemCost = flameSwordCost;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-171);
                _currentSelectedItem = item;
                _currentItemCost = bootsOfFlightCost;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-286);
                _currentSelectedItem = item;
                _currentItemCost = keyToCastleCost;
                break;
        }

        Debug.Log("Item Selected: " + item);
    }

    public void BuyItem()
    {
        if(GameManager.Instance.GetGemsAmount() >= _currentItemCost)
        {
            if (_currentSelectedItem == 2)
                GameManager.Instance.HasKeyToCastle = true;

            GameManager.Instance.SubGems(_currentItemCost);
            UIManager.Instance.OpenShop(GameManager.Instance.GetGemsAmount(), keyToCastleCost, bootsOfFlightCost, flameSwordCost);
        }
        else
        {
            UIManager.Instance.CloseShop();
        }
    }
}
