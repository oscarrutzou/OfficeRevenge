using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class Bullet : GameObject
    {
        public float lifespan { get; private set; }
        public float TotalSeconds;
        public Bullet()
        {
            texture = GlobalTextures.textures[TextureNames.Bullet];
            centerOrigin = true;
            SetCorrectBulletPosition();
            rotation = Global.player.rotation;
            direction = new((float)Math.Cos(Global.player.rotation), (float)Math.Sin(Global.player.rotation));
            speed = 100;
            lifespan = 2;
        }

        public Bullet(BulletData data)
        {
            texture = GlobalTextures.textures[TextureNames.Bullet];
            speed = 100;
            rotation = data.rotation;
            direction = new((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            lifespan = 2;
            position = data.position;
        }


        public override void Update()
        {
            TotalSeconds = (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            position += direction * speed * TotalSeconds;
        }

        public Vector2 SetCorrectBulletPosition()
        {
            Texture2D playerTexture = Global.player.animation.frames[Global.player.animation.currentFrame];
            // Offset from the center of the tower to the right side of the tower sprite
            Vector2 offset = new Vector2(playerTexture.Width / 2, 0);

            // Add half the height of the projectile sprite to the offset
            offset.X += texture.Height / 2; //Should use the width when it has a proper texture that faces right

            // Rotate the offset by the same amount as the tower
            float cos = (float)Math.Cos(Global.player.rotation);
            float sin = (float)Math.Sin(Global.player.rotation);
            Vector2 rotatedOffset = new Vector2(
                offset.X * cos - offset.Y * sin,
                offset.X * sin + offset.Y * cos);

            // Add the rotated offset to the tower's position
            position = Global.player.position + rotatedOffset;

            return position;
        }

        public override void OnCollisionBox()
        {
            foreach (Bullet bullet in Global.currentSceneData.bullets)
            {
                if (Intersects(bullet))
                {
                    bullet.isRemoved = true;
                }
            }
        }
    }
}
