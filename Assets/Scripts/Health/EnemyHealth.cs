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
            EnemyController enemy = GetComponent<EnemyController>();
            PlayerController character = GetComponent<PlayerController>();
            if(enemy == null)
            {
                character.Kill();
            }
            else
            {
                enemy.Kill();
            }
        }
    }
}
