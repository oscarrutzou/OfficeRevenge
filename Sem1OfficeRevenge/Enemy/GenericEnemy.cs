using Microsoft.Xna.Framework;

namespace Sem1OfficeRevenge
{
    public class GenericEnemy : GameObject
    {
        private int health;
        public bool dead;


        internal SoundNames[] deathVoiceLines = new SoundNames[]
        {
            SoundNames.DeathVoiceLine1, SoundNames.DeathVoiceLine2, SoundNames.DeathVoiceLine3, SoundNames.DeathVoiceLine4, SoundNames.DeathVoiceLine5, SoundNames.DeathVoiceLine6, SoundNames.DeathVoiceLine7, SoundNames.DeathVoiceLine8, SoundNames.DeathVoiceLine9, SoundNames.DeathVoiceLine10,
        };

        internal SoundNames[] deathSounds = new SoundNames[]
{
            SoundNames.GenericDeath1, SoundNames.GenericDeath2, SoundNames.GenericDeath3, SoundNames.GenericDeath4, SoundNames.GenericDeath5, SoundNames.GenericDeath6, SoundNames.GenericDeath7, SoundNames.GenericDeath8, SoundNames.GenericDeath9
        };

        public GenericEnemy()
        {
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Enemies);
        }
        
        public void Die()
        {
            dead = true;
            ScoreManager.killCount++;
            animation.frameRate = 0;
            color = Color.DarkRed;

            SoundOnDeath();
        }

        private void SoundOnDeath()
        {
            if (!GlobalSound.IsAnySoundPlaying(deathVoiceLines))
            {
                GlobalSound.PlayRandomSound(deathVoiceLines, 1);
            }
            else
            {
                GlobalSound.PlayRandomSound(deathSounds, 1);
            }
        }

    }
}
