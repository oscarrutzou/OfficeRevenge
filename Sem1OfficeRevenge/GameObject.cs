using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using System;

namespace Sem1OfficeRevenge
{
    public abstract class GameObject
    {
        public Vector2 position;
        public Vector2 origin;
        internal float rotation;
        internal float rotationVelocity = 3f;
        public Vector2 scale = new Vector2(1,1);
        public float speed;
        public Vector2 direction;
        public SpriteEffects spriteEffects = SpriteEffects.None;

        public Texture2D texture;
        
        public bool centerOrigin;
        public float layerDepth; //How object gets drawn
        public Color color = Color.White;

        public Animation animation;

        private int collisionBoxWidth;
        private int collisionBoxHeight;
        private Vector2 offset;
        
        public bool isRemoved;
        public virtual bool isVisible { get; set; }

        public Rectangle collisionBox
        {
            get
            {
                // Try to get the width and height of the texture or the current frame of the animation.
                Texture2D drawTexture = texture ?? animation?.frames[animation.currentFrame];
                if (drawTexture is null)
                {
                    throw new InvalidOperationException("GameObject must have a valid texture or animation.");
                }

                int width = collisionBoxWidth > 0 ? collisionBoxWidth : drawTexture.Width;
                int height = collisionBoxHeight > 0 ? collisionBoxHeight : drawTexture.Height;

                origin = centerOrigin ? new Vector2(width / 2, height / 2) : Vector2.Zero;

                return new Rectangle(
                    (int)(position.X + offset.X - origin.X * scale.X),
                    (int)(position.Y + offset.Y - origin.Y * scale.Y),
                    (int)(width * scale.X),
                    (int)(height * scale.Y)
                );
            }

            set {  }
        }

        public GameObject()
        {
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Background);
            isVisible = true;
        }

        public virtual void Update(){}

        

        public virtual void Draw()
        {
            if (!isVisible) return;
            Texture2D drawTexture = texture ?? animation?.frames[animation.currentFrame];

            //If the bool is true, choose the option on the left, if not then it chooses the right
            origin = centerOrigin ? new Vector2(drawTexture.Width / 2, drawTexture.Height / 2) : Vector2.Zero;

            if (animation != null && texture != null)
            {
                throw new Exception("You should only have either a animation or texture loaded. Set the other variable to null");
            }

            if (animation != null)
            {
                // Draw animation
                Global.spriteBatch.Draw(drawTexture, position, null, color, rotation, origin, scale, spriteEffects, layerDepth);
            }
            else if (texture != null)
            {
                // Draw static texture
                Global.spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, spriteEffects, layerDepth);
            }
        }

        public void SetObjectAnimation(Animation animation)
        {
            // Check if the current animation is already the one we want to set
            if (this.animation != null && this.animation == animation) return;
            this.animation = animation;
        }

        public void SetObjectAnimation(AnimNames animationName)
        {
            // Check if the current animation is already the one we want to set
            if (animation != null && animation.animationName == animationName) return;

            // If it's not, we create a new animation
            animation = GlobalAnimations.SetAnimation(animationName);
        }

        #region CollsionBox
        public void SetCollisionBox(int width, int height)
        {
            collisionBoxWidth = width;
            collisionBoxHeight = height;
        }

        public void SetCollisionBox(int width, int height, Vector2 offset)
        {
            collisionBoxWidth = width;
            collisionBoxHeight = height;
            this.offset = offset;
        }

        public virtual void CheckCollisionBox() { } // This don't need to have anything in it, in this GameObject script
        public void RotateTowardsTarget(Vector2 target)
        {
            if (position == target) return;

            Vector2 dir = target - position;
            rotation = (float)Math.Atan2(-dir.Y, -dir.X) + MathHelper.Pi;
        }
        public void RotateTowardsTargetWithOffset(Vector2 target, Vector2 offset)
        {
            // Add the offset to the target
            Vector2 targetWithOffset = target + offset;

            if (position == targetWithOffset) return;

            Vector2 dir = targetWithOffset - position;
            rotation = (float)Math.Atan2(-dir.Y, -dir.X) + MathHelper.Pi;
        }

        public virtual void LerpTowardsTarget(float target, float origin, float timer, float speed)
        {
            if (rotation == target) return;
            rotation = MathHelper.Lerp(origin, target, timer * speed);

        }

        public float ShortestRotation(float targetRotation, float currentRotation)
        {
            float delta = MathHelper.WrapAngle(targetRotation - currentRotation);

            if (delta > MathHelper.Pi)
            {
                delta -= MathHelper.TwoPi;
            }
            else if (delta < -MathHelper.Pi)
            {
                delta += MathHelper.TwoPi;
            }

            return currentRotation + delta;
        }
        #endregion

        #region DebugCollsionBox
        internal void DrawDebugCollisionBox()
        {
            // Draw debug collision box
            Texture2D pixel = new Texture2D(Global.graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            // Get the corners of the rectangle
            Vector2[] corners = new Vector2[4];
            corners[0] = new Vector2(collisionBox.Left, collisionBox.Top);
            corners[1] = new Vector2(collisionBox.Right, collisionBox.Top);
            corners[2] = new Vector2(collisionBox.Right, collisionBox.Bottom);
            corners[3] = new Vector2(collisionBox.Left, collisionBox.Bottom);

            // Rotate the corners around the center of the rectangle
            Vector2 origin = new Vector2(collisionBox.Center.X, collisionBox.Center.Y);
            for (int i = 0; i < 4; i++)
            {
                Vector2 dir = corners[i] - origin;
                //dir = Vector2.Transform(dir, Matrix.CreateRotationZ(rotation));
                corners[i] = dir + origin;
            }

            // Draw the rotated rectangle
            for (int i = 0; i < 4; i++)
            {
                Vector2 start = corners[i];
                Vector2 end = corners[(i + 1) % 4];
                DrawLine(pixel, start, end, Color.Red);
            }
        }

        internal void DrawDebugCollisionBox(Rectangle recBox)
        {
            // Draw debug collision box
            Texture2D pixel = new Texture2D(Global.graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            // Get the corners of the rectangle
            Vector2[] corners = new Vector2[4];
            corners[0] = new Vector2(recBox.Left, recBox.Top);
            corners[1] = new Vector2(recBox.Right, recBox.Top);
            corners[2] = new Vector2(recBox.Right, recBox.Bottom);
            corners[3] = new Vector2(recBox.Left, recBox.Bottom);

            // Rotate the corners around the center of the rectangle
            Vector2 origin = new Vector2(recBox.Center.X, recBox.Center.Y);
            for (int i = 0; i < 4; i++)
            {
                Vector2 dir = corners[i] - origin;
               // dir = Vector2.Transform(dir, Matrix.CreateRotationZ(rotation));
                corners[i] = dir + origin;
            }

            // Draw the rotated rectangle
            for (int i = 0; i < 4; i++)
            {
                Vector2 start = corners[i];
                Vector2 end = corners[(i + 1) % 4];
                DrawLine(pixel, start, end, Color.Red);
            }
        }

        private void DrawLine(Texture2D pixel, Vector2 start, Vector2 end, Color color)
        {
            float length = Vector2.Distance(start, end);
            float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            Global.spriteBatch.Draw(pixel, start, null, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, Global.currentScene.GetObjectLayerDepth(LayerDepth.FullOverlay));
        }
        #endregion

        
    }
}
