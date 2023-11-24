﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sem1OfficeRevenge
{
    public class GameWorld : Game
    {
        private Scene[] scenes = new Scene[5];
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
            GlobalAnimations.LoadContent();

            Global.spriteBatch = new SpriteBatch(GraphicsDevice);

            GenerateScenes();
            activeSceneIndex = 1;
            Global.currentScene = scenes[activeSceneIndex];
            camera = new Camera(new Vector2(Global.graphics.PreferredBackBufferWidth / 2, Global.graphics.PreferredBackBufferHeight / 2));
            scenes[activeSceneIndex].Initialize();


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

            scenes[activeSceneIndex].Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Global.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, transformMatrix: Global.world.camera.GetMatrix());
            scenes[activeSceneIndex].DrawInWorld();
            Global.spriteBatch.End();

            Global.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack);
            scenes[activeSceneIndex].DrawOnScreen();
            Global.spriteBatch.End();

            base.Draw(gameTime);
        }

        private void GenerateScenes()
        {
            scenes[0] = new TestBaseScene();
            scenes[1] = new TestSceneJasper();
            scenes[2] = new TestSceneLeonard();
            scenes[3] = new TestSceneMarc();
            scenes[4] = new TestSceneOscar();
        }

        private void WindowedScreen()
        {
            Global.graphics.HardwareModeSwitch = true;
            Global.graphics.PreferredBackBufferWidth = 1280;
            Global.graphics.PreferredBackBufferHeight = 720;
            //camera.SetOrigin(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2));
            Global.graphics.IsFullScreen = false;
            Global.graphics.ApplyChanges();
        }

    }
}