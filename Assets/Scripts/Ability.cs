using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] protected float _timeUsed;
        [SerializeField] protected CharacterMovement character;
        [SerializeField] protected double _cooldown;
        [SerializeField] protected string _name;
        [SerializeField] protected bool _available;
        protected bool _inCooldown;
        protected bool _activated;
        [SerializeField] protected double _durationTime;
        public virtual void Use()
        {

        }
        protected void End()
        {
            _inCooldown = true;
        }
        protected void Update()
        {
            if(Time.time - _timeUsed >= _cooldown && _inCooldown)
            {
                _inCooldown = false;
            }
            if((Time.time - _timeUsed >= _durationTime) && _activated)
            {
                _activated = false;
                End();
            }
        }
    }
}

