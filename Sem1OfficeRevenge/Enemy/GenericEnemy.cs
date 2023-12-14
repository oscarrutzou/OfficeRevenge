using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sem1OfficeRevenge
{
    public class GenericEnemy : GameObject
    {
        public bool dead;
        private Blood blood;
        protected List<ShoePrint> shoePrints = new List<ShoePrint>();
        protected Vector2 oldPos;
        protected int bloodied = 0;
        protected bool right = true;
        protected Random rnd = new Random();
        public int health;

        //Voice lines
        public static SoundNames[] deathVoiceLines = new SoundNames[]
        {
            SoundNames.DeathVoiceLine1, SoundNames.DeathVoiceLine2, SoundNames.DeathVoiceLine3, SoundNames.DeathVoiceLine4, SoundNames.DeathVoiceLine5, SoundNames.DeathVoiceLine6, SoundNames.DeathVoiceLine7, SoundNames.DeathVoiceLine8, SoundNames.DeathVoiceLine9, SoundNames.DeathVoiceLine10,
        };

        //Death sounds
        internal SoundNames[] deathSounds = new SoundNames[]
{
            SoundNames.GenericDeath1, SoundNames.GenericDeath2, SoundNames.GenericDeath3, SoundNames.GenericDeath4, SoundNames.GenericDeath5, SoundNames.GenericDeath6, SoundNames.GenericDeath7, SoundNames.GenericDeath8, SoundNames.GenericDeath9
        };


        public GenericEnemy()
        {
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Enemies);
            scale = new Vector2(3.3f,3.3f);
            SetObjectAnimation(AnimNames.NPCIdle);
            SetCollisionBox(60, 60);
        }

        //Take damage
        public void TakeDmg(int bulletDmg)
        {
            health -= bulletDmg;
            if (health <= 0)
            {
                Die();

            }
        }
        
        //Die
        public void Die()
        {
            blood = new Blood(position);
            Global.currentScene.Instantiate(blood);
            dead = true;
            ScoreManager.killCount++;
            
            //Set death animation
            if (this is CombatEnemy)
            {
                SetObjectAnimation(AnimNames.ChairDeath);
                animation.onAnimationDone += () => { animation.frameRate = 0; };
            }
            
            //Set death animation
            if (this is CivillianEnemy)
            {
                SetObjectAnimation(AnimNames.CivDeath);
                animation.onAnimationDone += () => { animation.frameRate = 0; };
            }


            SoundOnDeath();

            
        }

        //Play sound on death
        private void SoundOnDeath()
        {
            if (Global.rnd.Next(0, 2) != 0) return;

            if (!GlobalSound.IsAnySoundPlaying(deathVoiceLines) && !GlobalSound.IsAnySoundPlaying(Global.player.shootVoiceLines))
            {
                GlobalSound.PlayRandomSound(deathVoiceLines, 1, 3);
            }
            else
            {
                GlobalSound.PlayRandomSound(deathSounds, 1, 6);
            }
        }

        public override void Draw()
        {
            base.Draw();
            //DrawDebugCollisionBox();
        }
    }        
}

