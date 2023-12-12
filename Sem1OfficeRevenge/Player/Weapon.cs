using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public abstract class Weapon : GameObject
    {
        public int dmg;        
        public int magSize; // standart size 5
        public int magFull;
        public static int bulletSpeed = 1000;
        public static int bulletDmg = 10;
        public List<Bullet> bullets;
        public bool reloading;
        protected float reloadTime;
        public int ammo;
        public float cooldown;
        public float pumpTime;

        private bool hasPlayedReloadAnim;
        public Weapon()
        {
            cooldown = 0;
            reloading = false;
            dmg = bulletDmg;
            magSize = 5;
        }

        public virtual void Fire()
        {
            if (cooldown > 0 || reloading) return;
            
            
            if (ammo > 0)
            {
                MakeBullets();
                
            }
            
        }

        protected abstract void MakeBullets();

        public override void Update()
        {
            base.Update();
            pumpTime -= (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            if (ammo <= 0) Reload();

            if (cooldown > 0)
            {
                cooldown -= (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
                
                if (this is Shotgun)
                {
                    Global.player.AnimReload();
                    if (Global.player.animation == Global.player.reloadAnim) Global.player.animation.frameRate = 40;

                }
            }
            else if (reloading)
            {
                reloading = false;
                ammo = magFull; //Refill ammo
            }


        }

        public void RefreshGunAfterRun()
        {
            ammo = magSize;
            reloading = false;
            cooldown = 0;
        }

        public virtual void Reload()
        {
            if (reloading || (ammo == magFull)) return;
            cooldown = reloadTime;
            reloading = true;
            ammo = 0;
            Global.player.AnimReload();
        }

    }
}
