using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class ChangeGravity : Ability
    {
        public override void Use()
        {
            if (_available && !_activated && !_inCooldown)
            {
                _activated = true;
                _inCooldown = true;
                _timeUsed = Time.time;
                character.ChangeGravity();
            }
        }
    }
}
