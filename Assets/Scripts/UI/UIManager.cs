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
    [SerializeField] private Text           playerGemAmount;
    [SerializeField] private Image          selectionIMG;
    [SerializeField] private Image          aButton;
    [SerializeField] private Image          bButton;
    [SerializeField] private Image          joyStick;
    [SerializeField] private GameObject     selectionGO;
    [SerializeField] private GameObject     uiShop;
    [SerializeField] private GameObject     hudGO;
    [SerializeField] private GameObject     pauseMenu;
    [SerializeField] private Image[]        healthBars = new Image[4];
    private bool _pause = false;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount, int key, int boot, int flamesword)
    {
        hudGO.SetActive(false);
        aButton.enabled = false;
        bButton.enabled = false;
        joyStick.enabled = false;
        playerGemAmount.text = gemCount.ToString() + "G";
        flameSwordAmount.text = flamesword.ToString() + "G";
        bootsAmount.text = boot.ToString() + "G";
        keyAmount.text = key.ToString() + "G";
    }

    public void CloseShop()
    {
        uiShop.SetActive(false);
        selectionGO.SetActive(false);
        hudGO.SetActive(true);
        aButton.enabled = true;
        bButton.enabled = true;
        joyStick.enabled = true;
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
        for (int i = 0; i <= livesRemaining; i++)
        {
            if(i == livesRemaining)
                healthBars[i].enabled = false;
        }
    }

    public void PauseMenu()
    {
        _pause = !_pause;
        if (_pause)
        {
            pauseMenu.SetActive(true);
            hudGO.SetActive(false);
            aButton.enabled = false;
            bButton.enabled = false;
            joyStick.enabled = false;
        }
        else
        {
            pauseMenu.SetActive(false);
            hudGO.SetActive(true);
            aButton.enabled = true;
            bButton.enabled = true;
            joyStick.enabled = true;
        }
    }

    public void EnableShop(bool value) => uiShop.SetActive(value);
}
