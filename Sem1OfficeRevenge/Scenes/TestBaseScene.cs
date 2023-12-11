using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.Direct3D9;

namespace Sem1OfficeRevenge
{
    public class TestBaseScene : Scene
    {
        LevelGeneration lvlGen;

        public override void Initialize()
        {
            GlobalSound.inMenu = false;

            //Level Generation
            lvlGen = new LevelGeneration();
            lvlGen.GenerateSecondWorld();

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
        }
    }
}
