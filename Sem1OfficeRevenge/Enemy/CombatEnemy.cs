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
                SetObjectAnimation(AnimNames.PlayerRifleIdle);


        }

        private void Attack()
        {
            if (dead) return; 
            


        }

        public override void Update() 
        {
            if (Global.currentScene.isPaused || dead) return;

            if (Math.Abs(Global.player.position.X - position.X) < 5 && Math.Abs(Global.player.position.Y - position.Y) < 5 && isAttacking == false)
            {
                isAttacking = true;

            }
            else if (Math.Abs(Global.player.position.X - position.X) > 7 && Math.Abs(Global.player.position.Y - position.Y) > 7)
            {
                isAttacking = false;
            }
            
            


        }
    }
}
