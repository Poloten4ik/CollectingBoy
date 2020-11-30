using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PickUps
{
    public abstract class AbstractsPickUp : MonoBehaviour
    {
        [Header("Sounds")]
        public AudioClip caughtPickUp;
      
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Character"))
            {
                ApplyEffect();
                AudioManager.Instance.PlaySound(caughtPickUp);
            }

            if (collision.gameObject.CompareTag("Character") || collision.gameObject.CompareTag("Lose Game"))
            {
                Destroy(gameObject);
            }
        }
        public abstract void ApplyEffect();
    }
}

