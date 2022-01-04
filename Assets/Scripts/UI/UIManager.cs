using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Text keyAmount;
    [SerializeField] private Text bootsAmount;
    [SerializeField] private Text flameSwordAmount;
    [SerializeField] private Text playerGemAmount;
    [SerializeField] private Image selectionIMG;
    [SerializeField] private GameObject selectionGO;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount, int key, int boot, int flamesword)
    {
        playerGemAmount.text = gemCount.ToString() + "G";
        flameSwordAmount.text = flamesword.ToString() + "G";
        bootsAmount.text = boot.ToString() + "G";
        keyAmount.text = key.ToString() + "G";
    }

    public void UpdateShopSelection(float yPos)
    {
        selectionGO.SetActive(true);
        selectionIMG.rectTransform.anchoredPosition = new Vector2(selectionIMG.rectTransform.anchoredPosition.x, yPos);
    }

    public void DisableSelectionGO() => selectionGO.SetActive(false);
}
