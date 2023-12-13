using System;
using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    internal class Pistol : Weapon
    {

        public Pistol()
        {
            magSize = 15;
            magFull = magSize;
            ammo = magSize;
            bulletDmg = 20;
            reloadTime = 1;
        }

        protected override void MakeBullets()
        {
            List<Bullet> bullets = new List<Bullet>();
            Bullet bullet = new Bullet(bulletSpeed, bulletDmg, Global.player.rotation);
            bullets.Add(bullet);
            Global.currentScene.Instantiate(bullet);
            GlobalSounds.PlaySound(SoundNames.Shot);
            ammo--;
        }
    }
}
