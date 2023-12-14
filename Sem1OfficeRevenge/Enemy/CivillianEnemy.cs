using Microsoft.Xna.Framework;
using System;

namespace Sem1OfficeRevenge
{
    internal class CivillianEnemy : GenericEnemy
    {
        public bool injured;
        private bool fleeing;
        private int fleeDirection;
        private int minSpeed = 1;
        private int maxSpeed = 7;
        private Vector2 lookPoint;
        private float rotTarget;
        private float rotOrigin;
        private float rotSpeed;
        private float timer;
        private Rectangle center;
        private Vector2 tempPosition;
        private bool isInsideRoom = true;
        private Room ourRoom;


        private float lastSoundTime = 0;
        private float soundCooldown = 2f; // Cooldown in seconds
        private bool shouldPlayVoice;

        private Vector2 targetPos;
       

        public CivillianEnemy()
        {
            speed = 5;
            SetObjectAnimation(AnimNames.NPCIdle);
            centerOrigin = true;
            layerDepth = Global.currentScene.GetObjectLayerDepth(LayerDepth.Enemies);
            health = 20;
            Vector2 tempPosition = this.position;            
        }

        //Check if enemy has walked far enough to leave a shoe print
        bool WalkedFar(float range, Vector2 v1, Vector2 v2)
        {
            var dx = v1.X - v2.X;
            var dy = v1.Y - v2.Y;
            return dx * dx + dy * dy < range * range;
        }

