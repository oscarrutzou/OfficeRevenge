using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Office_Revenge
{
    internal abstract class GameObject
    {
        public Texture2D texture;
        public float layerDepth;
        public Vector2 position;
        public float rotation;
        public float scale;
        public float speed;
        public Vector2 direction;
        public Rectangle collisionBox;
        public bool isRemoved;

        public abstract void Update();

        public virtual void Draw()
        {
            
        }

        public virtual void OnCollisionBox()
        {

        }

        public virtual void RotateTowardsTarget(Vector2 target)
        {

        }

    }
}
