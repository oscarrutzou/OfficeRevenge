using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sem1OfficeRevenge
{
    public class GameWorld : Game
    {
        private Dictionary<Scenes, Scene> scenes = new Dictionary<Scenes, Scene>();
        public Camera worldCamera;
        public Camera uiCamera;

        public GameWorld()
        {
            Global.world = this;
            Global.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Office Revenge!";
        }

        protected override void Initialize()
        {
            Global.spriteBatch = new SpriteBatch(GraphicsDevice);

            WindowedScreen();
            GlobalTextures.LoadContent();
            GlobalAnimations.LoadLoadingScreenIcon();
            GlobalAnimations.LoadContentTestScenes();

            worldCamera = new Camera(new Vector2(Global.graphics.PreferredBackBufferWidth / 2, Global.graphics.PreferredBackBufferHeight / 2));
            uiCamera = new Camera(Vector2.Zero);

            GenerateScenes();
            ChangeScene(Scenes.TestMarc);
            
            base.Initialize();
        }



        protected override void LoadContent()
        {
            Global.spriteBatch = new SpriteBatch(GraphicsDevice);            
        }

        protected override void Update(GameTime gameTime)
        {
            Global.gameTime = gameTime;

            InputManager.HandleInput();
            
            Global.currentScene.Update();

            //if (Global.player != null)
            //{
            //    worldCamera.FollowPlayerMove(Global.player.position);
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Global.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, transformMatrix: worldCamera.GetMatrix());
            Global.currentScene.DrawInWorld();
            Global.spriteBatch.End();

            Global.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, transformMatrix: uiCamera.GetMatrix());
            Global.currentScene.DrawOnScreen();
            Global.spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Scene and resolution management
        private void GenerateScenes()
        {
            scenes[Scenes.MainMenu] = new MainMenu();
            scenes[Scenes.LoadingScreen] = new LoadingScene();
            scenes[Scenes.TestJasper] = new TestSceneJasper();
            scenes[Scenes.TestLeonard] = new TestSceneLeonard();
            scenes[Scenes.TestMarc] = new TestSceneMarc();
            scenes[Scenes.TestOscar] = new TestSceneOscar();
            
        }

        public void ChangeScene(Scenes scene)
        {
            if (Global.currentSceneData != null)
            {
                foreach (GameObject gameObject in Global.currentSceneData.gameObjects)
                {
                    gameObject.isRemoved = true;
                }
            }

            Global.currentScene = scenes[scene];
            Global.currentScene.Initialize();
        }

        private void WindowedScreen()
        {
            Global.graphics.HardwareModeSwitch = true;
            Global.graphics.PreferredBackBufferWidth = 1280;
            Global.graphics.PreferredBackBufferHeight = 720;
            Global.graphics.IsFullScreen = false;
            Global.graphics.ApplyChanges();
        }

        public event EventHandler<ResolutionChangedEventArgs> OnResolutionChanged;

        public void ResolutionSize(int width, int height)
        {
            Global.graphics.HardwareModeSwitch = true;
            Global.graphics.PreferredBackBufferWidth = width;
            Global.graphics.PreferredBackBufferHeight = height;
            Global.graphics.IsFullScreen = false;
            Global.graphics.ApplyChanges();

            OnResolutionChanged?.Invoke(this, new ResolutionChangedEventArgs { Width = width, Height = height });
        }

        public void Fullscreen()
        {
            Global.graphics.HardwareModeSwitch = false;
            Global.graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            Global.graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            Global.graphics.IsFullScreen = true;
            Global.graphics.ApplyChanges();

            OnResolutionChanged?.Invoke(this, new ResolutionChangedEventArgs { 
                                        Width = GraphicsDevice.DisplayMode.Width, 
                                        Height = GraphicsDevice.DisplayMode.Height });
            
        }


        #endregion

    }
}