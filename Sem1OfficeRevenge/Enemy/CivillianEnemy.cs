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

        public CivillianEnemy()
        {
            SetObjectAnimation(AnimNames.PlayerRifleIdle);
        }

        public override void Update()
        {
            if (Math.Abs(Global.player.position.X - position.X) < 100 && Math.Abs(Global.player.position.Y - position.Y) < 100)
            {
                if (!fleeing)
                {
                    fleeDirection = rnd.Next(1,4);
                    fleeing = true;
                }
                else
                {
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
                        position.X += rnd.Next(1,6); 

                    }
                    else
                    {
                        position.X -= rnd.Next(1, 6);

                    }
                    break;

                case 2:
                    if (Global.player.position.Y > position.Y)
                    {
                        position.Y += rnd.Next(1, 6);

                    }
                    else
                    {
                        position.Y -= rnd.Next(1, 6);

                    }
                    break;

                case 3:
                    if (Global.player.position.X > position.X)
                    {
                        position.X += rnd.Next(1, 6);

                    }
                    else
                    {
                        position.X -= rnd.Next(1, 6);

                    }
                    if (Global.player.position.Y > position.Y)
                    {
                        position.Y += rnd.Next(1, 6);

                    }
                    else
                    {
                        position.Y -= rnd.Next(1, 6);

                    }
                    break;

                default:
                    break;
            }
        }


    }
}
