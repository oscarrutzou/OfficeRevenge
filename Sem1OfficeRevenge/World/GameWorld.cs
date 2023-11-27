using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Drawing;

namespace Sem1OfficeRevenge
{
    public class GameWorld : Game
    {
        private Dictionary<Scenes, Scene> scenes = new Dictionary<Scenes, Scene>();
        private int activeSceneIndex;
        public Camera camera;
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
            WindowedScreen();
            GlobalTextures.LoadContent();
            GlobalAnimations.LoadLoadingScreenIcon();
            GlobalAnimations.LoadContent();

            //camera = new Camera(new Vector2(Global.graphics.PreferredBackBufferWidth / 2, Global.graphics.PreferredBackBufferHeight / 2));
            camera = new Camera(Vector2.Zero);

            
            GenerateScenes();
            ChangeScene(Scenes.TestLeonard);
            

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
                camera.FollowPlayerMove(Global.player.position);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Global.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, transformMatrix: Global.world.camera.GetMatrix());
            Global.currentScene.DrawInWorld();
            Global.spriteBatch.End();

            Global.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack);
            Global.currentScene.DrawOnScreen();
            Global.spriteBatch.End();

            base.Draw(gameTime);
        }

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

        public void Fullscreen()
        {
            Global.graphics.HardwareModeSwitch = false;
            Global.graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            Global.graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            Global.graphics.IsFullScreen = true;
            Global.graphics.ApplyChanges();
        }

    }
}