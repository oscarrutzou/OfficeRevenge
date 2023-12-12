using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    internal class Shotgun : Weapon
    {
        Random random1 = new Random();
        

        public Shotgun()
        {
            magSize = 5;
            magFull = magSize;
            ammo = magSize;
            bulletDmg = 10;
            reloadTime = 2;
            
        }

        protected override void MakeBullets()
        {
            List<Bullet> bullets = new List<Bullet>();
            float spread = MathHelper.ToRadians(45f); // Spread angle in degrees. Adjust as needed.
            
            if (pumpTime <= 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    float bulletRotation = Global.player.rotation;
                    float offset = ((float)random1.NextDouble() - 0.5f) * spread;
                    bulletRotation += offset;

                    Vector2 direction = new Vector2((float)Math.Cos(bulletRotation), (float)Math.Sin(bulletRotation));
                    Bullet bullet = new Bullet(bulletSpeed, bulletDmg, Global.player.rotation);
                    bullet.texture = GlobalTextures.textures[TextureNames.Pellet];
                    bullet.direction = direction;

                    bullets.Add(bullet);
                    Global.currentScene.Instantiate(bullet);
                }
                GlobalSound.PlaySound(SoundNames.Shotgun);
                pumpTime = 1;
                ammo--;
            }
        }
    }
}
