using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sem1OfficeRevenge
{
    public class Button: Gui
    {
        private float maxScale = 1f;
        private float clickCooldown = 0.1f; // The delay between button clicks in seconds
        private float timeSinceLastClick = 0; // The time since the button was last clicked
        private bool invokeActionOnFullScale;
        private bool hasPressed;
        public Color textColor = Color.Black;

        public Button(Vector2 position, string text, Texture2D texture, bool invokeActionOnFullScale, Action onClick)
        {
            this.position = position;
            this.text = text;
            this.texture = texture;
            this.onClick = onClick;
            this.invokeActionOnFullScale = invokeActionOnFullScale;
            centerOrigin = true;
        }

        public Button(Vector2 position, string text, bool invokeActionOnFullScale, Action onClick)
        {
            this.position = position;
            this.text = text;
            texture = GlobalTextures.textures[TextureNames.GuiButtonTest];
            this.invokeActionOnFullScale = invokeActionOnFullScale;
            this.onClick = onClick;
            centerOrigin = true;
        }

        public Button(string text, bool invokeActionOnFullScale, Action onClick)
        {
            this.position = Vector2.Zero;
            this.text = text;
            texture = GlobalTextures.textures[TextureNames.GuiButtonTest];
            this.invokeActionOnFullScale = invokeActionOnFullScale;
            this.onClick = onClick;
            centerOrigin = true;
        }

        public override void Update()
        {
            if (timeSinceLastClick < clickCooldown)
            {
                timeSinceLastClick += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            }


            if (!IsMouseOver() || InputManager.mouseState.LeftButton == ButtonState.Released)
            {
                // Increase the scale by 1% each frame, up to the original size
                scale = new Vector2(Math.Min(maxScale, scale.X + 0.01f), Math.Min(maxScale, scale.Y + 0.01f));

                if (!isVisible) return;
                if (!invokeActionOnFullScale) return;
                if (!hasPressed) return;
                if (scale.X != maxScale) return;

                onClick?.Invoke();
                hasPressed = false;
            }
        }

        // Check if the mouse is over the button
        public bool IsMouseOver()
        {
            return collisionBox.Contains(InputManager.mousePositionOnScreen.ToPoint());
        }

        // Called when the left mouse button is pressed
        public void OnClick()
        {
            if (!isVisible) return;

            scale = new Vector2(0.95f, 0.95f);  // Shrink the button

            if (timeSinceLastClick >= clickCooldown)
            {
                timeSinceLastClick = 0; 

                if (invokeActionOnFullScale)
                    hasPressed = true;
                else
                    onClick?.Invoke();
            }
        }

        public override void Draw()
        {
            base.Draw();
            DrawText();
        }

        private void DrawText()
        {
            if (!isVisible) return;

            // Measure the size of the text
            Vector2 textSize = GlobalTextures.defaultFont.MeasureString(text);

            // Calculate the position to center the text
            Vector2 textPosition = position - textSize / 2;

            Global.spriteBatch.DrawString(GlobalTextures.defaultFont,
                                              text,
                                              textPosition,
                                              textColor,
                                              0,
                                              Vector2.Zero,
                                              1,
                                              SpriteEffects.None,
                                              Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }


    }
}
