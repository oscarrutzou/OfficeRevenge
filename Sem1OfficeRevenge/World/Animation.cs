using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    public class Animation
    {
        public AnimNames animationName { get; private set; }
        public List<Texture2D> frames { get; private set; }
        public int currentFrame { get; private set; }
        public Action onAnimationDone;
        public float frameRate = 20f;
        private float frameDuration;
        private float timer;

        public Animation(List<Texture2D> frames, AnimNames animationName)
        {
            this.frames = frames;
            currentFrame = 0;
            this.animationName = animationName;
        }

        
        public void AnimationUpdate()
        {
            // Calculate the frame duration based on the frame rate
            frameDuration = 1f / frameRate;

            // Add the elapsed time since the last frame to the timer
            timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > frameDuration)
            {
                timer -= frameDuration;
                currentFrame = (currentFrame + 1) % frames.Count;
                CheckAnimationDone();
            }
        }

        //Check if animation is done
        private void CheckAnimationDone()
        {
            if (currentFrame == frames.Count - 1)
            {
                onAnimationDone?.Invoke();
            }
        }
    }
}
