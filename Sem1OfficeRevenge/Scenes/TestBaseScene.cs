using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.Direct3D9;
using Microsoft.Xna.Framework.Graphics;
using static System.Net.Mime.MediaTypeNames;

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
            GlobalSound.inMenu = false;

            //Level Generation
            lvlGen = new LevelGeneration();
            lvlGen.GenerateWorld();

            //Player Generation
            Global.player = new Player();
            Global.player.centerOrigin = true;
            Global.currentScene.Instantiate(Global.player);
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
            // Measure the size of the text
            //Vector2 textSize = GlobalTextures.defaultFont.MeasureString(text);

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
