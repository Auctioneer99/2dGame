using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Damage
{
    internal class DeathArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var health = other.GetComponent<AHealth>();
            if (health != null)
            {
                health.TakeDamage();
            }
        }
    }
}
