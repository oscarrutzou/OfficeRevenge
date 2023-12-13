using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sem1OfficeRevenge
{
    public class TestBaseScene : Scene
    {
        public LevelGeneration lvlGen;
        bool pressed = false;
        PauseScreen pauseScreen;
        public TestBaseScene()
        {
            
        }

        public override void Initialize()
        {
            GlobalSounds.inMenu = false;

            //Level Generation
            lvlGen = new LevelGeneration();
            if (Global.world.curfloorLevel == 1) lvlGen.GenerateWorld();
            else lvlGen.GenerateSecondWorld();


            //Player Generation
            Global.player = new Player();
            Global.player.centerOrigin = true;
            Global.currentScene.Instantiate(Global.player);
            if (Global.world.curfloorLevel != 1) Global.player.position = lvlGen.elevator.collisionBox.Center.ToVector2();
        }

        public override void Update()
        {
            ScoreManager.UpdateScore();

            base.Update();
        }

        public override void DrawOnScreen()
        {
            base.DrawOnScreen();
            
            ScoreManager.DrawScore();
            DrawAmmo();
        }

        private void DrawAmmo()
        {
            string text = $"Ammo {Global.world.currentWeapon.ammo}/{Global.world.currentWeapon.magSize}";

            Global.spriteBatch.DrawString(GlobalTextures.defaultFont,
                                  text,
                                  Global.world.uiCamera.BottomLeft + new Vector2(10, -50),
                                  Color.Gray,
                                  0,
                                  Vector2.Zero,
                                  1,
                                  SpriteEffects.None,
                                  Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }
    }
}
