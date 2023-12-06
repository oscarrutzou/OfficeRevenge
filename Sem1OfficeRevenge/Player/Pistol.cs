using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    internal class Pistol : Weapon
    {
        private int magSizeFactor; // magsize 15 so factor is 3

        public Pistol()
        {
            magSizeFactor = 3;
            ammo = this.magSize * magSizeFactor;
            dmg = dmg * 1;
        }

        private void Shooting()
        {

        }
    }
}
