using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private int flameSwordCost     = 200;
    [SerializeField] private int bootsOfFlightCost  = 400;
    [SerializeField] private int keyToCastleCost    = 100;
    [SerializeField] private int healthPotionCost   = 50;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _selectSound;
    [SerializeField] private AudioClip _purchaseSound;
    [SerializeField] private AudioClip _healthPotion;
    [SerializeField] private AudioClip _keySound;
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
                UIManager.Instance.OpenShop(GameManager.Instance.GetGemsAmount(), keyToCastleCost, bootsOfFlightCost, flameSwordCost, healthPotionCost);
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
                UIManager.Instance.UpdateShopSelection(-45);
                _currentSelectedItem = item;
                _currentItemCost = flameSwordCost;
                AudioManager.Instance.PlaySound(_source, _selectSound);
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-131);
                _currentSelectedItem = item;
                _currentItemCost = bootsOfFlightCost;
                AudioManager.Instance.PlaySound(_source, _selectSound);
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-218);
                _currentSelectedItem = item;
                _currentItemCost = keyToCastleCost;
                AudioManager.Instance.PlaySound(_source, _selectSound);
                break;
            case 3:
                UIManager.Instance.UpdateShopSelection(-305);
                _currentSelectedItem = item;
                _currentItemCost = healthPotionCost;
                AudioManager.Instance.PlaySound(_source, _selectSound);
                break;
        }

        Debug.Log("Item Selected: " + item);
    }

    public void BuyItem()
    {
        if(GameManager.Instance.GetGemsAmount() >= _currentItemCost)
        {
            if (_currentSelectedItem == 0)
                GameManager.Instance.HasFlameSword = true;
            if (_currentSelectedItem == 1)
                GameManager.Instance.HasBootsOfFlight = true;
            if (_currentSelectedItem == 2)
            {
                AudioManager.Instance.PlaySound(_source, _keySound);
                GameManager.Instance.HasKeyToCastle = true;
            }
            if (_currentSelectedItem == 3)
            {
                _player.AddHealth(4);
                AudioManager.Instance.PlaySound(_source, _healthPotion);
            }

            AudioManager.Instance.PlaySound(_source, _purchaseSound);
            GameManager.Instance.SubGems(_currentItemCost);
            UIManager.Instance.OpenShop(GameManager.Instance.GetGemsAmount(), keyToCastleCost, bootsOfFlightCost, flameSwordCost, healthPotionCost);
        }
        else
        {
            UIManager.Instance.CloseShop();
        }
    }
}
