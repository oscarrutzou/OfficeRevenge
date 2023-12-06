using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public int health {  get; private set; }
        public bool canMove {  get; private set; }
        public bool alive;
        public float playerSpeed = 10f;
        private bool hasAttacked;
        int bulletSpeed = 2000;
        int bulletDmg = 10;
        public List<Bullet> bullets = new List<Bullet>();

        public Player()
        {
            health = 100;
            //scale = new Vector2(0.5f, 0.5f);
            centerOrigin = true;
            Global.player = this;

            position = Vector2.Zero;
            //position = Global.world.playerCamera.origin;
            SetObjectAnimation(AnimNames.PlayerRifleIdle);
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Player);
        }


        public override void Update()
        {
            if (Global.currentScene.isPaused) return;

            CheckCollisionBox();

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
            //GlobalSound.sounds[SoundNames.Shot].Play();
            //GlobalSound.PlaySound(GlobalSound.sounds[SoundNames.Shot]);
            Global.currentScene.Instantiate(bullet);
            
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void DamagePlayer(int dmgAmount)
        {
            health -= dmgAmount;
            if (health <= 0)
            {
                health = 0;
                Global.world.ChangeScene(Scenes.EndMenu);
            }
        }


    }
}
