using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pathfinding;

namespace Sem1OfficeRevenge
{
    public class CombatEnemy : GenericEnemy
    {
        public bool isAttacking;

        public CombatEnemy()
        {
            if (texture == null)
            {
                texture = GlobalTextures.textures[TextureNames.GuiButtonBasicBlue];
                scale.X = 0.25f;
            }

        }

        private void Attack()
        {
            
        }

        public override void Update() 
        {
            if (isAttacking) { Attack(); }
            
            


        }
    }
}
