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

        private void Shooting()
        {

        }
    }
}
