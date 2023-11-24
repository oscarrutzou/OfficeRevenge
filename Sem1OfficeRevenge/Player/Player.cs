﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sem1OfficeRevenge
{
    internal class Player : GameObject
    {
        private int health;
        public bool alive;
        public Vector2 origin;
        
        private bool hasAttacked;
        

        public Player(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            SetPlayerAnimation(AnimNames.PlayerRifleMove);
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
