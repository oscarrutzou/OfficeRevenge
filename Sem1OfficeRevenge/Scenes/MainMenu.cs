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
            backBtn = new Button(Global.world.camera.Center + new Vector2(0, 200),
                                 "Back",
                                 true,
                                 Settings);
            backBtn.isVisible = false;

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

        private void DrawSettingsMenu()
        {
            playBtn.isVisible = false;
            settingsBtn.isVisible = false;
            quitBtn.isVisible = false;

            backBtn.isVisible = true;
        }

        private void RemoveSettingsMenu()
        {
            playBtn.isVisible = true;
            settingsBtn.isVisible = true;
            quitBtn.isVisible = true;

            backBtn.isVisible = false;
        }

        private void QuitGame()
        {
            Global.world.Exit();
        }

    }
}
