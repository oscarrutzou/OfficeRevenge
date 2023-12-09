using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public enum SoundNames
    {
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
    internal static class GlobalSound
    {

        public static Dictionary<SoundNames, SoundEffect> sounds;
        public static Dictionary<SoundNames, List<SoundEffectInstance>> soundInstancesPool;
        private static int maxInstanceOfOneSound = 2;

        public static bool inMenu = true;

        public static SoundEffect MenuMusic;
        public static SoundEffect GameMusic;
        public static SoundEffect ElevatorMusic;

        public static SoundEffectInstance InstanceMenuMusic;
        public static SoundEffectInstance InstanceGameMusic;
        public static SoundEffectInstance InstanceElevatorMusic;

        public static float musicVolume = 1f;
        public static float sfxVolume = 1f;
        private static int musicVolDivide = 4; //Makes the song less loud by dividing the real volume

        public static List<SoundEffectData> soundInstances { get; private set; }


        public static void LoadContent()
        {
            soundInstances = new List<SoundEffectData>();
            soundInstancesPool = new Dictionary<SoundNames, List<SoundEffectInstance>>();

            MenuMusic = Global.world.Content.Load<SoundEffect>("Fonts\\MainTheme");
            GameMusic = Global.world.Content.Load<SoundEffect>("Fonts\\DistortedTheme");
            ElevatorMusic = Global.world.Content.Load<SoundEffect>("Sounds\\ElevatorTheme");

            sounds = new Dictionary<SoundNames, SoundEffect>
            {
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

            foreach (var sound in sounds)
            {
                soundInstancesPool[sound.Key] = new List<SoundEffectInstance>();
                for (int i = 0; i < maxInstanceOfOneSound; i++)
                {
                    soundInstancesPool[sound.Key].Add(sound.Value.CreateInstance());
                }
            }


        }

        public static bool IsAnySoundPlaying(SoundNames[] soundArray)
        {
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
            int soundIndex = Global.rnd.Next(0, soundArray.Length);
            int index = 0;

            SoundNames soundName = soundArray[soundIndex];

            foreach (SoundEffectInstance inst in soundInstancesPool[soundName])
            {
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

        public static void PlayRandomSound(SoundNames[] soundArray, int maxAmountPlaying, float floatSoundVolDivided)
        {
            int soundIndex = Global.rnd.Next(0, soundArray.Length);
            int index = 0;

            SoundNames soundName = soundArray[soundIndex];

            foreach (SoundEffectInstance inst in soundInstancesPool[soundName])
            {
                if (inst.State == SoundState.Playing)
                {
                    index++;
                }
            }

            if (index >= maxAmountPlaying)
            {
                return;
            }

            PlaySound(soundName, floatSoundVolDivided);
        }

        public static void PlaySound(SoundNames soundName, float floatSoundVolDivided)
        {
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
            if (InstanceGameMusic == null || InstanceMenuMusic == null || InstanceElevatorMusic == null)
            {
                InstanceMenuMusic = MenuMusic.CreateInstance();
                InstanceGameMusic = GameMusic.CreateInstance();
                InstanceElevatorMusic = ElevatorMusic.CreateInstance();
            }
            else
            {
                InstanceMenuMusic.Volume = Math.Clamp(musicVolume, 0, 1) / musicVolDivide;
                InstanceGameMusic.Volume = Math.Clamp(musicVolume, 0, 1) / musicVolDivide;
                InstanceElevatorMusic.Volume = Math.Clamp(musicVolume, 0, 1) / musicVolDivide;

                
                if (inMenu)
                {
                    InstanceGameMusic.Stop();
                }
                else
                {
                    InstanceMenuMusic.Stop();
                    InstanceElevatorMusic.Stop();
                }

                if (Global.currentScene == Global.world.scenes[Scenes.ElevatorMenu])
                {
                    InstanceElevatorMusic.Play();
                    InstanceElevatorMusic.IsLooped = true;
                }
                if (InstanceMenuMusic.State == SoundState.Stopped && inMenu && Global.currentScene != Global.world.scenes[Scenes.ElevatorMenu])
                {
                    InstanceMenuMusic.Play();
                }
                if (InstanceGameMusic.State == SoundState.Stopped && !inMenu)
                {
                    InstanceGameMusic.Play();
                }
            }

        }
    }
}
