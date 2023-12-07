﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public enum SoundNames
    {
        Shot,
        Step,
        TestSound3,

        //Player
        Player1,
        Player2,
        Player3,
        Player4,
        Player5,
        Player6,
        PlayerDeath,

        //Attack enemy
        ChairAction1,
        ChairAction2,
        ChairAction3,
        ChairAction4,
        ChairAction5,

        //Death
        DeathVoiceLine1,
        DeathVoiceLine2,
        DeathVoiceLine3,
        DeathVoiceLine4,
        DeathVoiceLine5,
        DeathVoiceLine6,
        DeathVoiceLine7,
        DeathVoiceLine8,
        DeathVoiceLine9,
        DeathVoiceLine10,

        GenericDeath1,
        GenericDeath2,
        GenericDeath3,
        GenericDeath4,
        GenericDeath5,
        GenericDeath6,
        GenericDeath7,
        GenericDeath8,
        GenericDeath9,

    }
    internal static class GlobalSound
    {

        public static Dictionary<SoundNames, SoundEffect> sounds;

        public static bool inMenu = true;

        public static SoundEffect MenuMusic;
        public static SoundEffect GameMusic;

        public static SoundEffectInstance InstanceMenuMusic;
        public static SoundEffectInstance InstanceGameMusic;

        public static float musicVolume = 0.2f;
        public static float sfxVolume = 1f;

        public static List<SoundEffectData> soundInstances { get; private set; }


        public static void LoadContent()
        {
            soundInstances = new List<SoundEffectData>();

            MenuMusic = Global.world.Content.Load<SoundEffect>("Fonts\\MainTheme");
            GameMusic = Global.world.Content.Load<SoundEffect>("Fonts\\DistortedTheme");

            sounds = new Dictionary<SoundNames, SoundEffect>
            {
                { SoundNames.Shot, Global.world.Content.Load<SoundEffect>("Fonts\\gunshot")},
                { SoundNames.Step, Global.world.Content.Load<SoundEffect>("Fonts\\step")},

                { SoundNames.Player1, Global.world.Content.Load<SoundEffect>("Sounds\\Shooter1")},
                { SoundNames.Player2, Global.world.Content.Load<SoundEffect>("Sounds\\Shooter2")},
                { SoundNames.Player3, Global.world.Content.Load<SoundEffect>("Sounds\\Shooter3")},
                { SoundNames.Player4, Global.world.Content.Load<SoundEffect>("Sounds\\Shooter4")},
                { SoundNames.Player5, Global.world.Content.Load<SoundEffect>("Sounds\\Shooter5")},
                { SoundNames.Player6, Global.world.Content.Load<SoundEffect>("Sounds\\Shooter6")},
                { SoundNames.PlayerDeath, Global.world.Content.Load<SoundEffect>("Sounds\\ShooterDeath")},

                { SoundNames.ChairAction1, Global.world.Content.Load<SoundEffect>("Sounds\\ChairAction1")},
                { SoundNames.ChairAction2, Global.world.Content.Load<SoundEffect>("Sounds\\ChairAction2")},
                { SoundNames.ChairAction3, Global.world.Content.Load<SoundEffect>("Sounds\\ChairAction3")},
                { SoundNames.ChairAction4, Global.world.Content.Load<SoundEffect>("Sounds\\ChairAction4")},
                { SoundNames.ChairAction5, Global.world.Content.Load<SoundEffect>("Sounds\\ChairAction5")},

                { SoundNames.DeathVoiceLine1, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\EnemDeathVoiceLine1")},
                { SoundNames.DeathVoiceLine2, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\EnemDeathVoiceLine2")},
                { SoundNames.DeathVoiceLine3, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\EnemDeathVoiceLine3")},
                { SoundNames.DeathVoiceLine4, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\EnemDeathVoiceLine4")},
                { SoundNames.DeathVoiceLine5, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\EnemDeathVoiceLine5")},
                { SoundNames.DeathVoiceLine6, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\EnemDeathVoiceLine6")},
                { SoundNames.DeathVoiceLine7, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\EnemDeathVoiceLine7")},
                { SoundNames.DeathVoiceLine8, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\EnemDeathVoiceLine8")},
                { SoundNames.DeathVoiceLine9, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\EnemDeathVoiceLine9")},
                { SoundNames.DeathVoiceLine10, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\EnemDeathVoiceLine10")},


                { SoundNames.GenericDeath1, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\GenericDeath1")},
                { SoundNames.GenericDeath2, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\GenericDeath2")},
                { SoundNames.GenericDeath3, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\GenericDeath3")},
                { SoundNames.GenericDeath4, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\GenericDeath4")},
                { SoundNames.GenericDeath5, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\GenericDeath5")},
                { SoundNames.GenericDeath6, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\GenericDeath6")},
                { SoundNames.GenericDeath7, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\GenericDeath7")},
                { SoundNames.GenericDeath8, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\GenericDeath8")},
                { SoundNames.GenericDeath9, Global.world.Content.Load<SoundEffect>("Sounds\\EnemyDeath\\GenericDeath9")},

                { SoundNames.TestSound3, Global.world.Content.Load<SoundEffect>("Fonts\\DistortedTheme")}
            };
        }

        public static bool IsAnySoundPlaying(SoundNames[] soundArray)
        {
            foreach (SoundEffectData data in soundInstances)
            {
                foreach (SoundNames name in soundArray)
                {
                    SoundEffect soundName = sounds[name];
                    if (data.Instance.State == SoundState.Playing && data.Sound == soundName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void PlayRandomSound(SoundNames[] soundArray, int maxAmountPlaying)
        {
            int soundIndex = Global.rnd.Next(0, soundArray.Length);
            int index = 0;

            SoundEffect sound = sounds[soundArray[soundIndex]];

            foreach (SoundEffectData data in soundInstances)
            {
                foreach (SoundNames name in soundArray)
                {
                    SoundEffect soundName = sounds[name];
                    if (data.Instance.State == SoundState.Playing && data.Sound == soundName)
                    {
                        index++;
                    }
                }
            }

            if (index >= maxAmountPlaying)
            {
                return;
            }

            PlaySound(sound);
        }


        public static void PlaySound(SoundEffect sound)
        {
            SoundEffectInstance instance = sound.CreateInstance();
            soundInstances.Add(new SoundEffectData(instance, sound));
            instance.Volume = sfxVolume;
            instance.Play();

            List<SoundEffectData> toRemove = new List<SoundEffectData>();

            foreach (SoundEffectData data in soundInstances)
            {
                if (data.Instance.State == SoundState.Stopped)
                {
                    data.Instance.Stop();
                    data.Instance.Dispose();
                    toRemove.Add(data);
                }
            }

            foreach (SoundEffectData data in toRemove)
            {
                soundInstances.Remove(data);
            }
        }


        public static void PitchedPlay(SoundEffect sound, float pitchVariance)
        {
            SoundEffectInstance instance = sound.CreateInstance();
            float pitch = (float)Global.rnd.NextDouble();
            pitch = pitch - (1 - pitch);

            if (Global.rnd.Next(0, 10) > 5)
            {
                pitch = 0 - pitch;
            }

            instance.Pitch = pitch;
            instance.Volume = sfxVolume;
            instance.Play();

            soundInstances.Add(new SoundEffectData(instance, sound));

            List<SoundEffectData> toRemove = new List<SoundEffectData>();

            foreach (SoundEffectData data in soundInstances)
            {
                if (data.Instance.State == SoundState.Stopped)
                {
                    data.Instance.Stop();
                    data.Instance.Dispose();
                    toRemove.Add(data);
                }
            }

            foreach (SoundEffectData data in toRemove)
            {
                soundInstances.Remove(data);
            }
        }


        public static void MusicUpdate() 
        {
            if (InstanceGameMusic == null ||InstanceMenuMusic == null)
            {
                InstanceMenuMusic = MenuMusic.CreateInstance();
                InstanceGameMusic = GameMusic.CreateInstance();
            }
            else
            {
                InstanceGameMusic.Volume = Math.Clamp(musicVolume, 0, 1);
                InstanceMenuMusic.Volume = Math.Clamp(musicVolume, 0, 1);

                if (inMenu)
                {
                    InstanceGameMusic.Stop();
                }
                else
                {
                    InstanceMenuMusic.Stop();
                }

                if (InstanceMenuMusic.State == SoundState.Stopped && inMenu)
                {
                    InstanceMenuMusic.Play();
                }
                if (InstanceGameMusic.State == SoundState.Stopped && !inMenu)
                {
                    InstanceGameMusic.Play();
                }
            }

        }




        //public static void PlayRandomSound(SoundNames[] soundArray, int maxAmountPlaying, float divideVolume)
        //{
        //    int soundIndex = Global.rnd.Next(0, soundArray.Length);
        //    int index = 0;

        //    SoundEffect sound = sounds[soundArray[soundIndex]];

        //    foreach (SoundEffectData data in soundInstances)
        //    {
        //        foreach (SoundNames name in soundArray)
        //        {
        //            SoundEffect soundName = sounds[name];
        //            if (data.Instance.State == SoundState.Playing && data.Sound == soundName)
        //            {
        //                index++;
        //            }
        //        }
        //    }

        //    if (index >= maxAmountPlaying)
        //    {
        //        return;
        //    }

        //    PlaySound(sound, divideVolume);
        //}

        //public static void PlaySound(SoundEffect sound, float divideVolume)
        //{
        //    SoundEffectInstance instance = sound.CreateInstance();
        //    soundInstances.Add(new SoundEffectData(instance, sound));
        //    instance.Volume = sfxVolume / divideVolume;
        //    instance.Play();

        //    List<SoundEffectData> toRemove = new List<SoundEffectData>();

        //    foreach (SoundEffectData data in soundInstances)
        //    {
        //        if (data.Instance.State == SoundState.Stopped)
        //        {
        //            data.Instance.Stop();
        //            data.Instance.Dispose();
        //            toRemove.Add(data);
        //        }
        //    }

        //    foreach (SoundEffectData data in toRemove)
        //    {
        //        soundInstances.Remove(data);
        //    }
        //}

    }


}
