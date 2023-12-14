using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sem1OfficeRevenge
{
    public class BlackScreenFadeInOut : Gui
    {
        public float fadeInTime = 1f;
        public int fadeInTimeMillisec = 800; //For the scenechange in gameworld.
        public float fadeOutTime = 2f; //To lerp towards
        private float timer;
        private float fadeAlpha = 0f; // Start with a transparent screen
        public bool beginAnimation = false;
        private bool isFadingIn;
        public EventHandler<EventArgs> onFadeToBlackDone;
        public EventHandler<EventArgs> onFadeFromBlackDone;
        public BlackScreenFadeInOut()
        {
            position = Vector2.Zero;
            texture = GlobalTextures.textures[TextureNames.Pixel];
            isVisible = false;
            isFadingIn = true;
        }

        public override void Draw()
        {
            base.Draw();

            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pixel],
                        position,
                        Global.graphics.GraphicsDevice.Viewport.Bounds,
                        new Color(Color.Black, fadeAlpha),
                        rotation,
                        Vector2.Zero,
                        scale,
                        SpriteEffects.None,
                        Global.currentScene.GetObjectLayerDepth(LayerDepth.FullOverlay));
        }

        public void StartFadeIn()
        {
            isVisible = true;
            beginAnimation = true;
            isFadingIn = true;
            timer = 0f;
        }

        public void StartFadeOut()
        {
            isVisible = false;
            beginAnimation = true;
            isFadingIn = false;
            timer = 0f;
        }

        public void StopAnimation()
        {
            beginAnimation = false;
        }

        public override void Update()
        {
            base.Update();

            if (!beginAnimation) return;

            // Update the timer
            timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;

            if (isFadingIn)
            {
                // Calculate the new alpha value for the fade-in effect
                fadeAlpha = MathHelper.Lerp(0f, 1f, timer / fadeInTime);

                // If the screen is fully opaque, switch to the fade-out effect
                if (fadeAlpha >= 1f)
                {
                    isFadingIn = false;
                    timer = 0f; // Reset the timer
                    onFadeToBlackDone?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                // Calculate the new alpha value for the fade-out effect
                fadeAlpha = MathHelper.Lerp(1f, 0f, timer / fadeOutTime);

                if (fadeAlpha <= 0f)
                {
                    onFadeFromBlackDone?.Invoke(this, EventArgs.Empty);
                }
            }

            // Clamp the alpha value between 0 and 1
            fadeAlpha = MathHelper.Clamp(fadeAlpha, 0f, 1f);


        }
    }
}
