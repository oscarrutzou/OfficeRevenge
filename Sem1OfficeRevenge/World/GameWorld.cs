using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class GameWorld : Game
    {
        public Dictionary<Scenes, Scene> scenes { get; private set; }
        public Camera playerCamera { get; private set; }
        public Camera uiCamera { get; private set; }
        public MiniMapCam mapCamera { get; private set; }

        public BlackScreenFadeInOut blackScreenFadeInOut;
        public PauseScreen pauseScreen { get; private set; }
        
        public bool playerWon;

        public int curfloorLevel = 1;
        public int maxFloorLevels = 5;

        public Weapon pistol;
        public Weapon shotgun;
        public Weapon rifle;
        public Weapon currentWeapon;

        public GameWorld()
        {
            Global.world = this;
            Global.graphics = new GraphicsDeviceManager(this);
            scenes = new Dictionary<Scenes, Scene>();
            Global.currentSceneData = new SceneData();
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            Window.Title = "Office Revenge!";
        }

        protected override void Initialize()
        {
            Global.spriteBatch = new SpriteBatch(GraphicsDevice);

            playerCamera = new Camera(new Vector2(Global.graphics.PreferredBackBufferWidth / 2, Global.graphics.PreferredBackBufferHeight / 2))
            {
                followPlayer = true
            };
            uiCamera = new Camera(Vector2.Zero)
            {
                followPlayer = false
            };
            mapCamera = new MiniMapCam(Vector2.Zero);

            Fullscreen();
            GlobalTextures.LoadContent();
            GlobalSounds.LoadContent();
            GlobalAnimations.LoadLoadingScreenIcon();

            GenerateScenes();
            ChangeScene(Scenes.MainMenu);

            blackScreenFadeInOut = new BlackScreenFadeInOut();
            InitWeapons();
            base.Initialize();
        }


        protected override void LoadContent()
        {
            Global.spriteBatch = new SpriteBatch(GraphicsDevice);            
        }

        protected override void Update(GameTime gameTime)
        {
            Global.gameTime = gameTime;

            GlobalSounds.MusicUpdate();

            InputManager.HandleInput();
            
            Global.currentScene.Update();

            blackScreenFadeInOut?.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Draw in world objects
            Global.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, 
                transformMatrix: playerCamera.GetMatrix());

            Global.currentScene.DrawInWorld();
            Global.spriteBatch.End();

            //Draw on screen objects
            Global.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, BlendState.AlphaBlend, 
                SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, 
                transformMatrix: uiCamera.GetMatrix());

            Global.currentScene.DrawOnScreen();
            blackScreenFadeInOut?.Draw(); //Can be it has not been set
            if (!IsCurrentSceneMenu()) pauseScreen.DrawOnScreen();
            Global.spriteBatch.End();

            //Draw minimap
            Global.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, 
                transformMatrix: mapCamera.GetMatrix());
            if (!IsCurrentSceneMenu()) mapCamera.DrawMiniMap();
            Global.spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Scene and resolution management
        private void GenerateScenes()
        {
            //Add scenes to dictionary
            scenes[Scenes.MainMenu] = new MainMenu();
            scenes[Scenes.LoadingScreen] = new LoadingScene();
            scenes[Scenes.ElevatorMenu] = new ElevatorMenu();
            scenes[Scenes.EndMenu] = new EndMenu();
            scenes[Scenes.TestJasper] = new TestSceneJasper();
            scenes[Scenes.TestLeonard] = new TestSceneLeonard();
            scenes[Scenes.TestMarc] = new TestSceneMarc();
            scenes[Scenes.TestOscar] = new TestSceneOscar();
            scenes[Scenes.GameScene] = new GameScene();
            
        }

        public bool IsCurrentSceneMenu()
        {
            //Check if the current scene is a menu
            return Global.currentScene == scenes[Scenes.MainMenu] || Global.currentScene == scenes[Scenes.LoadingScreen] || Global.currentScene == scenes[Scenes.ElevatorMenu] || Global.currentScene == scenes[Scenes.EndMenu];
        }


        public async void ChangeScene(Scenes scene)
        {
            if (scenes[scene] == Global.currentScene) return;

            if (Global.currentSceneData != null && Global.currentScene != null)
            {
                if(scene != Scenes.LoadingScreen)
                {
                    //Start fade
                    blackScreenFadeInOut.StartFadeIn();
                    blackScreenFadeInOut.onFadeToBlackDone += (sender, e) => { blackScreenFadeInOut.StopAnimation(); };
                    blackScreenFadeInOut.onFadeFromBlackDone += (sender, e) => { blackScreenFadeInOut.StopAnimation(); };

                    // Wait for the fade-in transition to complete
                    await Task.Delay(blackScreenFadeInOut.fadeInTimeMillisec);

                }

                if (Global.player != null) Global.player = null;

                foreach (GameObject gameObject in Global.currentSceneData.gameObjects)
                {
                    gameObject.isRemoved = true;
                }

                if (scene != Scenes.LoadingScreen) blackScreenFadeInOut?.StartFadeOut();

            }

            Global.currentScene = scenes[scene];
            Global.currentScene.Initialize();
            Global.currentScene.isPaused = false;
            Global.currentScene.hasFadeOut = false;

            if (pauseScreen == null) pauseScreen = new PauseScreen(); //Skal være her, da pausescreen bruger gameobjects
            if (!IsCurrentSceneMenu()) pauseScreen.Initialize();
            
        }


        public void ResolutionSize(int width, int height)
        {
            Global.graphics.HardwareModeSwitch = true;
            Global.graphics.PreferredBackBufferWidth = width;
            Global.graphics.PreferredBackBufferHeight = height;
            Global.graphics.IsFullScreen = false;
            Global.graphics.ApplyChanges();

            playerCamera.origin = new Vector2(width / 2, height / 2);
        }

        public void Fullscreen()
        {
            Global.graphics.HardwareModeSwitch = false;
            Global.graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            Global.graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            Global.graphics.IsFullScreen = true;
            Global.graphics.ApplyChanges();

            playerCamera.origin = new Vector2(Global.graphics.PreferredBackBufferWidth / 2, Global.graphics.PreferredBackBufferHeight / 2);
        }


        #endregion

        private void InitWeapons()
        {
            pistol = new Pistol();
            rifle = new Rifle();
            shotgun = new Shotgun();
            currentWeapon = pistol;
        }

        public void RefreshWeapons()
        {
            pistol.RefreshGunAfterRun();
            rifle.RefreshGunAfterRun();
            shotgun.RefreshGunAfterRun();
        }

    }
}