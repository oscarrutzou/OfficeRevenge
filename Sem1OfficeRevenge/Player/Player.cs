using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sem1OfficeRevenge.World;
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
        int bulletSpeed = 2000;
        int bulletDmg = 10;
        public List<Bullet> bullets = new List<Bullet>();

        public Player()
        {
            centerOrigin = true;
            Global.player = this;
            position = Global.world.playerCamera.position;
            SetObjectAnimation(AnimNames.PlayerRifleIdle);
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Player);


        }


        public override void Update()
        {
            if (Global.currentScene.isPaused) return;

            if (InputManager.anyMoveKeyPressed && InputManager.mouseClicked)
            {
                Fire();
                AnimRunNShoot();
            }
            else if (InputManager.anyMoveKeyPressed)
            {
                AnimMove();
            } 
            else if (InputManager.mouseClicked)
            {
                Fire();
                AnimShoot();
            }            
        }

        private void AnimRunNShoot()
        {
            SetObjectAnimation(AnimNames.PlayerRifleShoot);
            animation.onAnimationDone += () => { SetObjectAnimation(AnimNames.PlayerRifleIdle); };
        }

        private void AnimMove()
        {
            if (animation.animationName == AnimNames.PlayerRifleShoot) return; // So it shows the shoot animation
            
            SetObjectAnimation(AnimNames.PlayerRifleMove);
            animation.onAnimationDone += () => { SetObjectAnimation(AnimNames.PlayerRifleIdle); };
        }

        private void AnimShoot()
        {
            SetObjectAnimation(AnimNames.PlayerRifleShoot);
            animation.onAnimationDone += () => { SetObjectAnimation(AnimNames.PlayerRifleIdle); };
        }


        private void Fire()
        {
            Bullet bullet = new Bullet(new Vector2(0, 50), bulletSpeed, bulletDmg);
            bullets.Add(bullet);
            GlobalSound.sounds[SoundNames.Shot].Play();
            Global.currentScene.Instantiate(bullet);
            
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
