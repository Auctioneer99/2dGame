using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Health
{
    public class EnemyHealth : AHealth
    {
        public override void TakeDamage()
        {
            Destroy(gameObject);
        }
    }
}
