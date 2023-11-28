using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct3D9;

namespace Sem1OfficeRevenge
{
    public class MainMenu : Scene
    {
        #region Variables
        private Button playBtn;
        private Button settingsBtn;
        private Button quitBtn;

        private bool showSettings;

        private SoundSlider musicSlider;
        private Button resolutionBtn;
        private Button backBtn;

        private int resolutionIndex = 0;
        private BlackScreenFadeInOut fadeInOutObj;
        #endregion

        public override void Initialize()
        {
            Global.world.Fullscreen();
            InitMainMenu();
            InitSettingsMenu();

            //Global.world.OnResolutionChanged += DrawBlackScreenOnResolutionChanged;


            WorldOnResolutionChanged();
        }

        #region Main Menu
        private void InitMainMenu()
        {
            playBtn = new Button(
                                 "Start Game",
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

            Global.currentScene.Instantiate(playBtn);
            Global.currentScene.Instantiate(settingsBtn);
            Global.currentScene.Instantiate(quitBtn);
        }
        private void PlayGame()
        {
            Global.world.ChangeScene(Scenes.LoadingScreen);
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


            musicSlider = new SoundSlider(Global.world.worldCamera.Center - new Vector2(GlobalTextures.textures[TextureNames.GuiSliderBase].Width / 2, GlobalTextures.textures[TextureNames.GuiSliderBase].Height / 2));
            musicSlider.isVisible = false;

            backBtn = new Button(
                                 "Back",
                                 true,
                                 Settings);
            backBtn.isVisible = false;

            fadeInOutObj = new BlackScreenFadeInOut();
            fadeInOutObj.fadeInTime = 1f;
            fadeInOutObj.fadeOutTime = 1f;

            Global.currentScene.Instantiate(fadeInOutObj);
            Global.currentScene.Instantiate(resolutionBtn);
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
            playBtn.isVisible = false;
            settingsBtn.isVisible = false;
            quitBtn.isVisible = false;

            resolutionBtn.isVisible = true;
            musicSlider.isVisible = true;
            backBtn.isVisible = true;
        }

        private void RemoveSettingsMenu()
        {
            playBtn.isVisible = true;
            settingsBtn.isVisible = true;
            quitBtn.isVisible = true;

            resolutionBtn.isVisible = false;
            musicSlider.isVisible = false;
            backBtn.isVisible = false;
        }
        #endregion

        #region Setting Resolution

        private void WorldOnResolutionChanged()
        {
            playBtn.position = Global.world.worldCamera.Center + new Vector2(0, -100);
            settingsBtn.position = Global.world.worldCamera.Center;
            quitBtn.position = Global.world.worldCamera.Center + new Vector2(0, 100);

            resolutionBtn.position = Global.world.worldCamera.Center + new Vector2(0, -100);
            musicSlider.position = Global.world.worldCamera.Center;
            musicSlider.ChangeSliderRectangle(Global.world.worldCamera.Center - new Vector2(GlobalTextures.textures[TextureNames.GuiSliderBase].Width / 2, GlobalTextures.textures[TextureNames.GuiSliderBase].Height / 2));
            backBtn.position = Global.world.worldCamera.Center + new Vector2(0, 100);

            
        }
        private bool isChangingResolution = false;

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
            await Task.Delay((int)fadeInOutObj.fadeInTime * 1000);

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
            DrawResolutionText();
            DrawMusicText();
        }

        private void DrawResolutionText()
        {
            if (!resolutionBtn.isVisible) return;

            //float volume = (float)Math.Round(MediaPlayer.Volume * 100, 0);
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

        private void DrawMusicText()
        {
            if (!musicSlider.isVisible) return;

            float volume = (float)Math.Round(MediaPlayer.Volume * 100, 0);
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
