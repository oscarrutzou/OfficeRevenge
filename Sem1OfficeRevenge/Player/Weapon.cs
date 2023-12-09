using Microsoft.Xna.Framework;
using Sem1OfficeRevenge.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public abstract class Weapon : GameObject
    {
        public int dmg;        
        public int magSize; // standart size 5
        public int magFull;
        public static int bulletSpeed = 200;
        public static int bulletDmg = 10;
        public List<Bullet> bullets;
        public bool reloading;
        protected float reloadTime;
        protected static int ammo;
        protected float cooldown;
        public Weapon()
        {
            cooldown = 0;
            reloading = false;
            dmg = bulletDmg;
            magSize = 5;
        }

        public virtual void Fire()
        {
            if (cooldown > 0 || reloading) return;
            
            ammo--;
            if (ammo > 0)
            {
                MakeBullets();
                GlobalSound.sounds[SoundNames.Shot].Play();
                
            }
            else
            {
                Reload();
            }

        }

        protected abstract void MakeBullets();

        public override void Update()
        {
            base.Update();
            

            if (cooldown > 0)
            {
                cooldown -= (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (reloading)
            {
                reloading = false;
            }
            
        }

        public virtual void Reload()
        {
            if (reloading || (ammo == magFull)) return;
            cooldown = reloadTime;
            reloading = true;
            ammo = magFull;            
        }
        
    }
}
