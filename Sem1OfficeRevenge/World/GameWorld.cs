using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using SharpDX.Direct3D9;
//using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    public class GameWorld : Game
    {
        public Dictionary<Scenes, Scene> scenes = new Dictionary<Scenes, Scene>();
        public Camera worldCamera { get; private set; }
        public Camera uiCamera { get; private set; }
        public BlackScreenFadeInOut blackScreenFadeInOut;

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

            worldCamera = new Camera(new Vector2(Global.graphics.PreferredBackBufferWidth / 2, Global.graphics.PreferredBackBufferHeight / 2));
            uiCamera = new Camera(Vector2.Zero);

            ResolutionSize(1280, 720);
            GlobalTextures.LoadContent();
            GlobalAnimations.LoadLoadingScreenIcon();
            //GlobalAnimations.LoadContentTestScenes();


            GenerateScenes();
            ChangeScene(Scenes.MainMenu);

            blackScreenFadeInOut = new BlackScreenFadeInOut();

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

            if (Global.player != null)
            {
                worldCamera.FollowPlayerMove();
            }
            blackScreenFadeInOut?.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Global.spriteBatch.Begin(
                sortMode: SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.None,
                RasterizerState.CullCounterClockwise,
                transformMatrix: worldCamera.GetMatrix());

            Global.currentScene.DrawInWorld();
            Global.spriteBatch.End();

            Global.spriteBatch.Begin(
                sortMode: SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.None,
                RasterizerState.CullCounterClockwise,
                transformMatrix: uiCamera.GetMatrix());
            Global.currentScene.DrawOnScreen();
            blackScreenFadeInOut?.Draw();

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
            if (Global.currentSceneData != null && Global.currentScene != null)
            {
                foreach (GameObject gameObject in Global.currentSceneData.gameObjects)
                {
                    gameObject.isRemoved = true;
                }

                if (Global.currentScene == Global.world.scenes[Scenes.MainMenu])
                {
                    Global.currentScene = scenes[scene];
                    Global.currentScene.Initialize();
                    return;
                }

                //Global.currentScene.Initialize();

                // Start the fade-in transition
                blackScreenFadeInOut.StartFadeIn();

                // Wait for the fade-in transition to complete
                blackScreenFadeInOut.onFadeToBlackDone += (sender, e) =>
                {
                    // Change the scene
                    Global.currentScene = scenes[scene];
                    Global.currentScene.Initialize();

                    // Keep the screen black
                    blackScreenFadeInOut.StopAnimation();
                };
            }
            else
            {
                // Change the scene
                Global.currentScene = scenes[scene];
                Global.currentScene.Initialize();
            }
        }

        public void ResolutionSize(int width, int height)
        {
            Global.graphics.HardwareModeSwitch = true;
            Global.graphics.PreferredBackBufferWidth = width;
            Global.graphics.PreferredBackBufferHeight = height;
            Global.graphics.IsFullScreen = false;
            Global.graphics.ApplyChanges();

            worldCamera.origin = new Vector2(width / 2, height / 2);
        }

        public void Fullscreen()
        {
            Global.graphics.HardwareModeSwitch = false;
            Global.graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            Global.graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            Global.graphics.IsFullScreen = true;
            Global.graphics.ApplyChanges();

            worldCamera.origin = new Vector2(Global.graphics.PreferredBackBufferWidth / 2, Global.graphics.PreferredBackBufferHeight / 2);
        }


        #endregion

    }
}