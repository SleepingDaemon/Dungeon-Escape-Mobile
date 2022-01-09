using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(GameManager.Instance.HasKeyToCastle == true)
            {
                GameManager.Instance.GameWon();
            }
            else
            {
                GameManager.Instance.FindKey(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.FindKey(false);
        }
    }
}
