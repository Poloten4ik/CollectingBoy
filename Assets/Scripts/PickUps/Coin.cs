using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PickUps
{
    public class Coin : AbstractsPickUp
    {
        public int points;

        public override void ApplyEffect()
        {
            GameManager.Instance.AddScore(points);
        }
    }
}

