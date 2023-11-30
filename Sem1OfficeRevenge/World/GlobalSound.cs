using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge.World
{
    public enum SoundNames
    {
    Shot,
    Step,
    TestSound3,
    }
    internal static class GlobalSound
    {

        public static Dictionary<SoundNames, SoundEffect> sounds;

        public static bool inMenu = true;

        public static SoundEffect MenuMusic;
        public static SoundEffect GameMusic;

        public static SoundEffectInstance InstanceMenuMusic;
        public static SoundEffectInstance InstanceGameMusic;


        private static List<SoundEffectInstance> soundInstances;
        

        public static void LoadContent()
        {
            soundInstances = new List<SoundEffectInstance>();

            MenuMusic = Global.world.Content.Load<SoundEffect>("Fonts\\MainTheme");
            GameMusic = Global.world.Content.Load<SoundEffect>("Fonts\\DistortedTheme");

            sounds = new Dictionary<SoundNames, SoundEffect>
            {
                { SoundNames.Shot, Global.world.Content.Load<SoundEffect>("Fonts\\gunshot")},
                { SoundNames.Step, Global.world.Content.Load<SoundEffect>("Fonts\\step")},
                { SoundNames.TestSound3, Global.world.Content.Load<SoundEffect>("Fonts\\DistortedTheme")}
            };

            

        }

        public static void Play(SoundEffect sound)
        {
            soundInstances.Add(sound.CreateInstance());
            soundInstances[soundInstances.Count - 1].Volume = MediaPlayer.Volume;
            soundInstances[soundInstances.Count - 1].Play();
            foreach (SoundEffectInstance item in soundInstances)
            {
                if (item.State == SoundState.Stopped)
                {
                    item.Dispose();
                }
            }
        }

        public static void PitchedPlay(SoundEffect sound, float pitchVariance)
        {
            soundInstances.Add(sound.CreateInstance());
            float pitch = (float)Global.rnd.NextDouble();
            pitch = pitch - (1-pitch);
            if (Global.rnd.Next(0, 10) > 5)
            {
                pitch = 0 - pitch;
            }
            soundInstances[soundInstances.Count - 1].Pitch = pitch;
            soundInstances[soundInstances.Count - 1].Volume = MediaPlayer.Volume;
            soundInstances[soundInstances.Count - 1].Play();
            foreach (SoundEffectInstance item in soundInstances)
            {
                if (item.State == SoundState.Stopped)
                {
                    item.Dispose();
                }
            }
        }

        public static void music() 
        {
            if (InstanceGameMusic == null ||InstanceMenuMusic == null)
            {
                InstanceMenuMusic = MenuMusic.CreateInstance();
                InstanceGameMusic = GameMusic.CreateInstance();
            }
            else
            {
                InstanceGameMusic.Volume = MediaPlayer.Volume;
                InstanceMenuMusic.Volume = MediaPlayer.Volume;

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



    }

    
}
