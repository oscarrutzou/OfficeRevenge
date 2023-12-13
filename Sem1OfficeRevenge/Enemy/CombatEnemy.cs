using System;
using Microsoft.Xna.Framework;

namespace Sem1OfficeRevenge
{
    public class CombatEnemy : GenericEnemy
    {
        public bool isAttacking;
        private float timer;
        private float rotTarget;


        public CombatEnemy()
        {

            health = 50;
            SetObjectAnimation(AnimNames.ChairWalk);
            speed = 7.5f;
            centerOrigin = true;
        }

        private void Attack()
        {
            if (dead) return;
            AttackVL();
            Global.player.DamagePlayer(50);
            isAttacking = false;
            SetObjectAnimation(AnimNames.ChairAttack);
            animation.onAnimationDone += () => { SetObjectAnimation(AnimNames.ChairWalk); };
        }

        private void AttackVL()
        {
            if (Global.rnd.Next(0, 2) != 0) return;

            if (!GlobalSounds.IsAnySoundPlaying(deathVoiceLines) && !GlobalSounds.IsAnySoundPlaying(Global.player.shootVoiceLines) && !GlobalSounds.IsAnySoundPlaying(combatEnemyAttackVL))
            {
                GlobalSounds.PlayRandomSound(combatEnemyAttackVL, 1);
            }
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

            if (health <= 0)
            {
                Die();
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
                if (Math.Abs(position.X - blood.position.X) < (blood.texture.Width * scale.X) / 2 / 2 && Math.Abs(position.Y - blood.position.Y) < (blood.texture.Height * scale.Y) / 2 / 2)
                {

                    bloodied = 10;

                }
            }



            //if in range:
            if (Math.Abs(Global.player.position.X - position.X) < rnd.Next(850, 1250) && Math.Abs(Global.player.position.Y - position.Y) < rnd.Next(850, 1250))
            {
                if (animation == GlobalAnimations.SetAnimation(AnimNames.NPCIdle))
                {
                    SetObjectAnimation(AnimNames.ChairWalk);

                }

                if (Math.Abs(Global.player.position.X - position.X) < 65 && Math.Abs(Global.player.position.Y - position.Y) < 65 && isAttacking == false)
                {
                    isAttacking = true;
                    Attack();

                }
                else if (Math.Abs(Global.player.position.X - position.X) > 7 && Math.Abs(Global.player.position.Y - position.Y) > 7)
                {
                    isAttacking = false;

                }


                if (Math.Abs(Global.player.position.X - position.X) > 50 && Global.player.position.X > position.X)
                {
                    position.X += speed;
                }
                else if (Math.Abs(Global.player.position.X - position.X) > 50 && Global.player.position.X < position.X) { position.X -= speed; }

                if (Math.Abs(Global.player.position.Y - position.Y) > 50 && Global.player.position.Y > position.Y)
                {
                    position.Y += speed;
                }
                else if (Math.Abs(Global.player.position.Y - position.Y) > 50 && Global.player.position.Y < position.Y) { position.Y -= speed; }

                timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 dir = Global.player.position - position;
                rotTarget = (float)Math.Atan2(-dir.Y, -dir.X) + MathHelper.Pi;
                rotTarget = ShortestRotation(rotTarget, rotation);
                LerpTowardsTarget(rotTarget, rotation, timer, 0.05f);




            }
            
        }
    }
}
