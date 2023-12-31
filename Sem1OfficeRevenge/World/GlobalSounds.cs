﻿using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    public enum SoundNames
    {
        Shotgun,
        Shot,
        Step,
        TestSound3,
        ElevatorDoorOpen,
        ElevatorDing,

        //Player
        PlayerIntro,
        PlayerDeath,
        Player1,
        Player2,
        Player3,
        Player4,
        Player5,
        Player6,
        Player7,
        Player8,
        Player9,
        Player10,
        Player11,

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
    public static class GlobalSounds
    {
        //Sound effects
        public static Dictionary<SoundNames, SoundEffect> sounds;
        private static Dictionary<SoundNames, List<SoundEffectInstance>> soundInstancesPool;
        private static int maxInstanceOfOneSound = 2;
        private static int maxInstanceOfGunSound = 10;

        public static bool inMenu = true;

        private static SoundEffect menuMusic;
        private static SoundEffect gameMusic;
        private static SoundEffect elevatorMusic;

        private static SoundEffectInstance instanceMenuMusic;
        private static SoundEffectInstance instanceGameMusic;
        private static SoundEffectInstance instanceElevatorMusic;

        public static float musicVolume = 1f;
        public static float sfxVolume = 1f;
        private static int musicVolDivide = 4; //Makes the song less loud by dividing the real volume

        public static void LoadContent()
        {
            soundInstancesPool = new Dictionary<SoundNames, List<SoundEffectInstance>>();

            menuMusic = Global.world.Content.Load<SoundEffect>("Fonts\\MainTheme");
            gameMusic = Global.world.Content.Load<SoundEffect>("Fonts\\DistortedTheme");
            elevatorMusic = Global.world.Content.Load<SoundEffect>("Sounds\\ElevatorTheme");

            sounds = new Dictionary<SoundNames, SoundEffect>
            {
                { SoundNames.Shotgun, Global.world.Content.Load<SoundEffect>("Sounds\\shotgun")},
                { SoundNames.Shot, Global.world.Content.Load<SoundEffect>("Fonts\\gunshot")},
                { SoundNames.Step, Global.world.Content.Load<SoundEffect>("Fonts\\step")},
                { SoundNames.ElevatorDing, Global.world.Content.Load<SoundEffect>("Sounds\\ElevatorDing")},
                { SoundNames.ElevatorDoorOpen, Global.world.Content.Load<SoundEffect>("Sounds\\ElevatorDoorOpen")},

                { SoundNames.Player1, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter1")},
                { SoundNames.Player2, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter2")},
                { SoundNames.Player3, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter3")},
                { SoundNames.Player4, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter4")},
                { SoundNames.Player5, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter5")},
                { SoundNames.Player6, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter6")},
                { SoundNames.Player7, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter7")},
                { SoundNames.Player8, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter8")},
                { SoundNames.Player9, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter9")},
                { SoundNames.Player10, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter10")},
                { SoundNames.Player11, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\Shooter11")},
                { SoundNames.PlayerIntro, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\ShooterIntro")},
                { SoundNames.PlayerDeath, Global.world.Content.Load<SoundEffect>("Sounds\\Player\\ShooterDeath")},

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

            //Create sound instances
            foreach (var sound in sounds)
            {
                soundInstancesPool[sound.Key] = new List<SoundEffectInstance>();
                int max = maxInstanceOfOneSound;
                if (sound.Key == SoundNames.Shot || sound.Key == SoundNames.Shotgun) max = maxInstanceOfGunSound;

                for (int i = 0; i < max; i++)
                {
                    soundInstancesPool[sound.Key].Add(sound.Value.CreateInstance());
                }
            }
        }

        public static bool IsAnySoundPlaying(SoundNames[] soundArray)
        {
            //Check if any sound is playing
            foreach (SoundNames name in soundArray)
            {
                foreach (SoundEffectInstance inst in soundInstancesPool[name])
                {
                    if (inst.State == SoundState.Playing)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void PlayRandomSound(SoundNames[] soundArray, float maxAmountPlaying)
        {
            //Play a random sound from the array
            int soundIndex = Global.rnd.Next(0, soundArray.Length);
            int index = 0;

            SoundNames soundName = soundArray[soundIndex];

            foreach (SoundEffectInstance inst in soundInstancesPool[soundName])
            {
                //Check how many of the same sound is playing
                if (inst.State == SoundState.Playing)
                {
                    index++;
                }
            }

            if (index >= maxAmountPlaying)
            {
                return;
            }

            PlaySound(soundName);
        }

        public static void PlaySound(SoundNames soundName)
        {
            //Play a sound
            SoundEffectInstance instance = null;
            foreach (var inst in soundInstancesPool[soundName])
            {
                if (inst.State != SoundState.Playing)
                {
                    instance = inst;
                    break;
                }
            }

            if (instance == null)
            {
                // All instances are playing, so stop and reuse the oldest one.
                instance = soundInstancesPool[soundName][0];
                instance.Stop();
            }

            instance.Volume = sfxVolume;
            instance.Play();
        }

        public static void PlayRandomSound(SoundNames[] soundArray, int maxAmountPlaying, float soundVolDivided)
        {
            //Play a random sound from the array
            int soundIndex = Global.rnd.Next(0, soundArray.Length);
            int index = 0;

            SoundNames soundName = soundArray[soundIndex];

            //Check how many of the same sound is playing
            foreach (SoundEffectInstance inst in soundInstancesPool[soundName])
            {
                if (inst.State == SoundState.Playing)
                {
                    index++;
                }
            }

            //Check if the max amount of the same sound is playing
            if (index >= maxAmountPlaying)
            {
                return;
            }

            PlaySound(soundName, soundVolDivided);
        }

        public static void PlaySound(SoundNames soundName, float floatSoundVolDivided)
        {
            //Play a sound
            SoundEffectInstance instance = null;
            foreach (var inst in soundInstancesPool[soundName])
            {
                if (inst.State != SoundState.Playing)
                {
                    instance = inst;
                    break;
                }
            }

            if (instance == null)
            {
                // All instances are playing, so stop and reuse the oldest one.
                instance = soundInstancesPool[soundName][0];
                instance.Stop();
            }

            instance.Volume = sfxVolume / floatSoundVolDivided;
            instance.Play();
        }

        public static void PitchedPlay(SoundNames soundName)
        {
            //Play a sound with a random pitch
            float pitch = (float)Global.rnd.NextDouble();
            pitch = pitch - (1 - pitch);
            if (Global.rnd.Next(0, 10) > 5)
            {
                pitch = 0 - pitch;
            }

            SoundEffectInstance instance = null;
            foreach (var inst in soundInstancesPool[soundName])
            {
                if (inst.State != SoundState.Playing)
                {
                    instance = inst;
                    break;
                }
            }

            if (instance == null)
            {
                // All instances are playing, so stop and reuse the oldest one.
                instance = soundInstancesPool[soundName][0];
                instance.Stop();
            }

            instance.Pitch = pitch;
            instance.Volume = sfxVolume;
            instance.Play();
        }

        public static void MusicUpdate() 
        {
            //Update music
            if (instanceGameMusic == null || instanceMenuMusic == null || instanceElevatorMusic == null)
            {
                instanceMenuMusic = menuMusic.CreateInstance();
                instanceGameMusic = gameMusic.CreateInstance();
                instanceElevatorMusic = elevatorMusic.CreateInstance();
            }
            else
            {
                instanceMenuMusic.Volume = Math.Clamp(musicVolume, 0, 1) / musicVolDivide;
                instanceGameMusic.Volume = Math.Clamp(musicVolume, 0, 1) / musicVolDivide;
                instanceElevatorMusic.Volume = Math.Clamp(musicVolume, 0, 1) / musicVolDivide;

                //Check if the music should be playing
                if (inMenu)
                {
                    instanceGameMusic.Stop();
                }
                else
                {
                    instanceMenuMusic.Stop();
                    instanceElevatorMusic.Stop();
                }

                if (Global.currentScene == Global.world.scenes[Scenes.ElevatorMenu])
                {
                    instanceElevatorMusic.Play();
                    instanceElevatorMusic.IsLooped = true;
                }
                if (instanceMenuMusic.State == SoundState.Stopped && inMenu && Global.currentScene != Global.world.scenes[Scenes.ElevatorMenu])
                {
                    instanceMenuMusic.Play();
                }
                if (instanceGameMusic.State == SoundState.Stopped && !inMenu)
                {
                    instanceGameMusic.Play();
                }
            }

        }
    }
}
