﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class Player : GameObject
    {
        public int health {  get; private set; }
        public bool canMove {  get; private set; }
        public int textureOffset { get; private set; }
        public bool alive;
        public float playerSpeed = 10f;
        private bool hasAttacked;
        
        private List<ShoePrint> shoePrints = new List<ShoePrint>();
        private Vector2 oldPos;
        private int bloodied = 0;
        private bool right = true;

        private Texture2D sight;

        private int voiceLineBufferMili = 10000;
        private bool isPlayingVl;
        public SoundNames[] shootVoiceLines = new SoundNames[]
{
            SoundNames.Player1, SoundNames.Player2, SoundNames.Player3, SoundNames.Player4, SoundNames.Player5, SoundNames.Player6, SoundNames.Player7, SoundNames.Player8, SoundNames.Player9, SoundNames.Player10, SoundNames.Player11
        };

        private Animation idleAnim;
        private Animation moveAnim;
        private Animation shootAnim;
        public Animation reloadAnim { get; private set; }

        public Player()
        {
            health = 100;
            textureOffset = 50;
            centerOrigin = true;
            Global.player = this;
            position = Vector2.Zero;
            SetAnimCurrentWeapon();
            animation = idleAnim;
            sight = GlobalTextures.textures[TextureNames.Sight];
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Player);

            SetCollisionBox(110, 110);
        }

        //Set animations based on current weapon
        private void SetAnimCurrentWeapon()
        {
            switch (Global.world.currentWeapon)
            {
                case Pistol:
                    idleAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerHandgunIdle);
                    moveAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerHandgunMove);
                    shootAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerHandgunShoot);
                    reloadAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerHandgunReload);
                    break;
                case Rifle:
                    idleAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerRifleIdle);
                    moveAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerRifleMove);
                    shootAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerRifleShoot);
                    reloadAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerRifleReload);
                    break;
                case Shotgun:
                    idleAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerShotGunIdle);
                    moveAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerShotGunMove);
                    shootAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerShotGunShoot);
                    reloadAnim = GlobalAnimations.SetAnimation(AnimNames.PlayerShotGunReload);
                    break;
            }
        }

        public override void Update()
        {
            CheckCollisionBox();
            if (Global.currentScene.isPaused) return;
            
            CheckCollisionBox();

            Weapon currentWeapon = Global.world.currentWeapon;

            //Set animations based on current weapon
            if (InputManager.anyMoveKeyPressed && InputManager.mouseClicked)
            {
                if (currentWeapon.reloading == false)
                {
                    if (Global.world.currentWeapon is Shotgun shotgun)
                    {
                        if (shotgun.pumpTime <= 0)
                        {
                            //Change animation to shoot
                            AnimRunNShoot();
                            PlayShootVL();
                        }
                        currentWeapon.Fire();
                    }
                    else
                    {
                        //Change animation to shoot
                        currentWeapon.Fire();
                        AnimRunNShoot();
                        PlayShootVL();
                    }                    
                }               
            }
            else if (InputManager.anyMoveKeyPressed)
            {
                AnimMove();
            }
            else if (InputManager.mouseClicked)
            {                               
                if (currentWeapon.reloading == false)
                {
                    if (Global.world.currentWeapon is Shotgun shotgun)
                    {
                        if (shotgun.pumpTime <= 0)
                        {
                            //Change animation to shoot
                            AnimShoot();
                            PlayShootVL();
                        }
                        currentWeapon.Fire();
                    }
                    else
                    {
                        //Change animation to shoot
                        currentWeapon.Fire();
                        AnimShoot();
                        PlayShootVL();
                    }                    
                }               
            }

            //Reload
            if (InputManager.mouseRightClicked)
            {
                currentWeapon.Reload();
            }

            if (WalkedFar(75, position, oldPos) == false)
            {
                if (bloodied > 0)
                {
                    //Add shoe prints
                    shoePrints.Add(new ShoePrint(right, position, rotation));
                    oldPos = position;
                    right = !right;
                    bloodied--;
                }
            }

            foreach (Blood blood in Global.currentSceneData.bloods)
            {
                //Check if player is in blood
                if (Math.Abs(position.X - blood.position.X) < (blood.texture.Width * scale.X) / 2 / 2 && Math.Abs(position.Y - blood.position.Y) < (blood.texture.Height * scale.Y) / 2 / 2)
                {
                    bloodied = 10;
                }
            }
            currentWeapon.Update();
        }

        bool WalkedFar(float range, Vector2 v1, Vector2 v2)
        {
            // Check if the player has moved more than the range
            var dx = v1.X - v2.X;
            var dy = v1.Y - v2.Y;
            return dx * dx + dy * dy < range * range;
        }

        private void AnimRunNShoot()
        {
            //change animation to idle when player stops moving
            SetObjectAnimation(shootAnim);
            animation.onAnimationDone += () => { SetObjectAnimation(idleAnim); };
        }
        private void AnimMove()
        {
            // Don't change animation if the player is already moving
            if (animation == shootAnim || animation == reloadAnim) return; // So it shows the shoot animation
            
            SetObjectAnimation(moveAnim);
            animation.onAnimationDone += () => { SetObjectAnimation(idleAnim); };
        }

        public void AnimReload()
        {
            //Change animation to reload
            if (animation == reloadAnim) return;
            Global.currentScene.Instantiate(new Magazin(Global.player.position, Global.player.rotation));
            SetObjectAnimation(reloadAnim);
            animation.onAnimationDone += () => { SetObjectAnimation(idleAnim); };
        }

        private void AnimShoot()
        {            
            //Change animation to shoot
            SetObjectAnimation(shootAnim);
            animation.onAnimationDone += () => { SetObjectAnimation(idleAnim); };
        }
    
        private async void PlayShootVL()
        {
            //Play a random voice line
            if (isPlayingVl) return;
            if (GlobalSounds.IsAnySoundPlaying(GenericEnemy.deathVoiceLines)) return;

            GlobalSounds.PlayRandomSound(shootVoiceLines, 1);
            isPlayingVl = true;

            await Task.Delay(voiceLineBufferMili);
            isPlayingVl = false;
        }

        public override void Draw()
        {
            Global.spriteBatch.Draw(sight, position, null, Color.White, rotation, new Vector2(sight.Width / 2, sight.Height / 2), scale, spriteEffects, 1);
            //DrawDebugCollisionBox();
            base.Draw();
        }

        public void DamagePlayer(int dmgAmount)
        {
            //Damage the player
            health -= dmgAmount;
            if (health <= 0)
            {
                health = 0;
                Global.world.ChangeScene(Scenes.EndMenu);
            }
        }


    }
}
