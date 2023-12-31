﻿using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    public abstract class Weapon : GameObject
    {
        public int dmg;        
        public int magSize; // standart size 5
        public int magFull;
        public int bulletSpeed = 1000;
        public int bulletDmg;
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
            
            magSize = 5;
        }

        public virtual void Fire()
        {
            //Check if the gun is on cooldown or reloading
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
                    //Change animation to shoot
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
