using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sem1OfficeRevenge
{
    public class Player : GameObject
    {
        private int health;
        public bool alive;
        public Vector2 origin;
        public float playerSpeed = 10f;
        private bool hasAttacked;

        int bulletSpeed = 300;
        int bulletDmg = 10;


        public Player(Vector2 position)
        {
            
            this.position = position;
            SetObjectAnimation(AnimNames.PlayerRifleMove);
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Player);
        }

        public override void Update()
        {
            if (InputManager.mouseClicked)
            {
                Fire();
            }
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        private void Movement()
        {

        }

        private void Fire()
        {
            //BulletData bd = new();
            //{
            //    bd.position = position;
            //    bd.rotation = rotation;

            //};
            //Bullet bullet = new Bullet(bd);
            Bullet bullet = new Bullet(new Vector2(0, 50), bulletSpeed, bulletDmg);

            Global.currentScene.Instantiate(bullet);
        }
        
    }
}
