using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sem1OfficeRevenge
{
    public class Player : GameObject
    {
        private int health;
        public bool alive;
        public Vector2 origin;
        public float playerSpeed = 10f;
        private bool hasAttacked;
        

        public Player()
        {
            CenterOrigin = true;
            position.X = Global.graphics.PreferredBackBufferWidth/2;
            position.Y = Global.graphics.PreferredBackBufferHeight/2;
            SetObjectAnimation(AnimNames.PlayerRifleMove);
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Player);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        private void Movement()
        {

        }
    }
}
