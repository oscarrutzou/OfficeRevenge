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
        public Animation animation;
        public float layerDepth; //How object gets drawn
        public Vector2 position;
        public float rotation;
        public Vector2 scale = new Vector2(1,1);
        public float speed;
        public Vector2 direction;
        public bool isRemoved;
        public Color color = Color.White;
        public bool UseCenterOrigin;

        private float timer;
        public float frameDuration = 0.05f; // 20 fps

        public Rectangle collisionBox
        {
            get
            {
                // Try to get the width and height of the texture or the current frame of the animation.
                Texture2D drawTexture = texture ?? animation?.frames[animation.CurrentFrame];
                if (drawTexture == null)
                {
                    throw new InvalidOperationException("GameObject must have a valid texture or animation.");
                }

                int width = drawTexture.Width;
                int height = drawTexture.Height;

                Vector2 origin = UseCenterOrigin ? new Vector2(width / 2, height / 2) : Vector2.Zero;

                return new Rectangle(
                    (int)(position.X - origin.X * scale.X),
                    (int)(position.Y - origin.Y * scale.Y),
                    (int)(width * scale.X),
                    (int)(height * scale.Y)
                );
            }
        }

        public virtual void Update()
        {
            if (animation != null)
            {
                timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > frameDuration)
                {
                    timer -= frameDuration;
                    animation.CurrentFrame = (animation.CurrentFrame + 1) % animation.frames.Count;
                }
            }
        }

        public virtual void Draw()
        {
            Texture2D drawTexture = texture ?? animation?.frames[animation.CurrentFrame];
            Vector2 origin = UseCenterOrigin ? new Vector2(drawTexture.Width / 2, drawTexture.Height / 2) : Vector2.Zero;

            if (animation != null)
            {
                // Draw animation
                Global.spriteBatch.Draw(drawTexture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
            }
            else if (texture != null)
            {
                // Draw static texture
                Global.spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, layerDepth);
            }
            DrawDebugCollisionBox();
        }

        private void DrawDebugCollisionBox()
        {
            // Draw debug collision box
            Texture2D pixel = new Texture2D(Global.graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            Rectangle collisionBox = this.collisionBox;

            Global.spriteBatch.Draw(pixel, new Rectangle(collisionBox.Left, collisionBox.Top, collisionBox.Width, 1), Color.Red); // Top
            Global.spriteBatch.Draw(pixel, new Rectangle(collisionBox.Left, collisionBox.Bottom, collisionBox.Width, 1), Color.Red); // Bottom
            Global.spriteBatch.Draw(pixel, new Rectangle(collisionBox.Left, collisionBox.Top, 1, collisionBox.Height), Color.Red); // Left
            Global.spriteBatch.Draw(pixel, new Rectangle(collisionBox.Right, collisionBox.Top, 1, collisionBox.Height), Color.Red); // Right
        }

        public virtual void OnCollisionBox() { } //This don't need to have anything in it, in this GameObject script

        public virtual void RotateTowardsTarget(Vector2 target)
        {

        }

        public void SetObjectAnimation(AnimNames animationName)
        {
            animation = GlobalAnimations.SetObjAnimation(animationName);
        }


        //public virtual void Draw()
        //{
        //    //Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
        //    //Global.spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, layerDepth);

        //    Global.spriteBatch.Draw(texture, position, null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
        //}

    }
}
