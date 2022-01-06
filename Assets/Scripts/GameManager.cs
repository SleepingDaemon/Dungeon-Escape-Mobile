using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _gems = 0;
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
    public bool HasKeyToCastle { get; set; }
    public int Gems { get => _gems; set => _gems = value; }

    private void Awake()
    {
        _instance = this;
    }

    public void AddGems(int amount)
    {
        _gems += amount;
        UIManager.Instance.UpdateGemCount(_gems);
    }

    public void SubGems(int amount) => _gems -= amount;
    public int GetGemsAmount() => _gems;
}
