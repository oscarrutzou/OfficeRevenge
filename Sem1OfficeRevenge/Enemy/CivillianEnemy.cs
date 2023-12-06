using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    internal class CivillianEnemy : GenericEnemy
    {
        public bool injured;
        private bool fleeing;
        private int fleeDirection;
        private int minSpeed = 5-4;
        private int maxSpeed = 11-4;
        private Vector2 lookPoint;
        private float rotTarget;
        private float rotOrigin;
        private float rotSpeed;
        private float timer;

        private float lastSoundTime = 0;
        private float soundCooldown = 2f; // Cooldown in seconds
        private bool shouldPlayVoice;
       




        public CivillianEnemy()
        {
            SetObjectAnimation(AnimNames.PlayerRifleMove);
            centerOrigin = true;
            layerDepth = Global.currentScene.GetObjectLayerDepth(LayerDepth.Enemies);


        }

        bool WalkedFar(float range, Vector2 v1, Vector2 v2)
        {
            var dx = v1.X - v2.X;
            var dy = v1.Y - v2.Y;
            return dx * dx + dy * dy < range * range;
        }

        public override void Update()
        {
            if (Global.currentScene.isPaused || dead) return;

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
                if (Math.Abs(position.X - blood.position.X) < (blood.texture.Width * scale.X) / 2 / 2 && Math.Abs(position.Y - blood.position.Y) < (blood.texture.Height * scale.Y) / 2 / 2)
                {

                    bloodied = 10;

                }
            }


            timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 dir = lookPoint - position;
            rotTarget = (float)Math.Atan2(-dir.Y, -dir.X) + MathHelper.Pi;
            rotTarget = ShortestRotation(rotTarget, rotation);
            if (rotTarget > 2.5f) { rotSpeed = 0.0005f; } else if (rotTarget > 1.5f) { rotSpeed = 0.001f; } else { rotSpeed = 0.005f; };
            LerpTowardsTarget(rotTarget, rotation, timer, rotSpeed);
            
            if (Math.Abs(Global.player.position.X - position.X) < rnd.Next(850, 1250) && Math.Abs(Global.player.position.Y - position.Y) < rnd.Next(850, 1250))
            {
                if (!fleeing)
                {
                    //ChooseRndVoiceLine();

                    fleeDirection = rnd.Next(1,4);
                    rotOrigin = rotation;
                    fleeing = true;
                }
                else
                {
                    if (rnd.Next(1, 101) < 5)
                    {
                        fleeDirection = rnd.Next(1, 4);
                        rotOrigin = rotation;
                    }
                    Flee();
                }
            }
            else
            {
                fleeing = false;
            }

            
        }


        private void ChooseRndVoiceLine()
        {
            if (!shouldPlayVoice) lastSoundTime = soundCooldown;

            if (timer - lastSoundTime < soundCooldown) return;

            shouldPlayVoice = true;

            if (rnd.Next(0, 15) == 0)
            {
                int soundIndex = rnd.Next(0, deathSounds.Length);

                GlobalSound.sounds[deathSounds[soundIndex]].Play();

            }

            lastSoundTime = timer;
        }




        public void ChangeDirection() 
        {
            fleeDirection = rnd.Next(1, 4);
            rotOrigin = rotation;
        }

        public void Flee()
        {
            switch (fleeDirection)  
            {
                case 1:
                    if (Global.player.position.X>position.X)
                    {
                        position.X -= rnd.Next(minSpeed, maxSpeed);
                        lookPoint = new Vector2(position.X-50, position.Y);

                    }
                    else
                    {
                        position.X += rnd.Next(minSpeed, maxSpeed);
                        lookPoint = new Vector2(position.X + 50, position.Y);
                    }
                    break;

                case 2:
                    if (Global.player.position.Y > position.Y)
                    {
                        position.Y -= rnd.Next(minSpeed, maxSpeed);
                        lookPoint = new Vector2(position.X, position.Y-50);
                    }
                    else
                    {
                        position.Y += rnd.Next(minSpeed, maxSpeed);
                        lookPoint = new Vector2(position.X, position.Y+50);
                    }
                    break;

                case 3:
                    if (Global.player.position.X > position.X)
                    {
                        position.X -= rnd.Next(minSpeed, maxSpeed);
                        lookPoint = new Vector2(position.X-50, position.Y);

                    }
                    else
                    {
                        position.X += rnd.Next(minSpeed, maxSpeed);
                        lookPoint = new Vector2(position.X+50, position.Y);
                    }
                    if (Global.player.position.Y > position.Y)
                    {
                        position.Y -= rnd.Next(minSpeed, maxSpeed);
                        lookPoint = new Vector2(lookPoint.X, position.Y - 50);
                    }
                    else
                    {
                        position.Y += rnd.Next(minSpeed, maxSpeed);
                        lookPoint = new Vector2(lookPoint.X, position.Y + 50);
                    }
                    break;

                default:
                    break;
            }

            
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
