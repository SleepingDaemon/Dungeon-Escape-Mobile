using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager                _instance;
    public static UIManager                 Instance
    {
        get
        {
            if (_instance == null) print("UIManager is null");
            return _instance;
        }
    }

    [Header("HUD Gem Count Text")]
    [SerializeField] private Text           gemCountDisplay;

    [Header("Shop Item Cost Text")]
    [SerializeField] private Text           keyAmount;
    [SerializeField] private Text           bootsAmount;
    [SerializeField] private Text           flameSwordAmount;
    [SerializeField] private Text           healthPotionAmount;

    [Header("Shop Player Gem Count Text")]
    [SerializeField] private Text           playerGemAmount;

    [Header("Shop Item Names")]
    [SerializeField] private Text           flameSword;
    [SerializeField] private Text           bootsOfFlight;
    [SerializeField] private Text           keyToCastle;
    [SerializeField] private Text           healthPotion;

    [Header("Misc")]
    [SerializeField] private Image          selectionIMG;
    [SerializeField] private Image          aButton;
    [SerializeField] private Image          bButton;
    [SerializeField] private Image          joyStick;
    [SerializeField] private GameObject     selectionGO;
    [SerializeField] private GameObject     uiShop;
    [SerializeField] private GameObject     hudGO;
    [SerializeField] private GameObject     pauseMenu;
    [SerializeField] private GameObject     gameOverMenu;
    [SerializeField] private Image[]        healthBars = new Image[4];
    private bool _pause = false;

    public Text KeyAmount { get => keyAmount; set => keyAmount = value; }
    public Text BootsAmount { get => bootsAmount; set => bootsAmount = value; }
    public Text FlameSwordAmount { get => flameSwordAmount; set => flameSwordAmount = value; }

    public bool Pause { get => _pause; set => _pause = value; }

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        GameManager.Instance.SetGameState(GameState.SHOP);
        Cursor.visible = true;
        EnableHUD(false);
        playerGemAmount.text = gemCount + "G";
    }

    public void CloseShop()
    {
        uiShop.SetActive(false);
        selectionGO.SetActive(false);
        EnableHUD(true);
        gemCountDisplay.text = GameManager.Instance.GetGemsAmount().ToString() + "G";
        Cursor.visible = false;
        GameManager.Instance.SetGameState(GameState.ACTIVE);
    }

    public void UpdateShopSelection(float yPos)
    {
        selectionGO.SetActive(true);
        selectionIMG.rectTransform.anchoredPosition = new Vector2(selectionIMG.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int gemCount)
    {
        gemCountDisplay.text = gemCount.ToString() + "G";
    }

    public void UpdateShopGemCount(int gemCount) => playerGemAmount.text = gemCount.ToString() + "G";

    public void UpdateLives(int livesRemaining, bool onHealthPotion)
    {
        if(livesRemaining == 0)
        {
            healthBars[0].enabled = false;
            healthBars[1].enabled = false;
            healthBars[2].enabled = false;
            healthBars[3].enabled = false;
        }

        for (int i = 0; i <= livesRemaining; i++)
        {
            if (i == livesRemaining)
                healthBars[i].enabled = false;

            if (onHealthPotion)
            {
                i = 0;
            }
        }
    }

    public void UpdateLivesOnHealthPotion(Player player)
    {
        healthBars[0].enabled = true;
        healthBars[1].enabled = true;
        healthBars[2].enabled = true;
        healthBars[3].enabled = true;
    }

    public void PauseMenu()
    {
        _pause = !_pause;

        if (_pause)
        {
            GameManager.Instance.PauseGame(true);
            pauseMenu.SetActive(true);
            EnableHUD(false);
        }
        else
        {
            GameManager.Instance.PauseGame(false);
            pauseMenu.SetActive(false);
            EnableHUD(true);
        }
    }

    public void GameOverUI()
    {
        gameOverMenu.SetActive(true);
    }

    public void EnableShop(bool value)
    {
        uiShop.SetActive(value);

    }

    public void EnableHUD(bool value)
    {
        hudGO.SetActive(value);
        aButton.enabled = value;
        bButton.enabled = value;
        joyStick.enabled = value;
    }

    public void ChangeItemTextColor(int selection, Color color)
    {
        switch (selection)
        {
            case 0:
                flameSword.color = color;
                break;
            case 1:
                bootsOfFlight.color = color;
                break;
            case 2:
                keyToCastle.color = color;
                break;
            //case 3:
            //    healthPotion.color = color;
            //    break;
        }
    }

    public void ChangeItemPriceText(int selection)
    {
        if (selection == 0)
            FlameSwordAmount.text = "--SOLD---";
        if (selection == 1)
            BootsAmount.text = "--SOLD---";
        if (selection == 2)
            KeyAmount.text = "--SOLD---";
        //if (selection == 3)
        //    healthPotionAmount.text = "--SOLD--";
    }
}
