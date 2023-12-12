using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    internal class Rifle : Weapon
    {
        public Rifle()
        {
            magSize = 30;
            magFull = magSize;
            ammo = magSize;
            bulletDmg = 30;
            reloadTime = 1;
        }

        protected override void MakeBullets()
        {
            List<Bullet> bullets = new List<Bullet>();
            Bullet bullet = new Bullet(bulletSpeed, bulletDmg, Global.player.rotation);
            bullets.Add(bullet);
            Global.currentScene.Instantiate(bullet);
            GlobalSound.PlaySound(SoundNames.Shot);
            ammo--;
        }
    }
}
