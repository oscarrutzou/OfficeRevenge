using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using System;

namespace Sem1OfficeRevenge
{
    public abstract class GameObject
    {
        public Texture2D texture;
        public float layerDepth; //How object gets drawn
        public Vector2 position;
        public float rotation;
        public Vector2 scale = new Vector2(1, 1);
        public float speed;
        public Vector2 direction;
        public bool isRemoved;
        public Color color = Color.White;
        public Rectangle rect;
        public Rectangle collisionBox
        {
            get
            {
                return new Rectangle(
                (int)(position.X - texture.Width / 2 * scale.X),
                    (int)(position.Y - texture.Height / 2 * scale.Y),
                    (int)(texture.Width * scale.X),
                    (int)(texture.Height * scale.Y)
                    );
            }

            set {  }
        }

        public abstract void Update();

        public virtual void Draw()
        {
            //Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            //Global.spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, layerDepth);

            Global.spriteBatch.Draw(texture, position, null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
        }

        public virtual void OnCollisionBox() { } //This don't need to have anything in it, in this GameObject script

        public virtual void RotateTowardsTarget(Vector2 target)
        {

        }

    }
}
