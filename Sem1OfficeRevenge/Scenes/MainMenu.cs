using System;
using Microsoft.Xna.Framework;

namespace Sem1OfficeRevenge
{
    public class MainMenu : Scene
    {

        private Button playBtn;
        private Button settingsBtn;
        private Button quitBtn;
        private bool showSettings;

        private Button musicBtn;
        private Button resolutionBtn;
        private Button backBtn;

        public override void Initialize()
        {
            //Global.world.Fullscreen();
            InitMainMenu();
            InitSettingsMenu();
        }
        
        private void InitMainMenu()
        {
            playBtn = new Button(Global.world.camera.Center + new Vector2(0, -100),
                                 "Start Game",
                                 true,
                                 PlayGame);

            settingsBtn = new Button(Global.world.camera.Center,
                                 "Settings",
                                 true,
                                 Settings);

            quitBtn = new Button(Global.world.camera.Center + new Vector2(0, 100),
                                 "Quit",
                                 true,
                                 QuitGame);

            Global.currentScene.Instantiate(playBtn);
            Global.currentScene.Instantiate(settingsBtn);
            Global.currentScene.Instantiate(quitBtn);
        }

        private void InitSettingsMenu()
        {
            resolutionBtn = new Button(Global.world.camera.Center + new Vector2(0, -100),
                                 "Resolution",
                                 true,
                                 ChangeResolution);
            resolutionBtn.isVisible = false;

            musicBtn = new Button(Global.world.camera.Center,
                                 "Music",
                                 true,
                                 ChangeMusic);
            musicBtn.isVisible = false;

            backBtn = new Button(Global.world.camera.Center + new Vector2(0, 200),
                                 "Back",
                                 true,
                                 Settings);
            backBtn.isVisible = false;

            Global.currentScene.Instantiate(resolutionBtn);
            Global.currentScene.Instantiate(musicBtn);
            Global.currentScene.Instantiate(backBtn);
        }

        private void PlayGame()
        {
            Global.world.ChangeScene(Scenes.LoadingScreen);
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

        private void ChangeResolution()
        {

        }

        private void ChangeMusic() 
        { 
        
        }

        private void DrawSettingsMenu()
        {
            playBtn.isVisible = false;
            settingsBtn.isVisible = false;
            quitBtn.isVisible = false;

            resolutionBtn.isVisible = true;
            musicBtn.isVisible = true;
            backBtn.isVisible = true;
        }

        private void RemoveSettingsMenu()
        {
            playBtn.isVisible = true;
            settingsBtn.isVisible = true;
            quitBtn.isVisible = true;

            resolutionBtn.isVisible = false;
            musicBtn.isVisible = false;
            backBtn.isVisible = false;
        }

        private void QuitGame()
        {
            Global.world.Exit();
        }

    }
}
