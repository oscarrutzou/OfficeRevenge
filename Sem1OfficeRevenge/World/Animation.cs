using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    public class Animation
    {
        public List<Texture2D> frames;
        public int CurrentFrame;

        public Animation(List<Texture2D> frames)
        {
            this.frames = frames;
            CurrentFrame = 0;
        }
    }
}
