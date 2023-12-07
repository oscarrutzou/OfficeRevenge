﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sem1OfficeRevenge.Enemy;
using Sem1OfficeRevenge.World;
using System;
using System.Collections.Generic;
using System.IO;
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
        

        private List<ShoePrint> shoePrints = new List<ShoePrint>();
        private Vector2 oldPos;
        private int bloodied = 0;
        private bool right = true;

        private List<ShoePrint> shoePrints = new List<ShoePrint>();
        private Vector2 oldPos;
        private int bloodied = 0;
        private bool right = true;

        private Texture2D sight;

        public Player()
        {
            health = 100;
            
            centerOrigin = true;
            Global.player = this;

            position = Vector2.Zero;
            //position = Global.world.playerCamera.origin;
            SetObjectAnimation(AnimNames.PlayerRifleIdle);
            sight = GlobalTextures.textures[TextureNames.Sight];
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Player);
        }


        public override void Update()
        {
            CheckCollisionBox();
            if (Global.currentScene.isPaused) return;

            CheckCollisionBox();

            if (InputManager.anyMoveKeyPressed && InputManager.mouseClicked)
            {
                Weapon.Fire();
                AnimRunNShoot();
            }
            else if (InputManager.anyMoveKeyPressed)
            {
                AnimMove();
            } 
            else if (InputManager.mouseClicked)
            {
                Weapon.Fire();
                AnimShoot();
            }

            if (WalkedFar(75, position, oldPos) == false)
            {
                if (bloodied > 0)
                {
                    shoePrints.Add(new ShoePrint(right, position, rotation));
                    oldPos = position;
                    right = !right;
                    bloodied--;
                }

            }
            foreach (Blood blood in Global.currentSceneData.bloods)
            {
                if (Math.Abs(position.X - blood.position.X) < (blood.texture.Width*scale.X)/2/2 && Math.Abs(position.Y - blood.position.Y) < (blood.texture.Height * scale.Y) / 2/2)
                {

                    bloodied = 10;

                }
            }
        }

        bool WalkedFar(float range, Vector2 v1, Vector2 v2)
        {
            var dx = v1.X - v2.X;
            var dy = v1.Y - v2.Y;
            return dx * dx + dy * dy < range * range;
        }

        public override void CheckCollisionBox()
        {
            
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
        

        public override void Draw()
        {
            Global.spriteBatch.Draw(sight, position, null, Color.White, rotation, new Vector2(sight.Width / 2, sight.Height / 2), scale, spriteEffects, 1);

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
