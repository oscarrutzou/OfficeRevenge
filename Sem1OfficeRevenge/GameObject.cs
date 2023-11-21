using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public abstract class GameObject
    {
        public Texture2D texture;
        public float layerDepth;
        public Vector2 position;
        public float rotation;
        public float scale;
        public float speed;
        public Vector2 direction;
        public bool isRemoved;

        public Rectangle collisionBox;

        public abstract void Update();

        public virtual void Draw()
        {
            
        }

        public virtual void OnCollisionBox() { } //This don't need to have anything in it, in this GameObject script

        public virtual void RotateTowardsTarget(Vector2 target)
        {

        }

    }
}
