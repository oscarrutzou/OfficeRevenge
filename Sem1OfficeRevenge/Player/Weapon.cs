﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    internal class Weapon : GameObject
    {
        public int dmg;
        public Vector2 shootDirection;
        public float shootVelocity;
        public int magSize;
        public bool isMagFull;

        public override void Update()
        {
            
        }

        private void Reload()
        {

        }
        
    }
}