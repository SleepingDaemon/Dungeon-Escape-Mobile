using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null) print("UIManager is null");
            return _instance;
        }
    }

    [SerializeField] private Text           gemCountDisplay;
    [SerializeField] private Text           keyAmount;
    [SerializeField] private Text           bootsAmount;
    [SerializeField] private Text           flameSwordAmount;
    [SerializeField] private Text           healthPotionAmount;
    [SerializeField] private Text           playerGemAmount;
    [SerializeField] private Image          selectionIMG;
    [SerializeField] private Image          aButton;
    [SerializeField] private Image          bButton;
    [SerializeField] private Image          joyStick;
    [SerializeField] private GameObject     selectionGO;
    [SerializeField] private GameObject     uiShop;
    [SerializeField] private GameObject     hudGO;
    [SerializeField] private GameObject     pauseMenu;
    [SerializeField] private GameObject     gameOverMenu;
    [SerializeField] private GameObject     gameWon;
    [SerializeField] private Image[]        healthBars = new Image[4];
    [SerializeField] private Animator       topBorder;
    [SerializeField] private Animator       bottomBorder;
    private bool _pause = false;

    public bool Pause { get => _pause; set => _pause = value; }

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount, int key, int boot, int flamesword, int potion)
    {
        EnableHUD(false);
        playerGemAmount.text = gemCount.ToString() + "G";
        flameSwordAmount.text = flamesword.ToString() + "G";
        bootsAmount.text = boot.ToString() + "G";
        keyAmount.text = key.ToString() + "G";
        healthPotionAmount.text = potion.ToString() + "G";
    }

    public void CloseShop()
    {
        uiShop.SetActive(false);
        selectionGO.SetActive(false);
        EnableHUD(true);
        gemCountDisplay.text = GameManager.Instance.GetGemsAmount().ToString() + "G";
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

    public void UpdateLives(int livesRemaining)
    {
        if(livesRemaining == 4)
        {
            healthBars[0].enabled = true;
            healthBars[1].enabled = true;
            healthBars[2].enabled = true;
            healthBars[3].enabled = true;
            return;
        }

        for (int i = 0; i <= livesRemaining; i++)
        {
            if (i == livesRemaining)
                healthBars[i].enabled = false;
        }
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

    public void GameWonUI()
    {
        gameWon.SetActive(true);
    }

    public void EnableShop(bool value) => uiShop.SetActive(value);
    public void EnableHUD(bool value)
    {
        hudGO.SetActive(value);
        aButton.enabled = value;
        bButton.enabled = value;
        joyStick.enabled = value;
    }

    public void TriggerIntroBorders()
    {
        topBorder.SetTrigger("goUP");
        bottomBorder.SetTrigger("goDown");
    }
}
