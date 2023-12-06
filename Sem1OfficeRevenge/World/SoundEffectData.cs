using Microsoft.Xna.Framework.Audio;

namespace Sem1OfficeRevenge
{
    public class SoundEffectData
    {
        public SoundEffectInstance Instance { get; set; }
        public SoundEffect Sound { get; set; }

        public SoundEffectData(SoundEffectInstance instance, SoundEffect sound)
        {
            Instance = instance;
            Sound = sound;
        }
    }
}
