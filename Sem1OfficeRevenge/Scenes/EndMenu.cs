using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public class EndMenu : Scene
    {
        #region Variables
        private Button playAgainBtn;
        private Button settingsBtn;
        private Button quitBtn;

        private bool showSettings;

        private SoundSlider sfxSlider;
        private SoundSlider musicSlider;
        private Button resolutionBtn;
        private Button backBtn;

        private bool isChangingResolution = false;
        private int resolutionIndex = 0;
        private BlackScreenFadeInOut fadeInOutObj;
        #endregion

        public override void Initialize()
        {
            // Reset uiCamera's position and origin
            Global.world.uiCamera.position = Vector2.Zero;
            Global.world.uiCamera.origin = Vector2.Zero;

            InitMainMenu();
            InitSettingsMenu();

            WorldOnResolutionChanged();
        }

        #region Main Menu
        private void InitMainMenu()
        {
            playAgainBtn = new Button(
                                 "Play Again",
                                 true,
                                 PlayGame);

            settingsBtn = new Button(
                                 "Settings",
                                 true,
                                 Settings);
            quitBtn = new Button(
                                 "Quit",
                                 true,
                                 QuitGame);

            Global.currentScene.Instantiate(playAgainBtn);
            Global.currentScene.Instantiate(settingsBtn);
            Global.currentScene.Instantiate(quitBtn);
        }

        private void PlayGame()
        {
            Global.world.ChangeScene(Scenes.LoadingScreen);
            Global.world.curfloorLevel = 1;
            Global.world.playerWon = false;
        }

        private void QuitGame()
        {
            Global.world.Exit();
        }

        #endregion

        #region Setting Menu
        private void InitSettingsMenu()
        {
            resolutionBtn = new Button(
                                 "Resolution",
                                 true,
                                 ChangeResolution);
            resolutionBtn.isVisible = false;

            Vector2 sfxPos = Global.world.uiCamera.Center;
            sfxSlider = new SoundSlider(sfxPos, true);
            sfxSlider.isVisible = false;

            Vector2 musicPos = Global.world.uiCamera.Center + new Vector2(0, 85);
            musicSlider = new SoundSlider(musicPos, false);
            musicSlider.isVisible = false;

            backBtn = new Button(
                                 "Back",
                                 true,
                                 Settings);
            backBtn.isVisible = false;

            fadeInOutObj = new BlackScreenFadeInOut();
            fadeInOutObj.fadeInTimeMillisec = 500;
            fadeInOutObj.fadeOutTime = 1f;

            Global.currentScene.Instantiate(fadeInOutObj);
            Global.currentScene.Instantiate(resolutionBtn);
            Global.currentScene.Instantiate(sfxSlider);
            Global.currentScene.Instantiate(musicSlider);
            Global.currentScene.Instantiate(backBtn);
        }
        private void Settings()
        {
            if (!showSettings)
            {
                DrawSettingsMenu();
                showSettings = true;
            }
            else
            {
                RemoveSettingsMenu();
                showSettings = false;
            }
        }

        private void DrawSettingsMenu()
        {
            playAgainBtn.isVisible = false;
            settingsBtn.isVisible = false;
            quitBtn.isVisible = false;

            resolutionBtn.isVisible = true;
            sfxSlider.isVisible = true;
            musicSlider.isVisible = true;
            backBtn.isVisible = true;
        }

        private void RemoveSettingsMenu()
        {
            playAgainBtn.isVisible = true;
            settingsBtn.isVisible = true;
            quitBtn.isVisible = true;

            resolutionBtn.isVisible = false;
            sfxSlider.isVisible = false;
            musicSlider.isVisible = false;
            backBtn.isVisible = false;
        }
        #endregion

        #region Setting Resolution

        private void WorldOnResolutionChanged()
        {
            playAgainBtn.position = Global.world.uiCamera.Center + new Vector2(0, -85);
            settingsBtn.position = Global.world.uiCamera.Center;
            quitBtn.position = Global.world.uiCamera.Center + new Vector2(0, 85);

            resolutionBtn.position = Global.world.uiCamera.Center + new Vector2(0, -85);

            sfxSlider.position = Global.world.uiCamera.Center;
            sfxSlider.ChangeSliderRectangle(Global.world.uiCamera.Center);

            musicSlider.position = Global.world.uiCamera.Center + new Vector2(0, 85);
            musicSlider.ChangeSliderRectangle(Global.world.uiCamera.Center + new Vector2(0, 85));

            backBtn.position = Global.world.uiCamera.Center + new Vector2(0, 170);
        }



        private async void ChangeResolution()
        {
            // If a resolution change is already in progress, do nothing
            if (isChangingResolution) return;

            // Indicate that a resolution change is in progress
            isChangingResolution = true;

            resolutionIndex++;
            if (Global.graphics.IsFullScreen) 
                resolutionIndex = 0;


            // Start the fade-in transition
            fadeInOutObj.StartFadeIn();

            // Wait for the fade-in transition to complete
            await Task.Delay(fadeInOutObj.fadeInTimeMillisec);

            // Change the resolution
            switch (resolutionIndex)
            {
                case 0:
                    Global.world.ResolutionSize(1280, 720);
                    break;
                case 1:
                    Global.world.ResolutionSize(1920, 1080);
                    break;
                case 2:
                    Global.world.Fullscreen();
                    break;
            }

            // Update the button positions
            WorldOnResolutionChanged();

            // Start the fade-out transition
            fadeInOutObj.StartFadeOut();
            isChangingResolution = false;
        }

        #endregion

        #region Setting Text
        public override void  DrawOnScreen()
        {
            base.DrawOnScreen();
            DrawGameName();
            DrawResolutionText();
            DrawSfxText();
            DrawMusicText();
        }

        private void DrawGameName()
        {
            string text = "";
            if (Global.world.playerWon)
            {
                text = "You won :D";
            }
            else
            {
                text = "Loser :/";
            }
                
            // Measure the size of the text
            Vector2 textSize = GlobalTextures.defaultFontBig.MeasureString(text);

            // Calculate the position to center the text
            Vector2 textPosition = (playAgainBtn.position + new Vector2(0, -120)) - textSize / 2;

            Global.spriteBatch.DrawString(GlobalTextures.defaultFontBig,
                      text,
                      textPosition,
                      Color.Black,
                      0,
                      Vector2.Zero,
                      1,
                      SpriteEffects.None,
                      Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }

        private void DrawResolutionText()
        {
            if (!resolutionBtn.isVisible) return;

            string text;
            if (Global.graphics.IsFullScreen)
            {
                text = "Fullscreen";
            }
            else
            {
                text = $"Resolution {Global.graphics.PreferredBackBufferWidth}x{Global.graphics.PreferredBackBufferHeight}";
            }

            Global.spriteBatch.DrawString(GlobalTextures.defaultFont,
                                  text,
                                  resolutionBtn.position + new Vector2(-170, -70),
                                  Color.Black,
                                  0,
                                  Vector2.Zero,
                                  1,
                                  SpriteEffects.None,
                                  Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }

        private void DrawSfxText()
        {
            if (!sfxSlider.isVisible) return;

            float volume = (float)Math.Round(GlobalSounds.sfxVolume * 100, 0);
            string text = $"Sfx volume {volume}%";

            Global.spriteBatch.DrawString(GlobalTextures.defaultFont,
                                  text,
                                  sfxSlider.position + new Vector2(-170, -50),
                                  Color.Black,
                                  0,
                                  Vector2.Zero,
                                  1,
                                  SpriteEffects.None,
                                  Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }

        private void DrawMusicText()
        {
            if (!musicSlider.isVisible) return;

            float volume = (float)Math.Round(GlobalSounds.musicVolume * 100, 0);
            string text = $"Music volume {volume}%";

            Global.spriteBatch.DrawString(GlobalTextures.defaultFont,
                                  text,
                                  musicSlider.position + new Vector2(-170, -50),
                                  Color.Black,
                                  0,
                                  Vector2.Zero,
                                  1,
                                  SpriteEffects.None,
                                  Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }
        #endregion
    }
}
