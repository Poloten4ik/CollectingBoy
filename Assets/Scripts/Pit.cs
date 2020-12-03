using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Pit : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Character"))
            {
                GameManager.Instance.GameOver();

            }
            if (collision.gameObject.CompareTag("Buff"))
            {
                GameManager.Instance.LoseLife();
            }
        }
    }
}
