using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Sem1OfficeRevenge
{
    public class BlackScreenFadeInOut: Gui
    {
        public float fadeInTime = 0.5f;
        public float fadeOutTime = 2f;
        private float timer;
        private float fadeAlpha = 0f; // Start with a transparent screen
        private Rectangle blackScreenSize;
        private bool isFadingIn = true; // Start with the fade-in effect
        public BlackScreenFadeInOut()
        {
            position = Vector2.Zero;
            blackScreenSize = new Rectangle(0, 0, Global.graphics.PreferredBackBufferWidth, Global.graphics.PreferredBackBufferHeight);
        }

        public override void Draw()
        {
            base.Draw();
            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pixel],
                        position,
                        blackScreenSize,
                        new Color(Color.Black, fadeAlpha),
                        rotation,
                        Vector2.Zero,
                        scale,
                        SpriteEffects.None,
                        Global.currentScene.GetObjectLayerDepth(LayerDepth.FullOverlay));
        }

        public override void Update()
        {
            base.Update();
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
                }
            }
            else
            {
                // Calculate the new alpha value for the fade-out effect
                fadeAlpha = MathHelper.Lerp(1f, 0f, timer / fadeOutTime);

            }

            // Clamp the alpha value between 0 and 1
            fadeAlpha = MathHelper.Clamp(fadeAlpha, 0f, 1f);
        }
    }
}
