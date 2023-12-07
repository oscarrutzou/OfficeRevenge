using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    internal class Rifle : Weapon
    {
        private int magSizeFactor; // magsize 30 so factor is 6

        public Rifle()
        {
            magSizeFactor = 6;
            ammo = this.magSize * magSizeFactor;
            dmg = dmg * 2;
        }

        protected override void MakeBullets()
        {
            List<Bullet> bullets = new List<Bullet>();
            Bullet bullet = new Bullet(bulletSpeed, bulletDmg);
            bullets.Add(bullet);
            Global.currentScene.Instantiate(bullet);
        }

        private void Shooting()
        {

        }
    }
}
