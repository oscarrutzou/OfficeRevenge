using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Sem1OfficeRevenge
{
    public class SoundSlider : Gui
    {
        private int sliderBarLength = 338;
        private int sliderBarHeight = 24;
        private Vector2 fillOffset = new Vector2(12, 7);
        private Rectangle sliderRectangle;
        private bool isDragging = false;
        private Texture2D handleTexture;
        private Vector2 handlePosition;
        private Vector2 fillPosition;
        public float delayTimer = 0f;
        private float delayDuration = 0.1f;

        private bool _isVisible;
        public override bool isVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                if (_isVisible)
                {
                    // Reset the delay timer when the MusicSlider becomes visible
                    delayTimer = 0f;
                }
            }
        }
        public SoundSlider(Vector2 position)
        {
            centerOrigin = true;
            this.position = position;
            ChangeSliderRectangle(position);
            texture = GlobalTextures.textures[TextureNames.GuiSliderBase];
            handleTexture = GlobalTextures.textures[TextureNames.GuiSliderHandle];
        }

        public void ChangeSliderRectangle(Vector2 position)
        {
            sliderRectangle = new Rectangle((int)position.X + (int)fillOffset.X, (int)position.Y + (int)fillOffset.Y, sliderBarLength, sliderBarHeight);
            fillPosition = position + fillOffset;
        }

        public override void Update()
        {
            // Update the delay timer
            if (delayTimer < delayDuration)
            {
                delayTimer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
                return;
            }

            // Check if the left mouse button is pressed and the mouse is over the slider
            if (InputManager.mouseState.LeftButton == ButtonState.Pressed && sliderRectangle.Contains(InputManager.mousePositionOnScreen.ToPoint()))
            {
                isDragging = true;
            }
            else if (InputManager.mouseState.LeftButton == ButtonState.Released)
            {
                isDragging = false;
            }

            // If dragging, update the music volume and handle position
            if (isDragging)
            {
                MediaPlayer.Volume = (InputManager.mousePositionOnScreen.X - sliderRectangle.X) / (float)sliderRectangle.Width;
            }
            handlePosition = new Vector2(sliderRectangle.X + MediaPlayer.Volume * sliderRectangle.Width, fillPosition.Y + handleTexture.Height / 2 - 2);

        }

        public override void Draw()
        {
            base.Draw();
            if (!isVisible) return;

            // Draw the filled rectangle (colored area)
            float musicWidth = MediaPlayer.Volume * sliderRectangle.Width;
            Rectangle musicFill = new Rectangle(sliderRectangle.X, sliderRectangle.Y, (int)musicWidth, sliderRectangle.Height);

            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pixel],
                                    fillPosition,
                                    musicFill,
                                    new Color(195,195,195),
                                    rotation,
                                    Vector2.Zero,
                                    scale,
                                    SpriteEffects.None,
                                    Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiObjects) + 0.001f);

            Vector2 slideOverPos = position - new Vector2(GlobalTextures.textures[TextureNames.GuiSliderBase].Width / 2, GlobalTextures.textures[TextureNames.GuiSliderBase].Height / 2);

            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.GuiSliderOver],
                                    slideOverPos,
                                    null,
                                    Color.White,
                                    rotation,
                                    Vector2.Zero,
                                    scale,
                                    SpriteEffects.None,
                                    Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiObjects) + 0.002f);

            Vector2 handleOrigin = new Vector2(handleTexture.Width / 2, handleTexture.Height / 2);

            Global.spriteBatch.Draw(handleTexture,
                                    handlePosition,
                                    null,
                                    Color.White,
                                    0f,
                                    handleOrigin,
                                    1f,
                                    SpriteEffects.None,
                                    Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiObjects) + 0.003f);

        }
    }


}
