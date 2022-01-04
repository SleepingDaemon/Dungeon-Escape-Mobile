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

    [SerializeField] private Text playerGemAmount;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemAmount.text = gemCount.ToString() + "G";
    }
}
