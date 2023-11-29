using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Sem1OfficeRevenge.Content.UI;
using Sem1OfficeRevenge.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using SharpDX.Direct3D9;

namespace Sem1OfficeRevenge
{
    public class TestBaseScene : Scene
    {
        LevelGeneration lvlGen;
        bool pressed = false;

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

        public override void DrawInWorld()
        {

            ScoreManager.Draw(new Vector2(10, 10));
            base.DrawInWorld();
            Global.graphics.GraphicsDevice.Clear(Color.Black);
        }

        public override void Update()
        {
            KeyboardState state = Keyboard.GetState();

            //check if key is pressed
            //if (state.IsKeyDown(Keys.Space))
            //{
            //    lvlGen.RemoveRooms();
            //    lvlGen.GenerateWorld();
            //}

            ScoreManager.UpdateScore();

            if (state.IsKeyDown(Keys.R) && pressed == false)
            {
                pressed = true;
                Application.Restart();
                //Global.world.ChangeScene(Scenes.TestBaseScene);
            }

            if (state.IsKeyDown(Keys.LeftShift))
            {
                Global.player.playerSpeed = 15;
            }
            else
            {
                Global.player.playerSpeed = 7;
            }

            base.Update();
        }
    }
}
