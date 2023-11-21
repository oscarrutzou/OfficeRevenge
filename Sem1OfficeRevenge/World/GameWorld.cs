﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sem1OfficeRevenge
{
    public class GameWorld : Game
    {
        private Scene[] scenes = new Scene[5];
        private int activeSceneIndex;

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
            GenerateScenes();
            activeSceneIndex = 4;
            Global.currentScene = scenes[activeSceneIndex];
            scenes[activeSceneIndex].Initialize();
            base.Initialize();
        }



        protected override void LoadContent()
        {
            Global.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Global.gameTime = gameTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Update Inputmanager handleinput

            scenes[activeSceneIndex].Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            scenes[activeSceneIndex].Draw();
            // TODO: Add your drawing code here

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
    }
}