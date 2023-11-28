using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

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
        private BlackScreenFadeInOut fadeOut;
        #endregion

        public override void Initialize()
        {
            Global.world.Fullscreen();
            InitMainMenu();
            InitSettingsMenu();

            Global.world.OnResolutionChanged += DrawBlackScreenOnResolutionChanged;
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
        private async void DrawBlackScreenOnResolutionChanged(object sender, ResolutionChangedEventArgs e)
        {
            if (fadeOut != null) fadeOut.isRemoved = true;

            fadeOut = new BlackScreenFadeInOut();
            Global.currentScene.Instantiate(fadeOut);
            
            await Task.Delay((int)fadeOut.fadeInTime * 1000);

            WorldOnResolutionChanged();
        }
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
        private void ChangeResolution()
        {
            
            resolutionIndex++;
            if (Global.graphics.IsFullScreen) resolutionIndex = 0;

            switch (resolutionIndex)
            {
                case 0:
                    Global.world.ResolutionSize(1280, 720);
                    break;
                case 1:
                    Global.world.ResolutionSize(1920, 1080);
                    break;
                //case 2:
                //    Global.world.ResolutionSize(2560, 1440);
                //    break;
                case 2:
                    Global.world.Fullscreen();
                    break;
            }
         
            
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
            if (resolutionIndex == 2)
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
