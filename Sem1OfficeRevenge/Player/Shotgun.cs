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
            magFull = this.magSize * magSizeFactor;
            dmg = dmg * 1;
            reloadTime = 5;
        }

        protected override void MakeBullets()
        {
            List<Bullet> bullets = new List<Bullet>();
            
            for (int i = 0; i < 6; i++)
            {
                float bulletRotation = Global.player.rotation;
                float pi = random1.Next(((int)((float)(Math.PI * 100 / 12))));
                plusOrMinus = random1.Next(0, 4);
                if (plusOrMinus == 0 || plusOrMinus == 2)
                {
                    bulletRotation -= ((float)pi);
                }
                else if (plusOrMinus == 1 || plusOrMinus == 3)
                {
                    bulletRotation += ((float)pi);
                }
                Bullet bullet = new Bullet(bulletSpeed, bulletDmg, bulletRotation);
                bullets.Add(bullet);
                Global.currentScene.Instantiate(bullet);
            }
        }

        private void Shooting()
        {

        }
    }
}
