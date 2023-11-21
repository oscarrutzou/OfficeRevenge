using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    internal class CombatEnemy : GenericEnemy 
    {
        public bool isAttacking;

        public new void EnemyUpdate()
        {
            if (isAttacking) { Attack(); }
        }

        private void Attack()
        {
            
        }
    }
}
