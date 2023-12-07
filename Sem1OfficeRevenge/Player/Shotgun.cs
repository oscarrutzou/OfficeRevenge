using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    internal class Shotgun : Weapon
    {
        private int magSizeFactor; // magsize 5 so factor i 1
        Random random1 = new Random();
        private int spary;
        private int plusOrMinus;
        
        public Shotgun()
        {
            magSizeFactor = 1;
            ammo = this.magSize * magSizeFactor;
            dmg = dmg * 1;
        }

        protected override void MakeBullets()
        {
            List<Bullet> bullets = new List<Bullet>();

            for (int i = 0; i < 6; i++)
            {
                float pi = random1.Next(((int)((float)(Math.PI * 100 / 12))));
                plusOrMinus = random1.Next(0, 2);
                Bullet bullet = new Bullet(bulletSpeed, bulletDmg);
                if (plusOrMinus == 0)
                {
                    bullet.rotation = bullet.rotation - pi;
                }
                else if (plusOrMinus == 1)
                {
                    bullet.rotation = bullet.rotation + pi;
                }
                bullets.Add(bullet);
                Global.currentScene.Instantiate(bullet);
            }
        }

        private void Shooting()
        {

        }
    }
}
