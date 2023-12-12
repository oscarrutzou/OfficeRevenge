using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sem1OfficeRevenge
{
    public class Bullet : GameObject
    {
        public float lifespan { get; private set; } //Brug timeren i stedet for til at finde at lave en life time.
        private int bulletDmg;
        public float totalSecondsTimer;
   

        public Bullet(int speed, int bulletDmg, float rotation)
        {
            texture = GlobalTextures.textures[TextureNames.Bullet];
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Bullets);
            scale = new Vector2(0.05f, 0.05f);

            centerOrigin = true;
            SetCorrectBulletPositionWithOffset();
            this.rotation = rotation;
            direction = new((float)Math.Cos(Global.player.rotation), (float)Math.Sin(Global.player.rotation));
            this.speed = speed;
            this.bulletDmg = bulletDmg;

            lifespan = 2;
        }

        public override void Update()
        {
            if (isRemoved || Global.currentScene.isPaused) return;
            totalSecondsTimer = (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            position += direction * speed * totalSecondsTimer;

            bool isInsideRoom = false;

            foreach (Room room in Global.currentSceneData.rooms)
            {
                if (Collision.ContainsEitherBox(this, room.collisionBox, room.hallwayCol))
                {
                    isInsideRoom = true;
                    break;
                }
            }

            if (!isInsideRoom)
            {
                this.isRemoved = true;
            }

            CheckCollisionBox();
            
        }

        public override void CheckCollisionBox()
        {
            foreach (GenericEnemy enemy in Global.currentSceneData.enemies) //Ændre til at kigge i enemies i currentSceneData
            {
                if (Collision.IntersectBox(this, enemy) && !enemy.dead)
                {
                    isRemoved = true;
                    
                    enemy.Die();
                    
                    //Spil hit lyd måske?
                }
            }
            //Efter lav det samme foreach bare med en med væggene. 
        }

        public Vector2 SetCorrectBulletPositionWithOffset()
        {
            Texture2D playerTexture = Global.player.animation.frames[Global.player.animation.currentFrame];
            // Offset from the center of the tower to the right side of the tower sprite
            Vector2 offset = new Vector2(playerTexture.Width / 2, 0) + new Vector2(0, Global.player.textureOffset);

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
