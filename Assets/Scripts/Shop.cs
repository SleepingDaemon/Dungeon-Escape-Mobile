using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject uiShop;
    [SerializeField] private int flameSwordCost = 200;
    [SerializeField] private int bootsOfFlightCost = 400;
    [SerializeField] private int keyToCastleCost = 100;
    private int _currentItemCost;
    private int _currentSelectedItem;
    private int playerGemAmount;
    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
            if(_player != null)
            {
                playerGemAmount = _player.GetGemAmount();
                UIManager.Instance.OpenShop(_player.GetGemAmount(), keyToCastleCost, bootsOfFlightCost, flameSwordCost);
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

    public void SelectItem(int item)
    {
        print("Selected item " + item);

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

        print(_currentSelectedItem);
    }

    public void BuyItem()
    {
        if(_player.GetGemAmount() >= _currentItemCost)
        {
            if (_currentSelectedItem == 2)
                GameManager.Instance.HasKeyToCastle = true;

            _player.SubGem(_currentItemCost);
            UIManager.Instance.OpenShop(_player.GetGemAmount(), keyToCastleCost, bootsOfFlightCost, flameSwordCost);
        }
        else
        {
            UIManager.Instance.DisableSelectionGO();
            uiShop.SetActive(false);
        }
    }
}
