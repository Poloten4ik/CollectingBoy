﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PickUps
{
    public class SpeedDown : AbstractsPickUp
    {
        Character character;
        public override void ApplyEffect()
        {
            character = FindObjectOfType<Character>();
            character.SpeedDown();
        }
    }
}
