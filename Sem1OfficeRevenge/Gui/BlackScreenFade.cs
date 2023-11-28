using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Sem1OfficeRevenge
{
    public class BlackScreenFade: Gui
    {
        public float fadeTime = 0.5f;
        private float timer;
        private float fadeAlpha = 0f; // Start with a transparent screen
        private Rectangle blackScreenSize;
        public bool shouldFade = true;
        private float fadeFrom = 0f;
        private float fadeTo = 1f;
        private bool deleteAfterAnim;
        public BlackScreenFade(float fadeTime, float fadeFrom, float fadeTo)
        {
            this.fadeFrom = fadeFrom;
            this.fadeTo = fadeTo;
            this.fadeTime = fadeTime;
            position = Vector2.Zero;
            blackScreenSize = new Rectangle(0, 0, Global.graphics.PreferredBackBufferWidth, Global.graphics.PreferredBackBufferHeight);
        }

        public BlackScreenFade(float fadeTime, float fadeFrom, float fadeTo, bool deleteAfterAnim)
        {
            this.fadeFrom = fadeFrom;
            this.fadeTo = fadeTo;
            this.fadeTime = fadeTime;
            this.deleteAfterAnim = deleteAfterAnim;
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

            if (!shouldFade) return;

            // Update the timer
            timer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;

            fadeAlpha = MathHelper.Lerp(fadeFrom, fadeTo, timer / fadeTime);

            // Clamp the alpha value between 0 and 1
            fadeAlpha = MathHelper.Clamp(fadeAlpha, 0f, 1f);

            if (deleteAfterAnim && fadeAlpha == fadeTo)
            {
                isRemoved = true;
            }
        }
    }
}
