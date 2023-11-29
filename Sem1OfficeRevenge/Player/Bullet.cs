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
        public float lifespan { get; private set; } //Brug timeren i stedet for til at finde at lave en life time.
        private int bulletDmg;
        public float totalSecondsTimer;
   

        public Bullet(Vector2 offSet, int speed, int bulletDmg)
        {
            texture = GlobalTextures.textures[TextureNames.Bullet];
            layerDepth = Global.currentScene.GetObjectLayerDepth(LayerDepth.Enemies);
            scale = new Vector2(0.2f, 0.2f);

            centerOrigin = true;
            SetCorrectBulletPositionWithOffset(offSet);
            rotation = Global.player.rotation;
            direction = new((float)Math.Cos(Global.player.rotation), (float)Math.Sin(Global.player.rotation));
            this.speed = speed;
            this.bulletDmg = bulletDmg;

            lifespan = 2;
        }

        public override void Update()
        {
            if (isRemoved) return;
            totalSecondsTimer = (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            position += direction * speed * totalSecondsTimer;

            CheckCollisionBox();
        }

        public override void CheckCollisionBox()
        {
            foreach (GameObject test in Global.currentSceneData.defults) //Ændre til at kigge i enemies i currentSceneData
            {
                if (test is TestObjectCollide) //Fjern dette if, da vi allerede kigger igennem kun enemies. 
                {
                    if (Collision.IsCollidingBox(this, test))
                    {
                        isRemoved = true; //Fjerner bullet
                        test.isRemoved = true; //Her skal den dmg enemy med variablet "bulletDmg"
                        //Spil hit lyd måske?
                    }
                }
            }

            //Efter lav det samme foreach bare med en med væggene. 
        }

        public Vector2 SetCorrectBulletPositionWithOffset(Vector2 playerGunOffset)
        {
            Texture2D playerTexture = Global.player.animation.frames[Global.player.animation.currentFrame];
            // Offset from the center of the tower to the right side of the tower sprite
            Vector2 offset = new Vector2(playerTexture.Width / 2, 0) + playerGunOffset;

            // Add half the height of the projectile sprite to the offset
            offset.X += (texture.Height * scale.Y) / 2; //Should use the width when it has a proper texture that faces right

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
    }
}
