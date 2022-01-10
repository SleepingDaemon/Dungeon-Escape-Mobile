using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

public enum GameState { NONE, INTRO, ACTIVE, GAMEOVER, PAUSE, COMPLETE, SHOP }

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState                  _state;
    [SerializeField] private float                      _delayIntro;
    [SerializeField] private Player                     _player;
    [SerializeField] private CinemachineVirtualCamera   _camera;
    [SerializeField] private AudioSource                _source = null;
    [SerializeField] private AudioClip                  _gameWon;
    [SerializeField] private AudioClip                  _gameLost;
    [SerializeField] private Animator                   _topBorder;
    [SerializeField] private Animator                   _bottomBorder;
    [SerializeField] private Animator                   _introText;
    [SerializeField] private Animator                   _outroText;
    [SerializeField] private Animator                   _findKey;
    [SerializeField] private int                        _gems;
    [SerializeField] private bool                       _hasKey = false;
    [SerializeField] private bool                       _hasBoots = false;
    [SerializeField] private bool                       _hasFlameSword = false;
    [SerializeField] private bool                       _enableIntro;
    [SerializeField] private GameObject                 _menuOptions;
    [SerializeField] private GameObject                 _bgFadeIn;

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
        if(_enableIntro)
            _state = SetGameState(GameState.INTRO);
    }

    private void Update()
    {
        if(_state == GameState.INTRO)
        {
            GameIntro();
        }
    }

    public void FindKey(bool value)
    {
        _findKey.gameObject.SetActive(value);
    }

    public GameState SetGameState(GameState state)
    {
        _state = state;
        return _state;
    }

    public void PauseGame(bool value)
    {
        if (value == true)
        {
            _state = SetGameState(GameState.PAUSE);
            Time.timeScale = 0;
        }
        else
        {
            _state = SetGameState(GameState.ACTIVE);
            Time.timeScale = 1;
        }
    }

    public void GameOver()
    {
        UIManager.Instance.EnableHUD(false);
        StartCoroutine(GameOverScreenRoutine());
    }

    public void GameWon()
    {
        _state = SetGameState(GameState.COMPLETE);
        UIManager.Instance.EnableHUD(false);
        SetAnimatedGameObjectBorders(true);
        AudioManager.Instance.MusicSource.Stop();
        AudioManager.Instance.PlaySound(_source, _gameWon);
        _outroText.gameObject.SetActive(true);
        StartCoroutine(AcitivateMenuOptionsRoutine());
        //Time.timeScale = 0;
    }

    public void AddGems(int amount)
    {
        _gems += amount;
        UIManager.Instance.UpdateGemCount(_gems);
    }

    IEnumerator GameOverScreenRoutine()
    {
        _state = SetGameState(GameState.GAMEOVER);
        _camera.enabled = false;
        AudioManager.Instance.MusicSource.Stop();
        AudioManager.Instance.PlaySound(_source, _gameLost);
        yield return new WaitForSeconds(1.5f);
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
        _topBorder.gameObject.SetActive(true);
        _bottomBorder.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _introText.gameObject.SetActive(true);
        yield return new WaitForSeconds(_delayIntro);
        TriggerAnimatedGOBorders();
        TriggerIntroText();
        _state = SetGameState(GameState.ACTIVE);
        UIManager.Instance.EnableHUD(true);
        yield return new WaitForSeconds(5f);
        SetAnimatedGameObjectBorders(false);
    }

    public GameState GetGameState() => _state;
    public void TriggerAnimatedGOBorders()
    {
        _topBorder.SetTrigger("goUP");
        _bottomBorder.SetTrigger("goDown");
    }

    public void TriggerIntroText()
    {
        _introText.SetTrigger("fadeOut");
    }

    public void SetAnimatedGameObjectBorders(bool value)
    {
        _topBorder.gameObject.SetActive(value);
        _bottomBorder.gameObject.SetActive(value);
    }

    IEnumerator AcitivateMenuOptionsRoutine()
    {
        yield return new WaitForSeconds(5f);
        _bgFadeIn.SetActive(true);
        _outroText.gameObject.SetActive(false);
        _menuOptions.SetActive(true);
    }
}
