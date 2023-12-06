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

        public Shotgun()
        {
            magSizeFactor = 1;
            ammo = this.magSize * magSizeFactor;
            dmg = dmg * 1;
        }

        private void Shooting()
        {

        }
    }
}
