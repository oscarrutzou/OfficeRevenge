using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public class MainMenu : Scene
    {

        private Button playBtn;
        private Vector2 playBtnPos;
        private Button settingsBtn;
        private Vector2 settingsBtnPos;
        private Button quitBtn;
        private Vector2 quitBtnPos;

        private bool showSettings;

        private Button musicBtn;
        private Vector2 musicBtnPos;
        private Button resolutionBtn;
        private Vector2 resolutionBtnPos;
        private Button backBtn;
        private Vector2 backBtnPos;

        public override void Initialize()
        {
            //Global.world.Fullscreen();
            InitMainMenu();
            InitSettingsMenu();
            Global.world.OnResolutionChanged += WorldOnResolutionChanged;
            WorldOnResolutionChanged(this, new ResolutionChangedEventArgs { Width = Global.graphics.PreferredBackBufferWidth, Height = Global.graphics.PreferredBackBufferHeight });
        }
        
        private int index = 1;

        //Lav sort skærm, ændre pos, fade ud til normal skærm. 

        private void ChangeResolution()
        {
            switch (index)
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
            index++;
            if (index == 3) index = 0;
        }

        private void WorldOnResolutionChanged(object sender, ResolutionChangedEventArgs e)
        {
            playBtn.position = Global.world.camera.Center + new Vector2(0, -100);
            settingsBtn.position = Global.world.camera.Center;
            quitBtn.position = Global.world.camera.Center + new Vector2(0, 100);

            resolutionBtn.position = Global.world.camera.Center + new Vector2(0, -100);
            musicBtn.position = Global.world.camera.Center;
            backBtn.position = Global.world.camera.Center + new Vector2(0, 200);
        }
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

        private void InitSettingsMenu()
        {
            resolutionBtn = new Button(
                                 "Resolution",
                                 true,
                                 ChangeResolution);
            resolutionBtn.isVisible = false;

            musicBtn = new Button(
                                 "Music",
                                 true,
                                 ChangeMusic);
            musicBtn.isVisible = false;

            backBtn = new Button(
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
