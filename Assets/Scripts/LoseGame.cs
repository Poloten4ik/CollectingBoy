using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGame : MonoBehaviour
{
    public AudioClip droppedPickUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Buff"))
        {
            GameManager.Instance.LoseLife();
            AudioManager.Instance.PlaySound(droppedPickUp);
        }
    }
}
