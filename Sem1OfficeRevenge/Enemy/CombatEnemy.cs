using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Sem1OfficeRevenge
{
    public class CombatEnemy : GenericEnemy
    {
        public bool isAttacking;

        public CombatEnemy()
        {
                SetObjectAnimation(AnimNames.PlayerRifleReload);


        }

        private void Attack()
        {
            
        }

        public override void Update() 
        {
            base.Update();
            if (isAttacking) { Attack(); }
            
            


        }
    }
}
