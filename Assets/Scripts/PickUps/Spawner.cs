using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PickUps
{

    public class Spawner : MonoBehaviour
    {
        public float spawnDuration;
        public List<AbstractsPickUp> Buff;
        public List<AbstractsPickUp> DeBuff;


        private float speedkoef;


        [Range(0, 100)]
        public float buffChange;

        private void Start()
        { 
            StartCoroutine(SpawnCD());
        }

        private void Repeat()
        {
            StartCoroutine(SpawnCD());
        }
        
        private IEnumerator SpawnCD()
        {
            yield return new WaitForSeconds(spawnDuration);
            Vector2 spawnPosition = transform.position;
            spawnPosition.x = Random.Range(-7.2f, 7.2f);

            float change = Random.Range(0, 100);
            if (change < buffChange)
            {
                int buffIndex = Random.Range(0, Buff.Count);
                Instantiate(Buff[buffIndex], spawnPosition, Quaternion.identity);
            }
            else
            {
                int buffIndex = Random.Range(0, DeBuff.Count);
                Instantiate(DeBuff[buffIndex], spawnPosition, Quaternion.identity);
            }           
            Repeat();
        }
    }
}
