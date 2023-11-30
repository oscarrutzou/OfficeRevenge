using Microsoft.Xna.Framework;
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
        private Random rnd = new Random();
        private int minSpeed = 5-4;
        private int maxSpeed = 11-4;
        private Vector2 lookPoint;
        private float rotTarget;
        private float rotOrigin;
        private float timer;

        public CivillianEnemy()
        {
            SetObjectAnimation(AnimNames.PlayerRifleMove);
            centerOrigin = true;
            layerDepth = Global.currentScene.GetObjectLayerDepth(LayerDepth.Enemies);
        }

        public override void Update()
        {
            if (dead) return;
            timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 dir = lookPoint - position;
            rotTarget = (float)Math.Atan2(-dir.Y, -dir.X) + MathHelper.Pi;
            LerpTowardsTarget(rotTarget, rotOrigin, 1, 1);
            if (Math.Abs(Global.player.position.X - position.X) < rnd.Next(850, 1250) && Math.Abs(Global.player.position.Y - position.Y) < rnd.Next(850, 1250))
            {
                if (!fleeing)
                {
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
