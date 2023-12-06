using Microsoft.Xna.Framework;
using Sem1OfficeRevenge.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    internal class Weapon : GameObject
    {
        public int dmg;        
        public int magSize; // standart size 5
        public int magFull;
        public static int bulletSpeed = 2000;
        public static int bulletDmg = 10;
        public List<Bullet> bullets;
        public bool reloading;
        protected float reloadTime;
        protected static int ammo;
        protected float cooldown;
        public Weapon()
        {
            reloading = false;
            dmg = bulletDmg;
            magSize = 5;
        }

        public static void Fire()
        {
            ammo--;
            List<Bullet> bullets = new List<Bullet>();
            Bullet bullet = new Bullet(new Vector2(0, 50), bulletSpeed, bulletDmg);
            bullets.Add(bullet);
            GlobalSound.sounds[SoundNames.Shot].Play();
            Global.currentScene.Instantiate(bullet);

        }

        

        public override void Update()
        {
            
        }

        public virtual void Reload()
        {
            if (reloading || (magSize == magFull)) return;
            reloadTime = cooldown;
            reloading = true;
            magSize = magFull;            
        }
        
    }
}
