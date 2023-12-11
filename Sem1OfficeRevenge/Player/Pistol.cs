using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    internal class Pistol : Weapon
    {

        public Pistol()
        {
            magSize = 15;
            magFull = magSize;
            ammo = magSize;
            dmg = dmg * 1;
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
