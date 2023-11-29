using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    public class Animation
    {
        public List<Texture2D> frames;
        public int currentFrame;
        public Action onAnimationDone;


        public float frameRate = 20f;
        private float frameDuration;
        private float timer;

        public Animation(List<Texture2D> frames)
        {
            this.frames = frames;
            currentFrame = 0;
        }

        public void AnimationUpdate()
        {
            frameDuration = 1f / frameRate;

            timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > frameDuration)
            {
                timer -= frameDuration;
                currentFrame = (currentFrame + 1) % frames.Count;

                CheckAnimationDone();
            }
        }

        private void CheckAnimationDone()
        {
            if (currentFrame == frames.Count -1)
            {
                onAnimationDone?.Invoke();
            }
        }
    }
}
