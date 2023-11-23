using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    internal class Player : GameObject
    {
        private int health;
        public bool alive;
        public Vector2 origin;
        public float rotationVelocity = 3f;
        private bool hasAttacked;
        

        public Player(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public override void Update()
        {
            
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
