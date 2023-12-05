using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sem1OfficeRevenge.Enemy;

namespace Sem1OfficeRevenge
{
    public class GenericEnemy : GameObject
    {
        public bool dead;
        private Blood blood;
        protected List<ShoePrint> shoePrints = new List<ShoePrint>();
        protected Vector2 oldPos;
        protected int bloodied = 0;
        protected bool right = true;
        protected Random rnd = new Random();



        public GenericEnemy()
        {
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Enemies);
        }
        
        public void Die()
        {
            blood = new Blood(position);
            Global.currentScene.Instantiate(blood);
            dead = true;
            ScoreManager.killCount++;
            animation.frameRate = 0;
            //color = Color.DarkRed;

        }
        
    }
}
