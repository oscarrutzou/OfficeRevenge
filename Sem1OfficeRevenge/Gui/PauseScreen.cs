using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public class PauseScreen
    {
        #region Variables
        public bool hasInit;

        private Button pauseBtn;
        private Button mainMenuBtn;
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

        public void Initialize()
        {
            InitDefaultMenu();
            InitSettingsMenu();
            HidePauseMenu();

            WorldOnResolutionChanged();
        }

        public void HidePauseMenu()
        {
            pauseBtn.isVisible = false;
            settingsBtn.isVisible = false;
            quitBtn.isVisible = false;
            mainMenuBtn.isVisible = false;

            resolutionBtn.isVisible = false;
            sfxSlider.isVisible = false;
            musicSlider.isVisible = false;
            backBtn.isVisible = false;
            showSettings = false;
        }

        public void ShowPauseMenu()
        {
            pauseBtn.isVisible = true;
            settingsBtn.isVisible = true;
            quitBtn.isVisible = true;
            mainMenuBtn.isVisible = true;

            resolutionBtn.isVisible = false;
            sfxSlider.isVisible = false;
            musicSlider.isVisible = false;
            backBtn.isVisible = false;
        }

        #region Default Menu
        private void InitDefaultMenu()
        {
            pauseBtn = new Button(
                                 "Pause",
                                 true,
                                 PlayGame);
            mainMenuBtn = new Button(
                                 "Main Menu",
                                 true,
                                 MainMenuBack);
            settingsBtn = new Button(
                                 "Settings",
                                 true,
                                 Settings);

            quitBtn = new Button(
                                 "Quit",
                                 true,
                                 QuitGame);

            Global.currentScene.Instantiate(pauseBtn);
            Global.currentScene.Instantiate(mainMenuBtn);
            Global.currentScene.Instantiate(settingsBtn);
            Global.currentScene.Instantiate(quitBtn);
        }
        private void PlayGame()
        {
            HidePauseMenu();
            Global.currentScene.isPaused = false;
            showSettings = false;
        }
        private void MainMenuBack()
        {
            Global.world.ChangeScene(Scenes.MainMenu);
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
                ShowPauseMenu();
                showSettings = false;
            }
        }
        private void DrawSettingsMenu()
        {
            pauseBtn.isVisible = false;
            settingsBtn.isVisible = false;
            quitBtn.isVisible = false;
            mainMenuBtn.isVisible = false;

            resolutionBtn.isVisible = true;
            musicSlider.isVisible = true;
            sfxSlider.isVisible = true;
            backBtn.isVisible = true;
        }
        #endregion

        #region Setting Resolution
        private void WorldOnResolutionChanged()
        {
            pauseBtn.position = Global.world.uiCamera.Center + new Vector2(0, -85);
            mainMenuBtn.position = Global.world.uiCamera.Center;
            settingsBtn.position = Global.world.uiCamera.Center + new Vector2(0, 85);
            quitBtn.position = Global.world.uiCamera.Center + new Vector2(0, 170);

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
        public void DrawOnScreen()
        {
            DrawResolutionText();
            DrawSfxText();
            DrawMusicText();
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
                                  new Color(195, 195, 195),
                                  0,
                                  Vector2.Zero,
                                  1,
                                  SpriteEffects.None,
                                  Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }

        private void DrawSfxText()
        {
            if (!sfxSlider.isVisible) return;

            float volume = (float)Math.Round(GlobalSound.sfxVolume * 100, 0);
            string text = $"Sfx volume {volume}%";

            Global.spriteBatch.DrawString(GlobalTextures.defaultFont,
                                  text,
                                  sfxSlider.position + new Vector2(-170, -50),
                                  new Color(195, 195, 195),
                                  0,
                                  Vector2.Zero,
                                  1,
                                  SpriteEffects.None,
                                  Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }

        private void DrawMusicText()
        {
            if (!musicSlider.isVisible) return;

            float volume = (float)Math.Round(GlobalSound.musicVolume * 100, 0);
            string text = $"Music volume {volume}%";

            Global.spriteBatch.DrawString(GlobalTextures.defaultFont,
                                  text,
                                  musicSlider.position + new Vector2(-170, -50),
                                  new Color(195, 195, 195),
                                  0,
                                  Vector2.Zero,
                                  1,
                                  SpriteEffects.None,
                                  Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }
        #endregion

    }
}
