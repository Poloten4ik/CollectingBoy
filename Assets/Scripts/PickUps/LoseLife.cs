using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PickUps
{
    public class LoseLife : AbstractsPickUp
    {
        public override void ApplyEffect()
        {
            GameManager.Instance.LoseLife();
        }
    }

}
