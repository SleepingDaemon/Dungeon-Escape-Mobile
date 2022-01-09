using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

public enum GameState { NONE, INTRO, ACTIVE, GAMEOVER, PAUSE, COMPLETE, }

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState _state;
    [SerializeField] private float _delayIntro;
    [SerializeField] private Player _player;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private AudioSource _source = null;
    [SerializeField] private AudioClip _gameWon;
    [SerializeField] private AudioClip _gameLost;
    [SerializeField] private int _gems;
    [SerializeField] private bool _hasKey = false;
    [SerializeField] private bool _hasBoots = false;
    [SerializeField] private bool _hasFlameSword = false;
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
    public bool HasFlameSword { get => _hasFlameSword; set => _hasFlameSword = value; }
    public Player IsPlayer { get => _player; set => _player = value; }
    public int Gems { get => _gems; set => _gems = value; }

    private void OnEnable()
    {
        AudioManager.Instance.PlayDragonSound();
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _state = SetGameState(GameState.INTRO);
    }

    private void Update()
    {
        if(_state == GameState.INTRO)
        {
            GameIntro();
        }
    }

    private GameState SetGameState(GameState state)
    {
        _state = state;
        return _state;
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
        StartCoroutine(GameOverScreenRoutine());
    }

    public void GameWon()
    {
        AudioManager.Instance.MusicSource.Stop();
        AudioManager.Instance.PlaySound(_source, _gameWon);
        UIManager.Instance.GameWonUI();
        Time.timeScale = 0;
    }

    public void AddGems(int amount)
    {
        _gems += amount;
        UIManager.Instance.UpdateGemCount(_gems);
    }

    IEnumerator GameOverScreenRoutine()
    {
        _camera.enabled = false;
        AudioManager.Instance.MusicSource.Stop();
        AudioManager.Instance.PlaySound(_source, _gameLost);
        yield return new WaitForSeconds(1f);
        UIManager.Instance.GameOverUI();
        Time.timeScale = 0;
    }

    public void SubGems(int amount) => _gems -= amount;
    public int GetGemsAmount() => _gems;
    public void GameIntro()
    {
        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine()
    {
        UIManager.Instance.EnableHUD(false);
        yield return new WaitForSeconds(_delayIntro);
        UIManager.Instance.TriggerIntroBorders();
        _state = SetGameState(GameState.ACTIVE);
        UIManager.Instance.EnableHUD(true);
    }

    public GameState GetGameState() => _state;
}
