using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _gems = 0;
    [SerializeField] private bool _hasKey = false;
    [SerializeField] private bool _hasBoots = false;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                print("GameManager is null");
            return _instance;
        }
    }

    public bool HasKeyToCastle { get => _hasKey; set => _hasKey = value; }
    public bool HasBootsOfFlight { get => _hasBoots; set => _hasBoots = value; }
    public int Gems { get => _gems; set => _gems = value; }

    private void Awake()
    {
        _instance = this;
    }

    public void PauseGame(bool value)
    {
        if (value == true)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void GameOver()
    {
        UIManager.Instance.GameOverUI();
    }

    public void GameWon()
    {
        UIManager.Instance.GameWonUI();
    }

    public void AddGems(int amount)
    {
        _gems += amount;
        UIManager.Instance.UpdateGemCount(_gems);
    }

    public void SubGems(int amount) => _gems -= amount;
    public int GetGemsAmount() => _gems;
}