        public override void Update()
        {
            //Sets the room the enemy is in
            if (ourRoom == null)
            {
                foreach (Room room in Global.currentSceneData.rooms)
                {
                    if (Collision.ContainsEitherBox(this, room.collisionBox, room.hallwayCol))
                    {
                        ourRoom = room;
                        break;
                    }


                }
            }

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

            if (isInsideRoom == true && ourRoom != null)
            {
                if (Collision.ContainsEitherBox(this, ourRoom.collisionBox, ourRoom.hallwayCol))
                {
                    
                    //this.color = Color.White;
                    //center = room.collisionBox
                }
                else
                {
                    targetPos = ourRoom.position + new Vector2(rnd.Next(-200, 201), rnd.Next(-200, 201));
                    isInsideRoom = false;
                    //ChangeDirection();
                    //this.color = Color.Gray;
                }


            }

            //if (!isInsideRoom)
            //{
            //    if (center.X > this.position.X)
            //    {
            //        this.position.X += 3;
            //    }
            //    else { this.position.X -= 3; }

            //    if (center.Y > this.position.Y)
            //    {
            //        this.position.Y += 3;
            //    }
            //    else { this.position.Y -= 3; }

            //    //this.position = tempPosition;
            //}

            //check if the enemy is in blood
            foreach (Blood blood in Global.currentSceneData.bloods)
            {
                if (Math.Abs(position.X - blood.position.X) < (blood.texture.Width * scale.X) / 2 / 2 && Math.Abs(position.Y - blood.position.Y) < (blood.texture.Height * scale.Y) / 2 / 2)
                {
                    bloodied = 10;
                }
            }

            //if in range:
            if (Math.Abs(Global.player.position.X - position.X) < rnd.Next(850, 1250) && Math.Abs(Global.player.position.Y - position.Y) < rnd.Next(850, 1250))
            {
                //Walk
                if (animation == GlobalAnimations.SetAnimation(AnimNames.NPCIdle))
                {
                    SetObjectAnimation(AnimNames.CivWalk);
                }
                if (!fleeing)
                {
                    //ChooseRndVoiceLine();

                    //Flee randomly
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
                if (animation == GlobalAnimations.SetAnimation(AnimNames.CivWalk))
                {
                    SetObjectAnimation(AnimNames.NPCIdle);
                }
            }
        }


        public void ChangeDirection() 
        {
            fleeDirection = rnd.Next(1, 4);
            rotOrigin = rotation;
        }
        
        //Choose a random voice line to play
        private void ChooseRndVoiceLine()
        {
            if (!shouldPlayVoice) lastSoundTime = soundCooldown;

            if (timer - lastSoundTime < soundCooldown) return;

            shouldPlayVoice = true;

            if (rnd.Next(0, 2) == 0)
            {
                int soundIndex = rnd.Next(0, deathSounds.Length);

                GlobalSound.sounds[deathSounds[soundIndex]].Play();

            }

            lastSoundTime = timer;
        }

        
        public void Flee()
        {
            if (isInsideRoom == false)
            {
                
                if (targetPos.X > position.X)
                {
                    position.X += speed;
                }
                else if (targetPos.X < position.X) { position.X -= speed; }

                if (targetPos.Y > position.Y)
                {
                    position.Y += speed;
                }
                else if (targetPos.Y < position.Y) { position.Y -= speed; }

                //float hat = Math.Abs(Global.player.position.X - position.X);
                //float fat = Math.Abs(Global.player.position.Y - position.Y);
                if (Math.Abs(Global.player.position.X - position.X) < 300 && Math.Abs(Global.player.position.Y - position.Y) < 300)
                {

                    isInsideRoom = true;
                }

                timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 dir = targetPos - position;
                rotTarget = (float)Math.Atan2(-dir.Y, -dir.X) + MathHelper.Pi;
                rotTarget = ShortestRotation(rotTarget, rotation);
                if (rotTarget > 2.5f) { rotSpeed = 0.0005f; } else if (rotTarget > 1.5f) { rotSpeed = 0.001f; } else { rotSpeed = 0.005f; };
                LerpTowardsTarget(rotTarget, rotation, timer, rotSpeed);
            }

            else
            {
                timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 dir = lookPoint - position;
                rotTarget = (float)Math.Atan2(-dir.Y, -dir.X) + MathHelper.Pi;
                rotTarget = ShortestRotation(rotTarget, rotation);
                if (rotTarget > 2.5f) { rotSpeed = 0.0005f; } else if (rotTarget > 1.5f) { rotSpeed = 0.001f; } else { rotSpeed = 0.005f; };
                LerpTowardsTarget(rotTarget, rotation, timer, rotSpeed);
                switch (fleeDirection)
                {
                    case 1:
                        if (Global.player.position.X > position.X)
                        {
                            tempPosition = this.position;
                            position.X -= rnd.Next(minSpeed, maxSpeed);
                            lookPoint = new Vector2(position.X - 50, position.Y);

                        }
                        else
                        {
                            tempPosition = this.position;
                            position.X += rnd.Next(minSpeed, maxSpeed);
                            lookPoint = new Vector2(position.X + 50, position.Y);
                        }
                        break;

                    case 2:
                        if (Global.player.position.Y > position.Y)
                        {
                            tempPosition = this.position;
                            position.Y -= rnd.Next(minSpeed, maxSpeed);
                            lookPoint = new Vector2(position.X, position.Y - 50);
                        }
                        else
                        {
                            tempPosition = this.position;
                            position.Y += rnd.Next(minSpeed, maxSpeed);
                            lookPoint = new Vector2(position.X, position.Y + 50);
                        }
                        break;

                    case 3:
                        if (Global.player.position.X > position.X)
                        {
                            tempPosition = this.position;
                            position.X -= rnd.Next(minSpeed, maxSpeed);
                            lookPoint = new Vector2(position.X - 50, position.Y);

                        }
                        else
                        {
                            tempPosition = this.position;
                            position.X += rnd.Next(minSpeed, maxSpeed);
                            lookPoint = new Vector2(position.X + 50, position.Y);
                        }
                        if (Global.player.position.Y > position.Y)
                        {
                            tempPosition = this.position;
                            position.Y -= rnd.Next(minSpeed, maxSpeed);
                            lookPoint = new Vector2(lookPoint.X, position.Y - 50);
                        }
                        else
                        {
                            tempPosition = this.position;
                            position.Y += rnd.Next(minSpeed, maxSpeed);
                            lookPoint = new Vector2(lookPoint.X, position.Y + 50);
                        }
                        break;

                    default:
                        break;
                }

            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
