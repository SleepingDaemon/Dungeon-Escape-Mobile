using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private int _gems;
    private int randomNumber;

    private void Start()
    {
        randomNumber = Random.Range(2, 8);
        _gems = randomNumber;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player _player = other.GetComponent<Player>();
            if(_player != null)
            {
                AudioManager.Instance.PlayGemSound();
                GameManager.Instance.AddGems(_gems);
                Destroy(gameObject);
            }
        }
    }

    public void SetGemAmount(int value) => _gems = value;
}
