using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PickUps
{
    public class AddLife : AbstractsPickUp
    {
        public override void ApplyEffect()
        {
            GameManager.Instance.AddLife();
        }
    }

}
