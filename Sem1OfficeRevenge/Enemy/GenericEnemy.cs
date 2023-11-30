using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sem1OfficeRevenge
{
    public class GenericEnemy : GameObject
    {
        private int health;
        public bool dead;
        



        public GenericEnemy()
        {
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Enemies);
        }
        
        public void Die()
        {
            dead = true;
            ScoreManager.killCount++;
            animation.frameRate = 0;
            color = Color.DarkRed;
        }
    }
}
