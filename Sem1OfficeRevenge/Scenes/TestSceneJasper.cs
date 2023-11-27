using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class TestSceneJasper : Scene
    {
        //public List<Texture2D> maps = new List<Texture2D>();
        //public List<Room> rooms = new List<Room>();

        LevelGeneration lvlGen;
        public bool isDown = false;

        public TestSceneJasper()
        {
            
        }

        public override void Initialize()
        {
            lvlGen = new LevelGeneration();
            lvlGen.GenerateWorld();
            
            Global.graphics.IsFullScreen = false;
            Global.graphics.PreferredBackBufferWidth = 1920;
            Global.graphics.PreferredBackBufferHeight = 1080;
            Global.graphics.ApplyChanges();

            //startRoom = new Room(GlobalTextures.textures[TextureNames.TileMap1], MathHelper.PiOver2);
            //rooms.Add(startRoom);
            //Global.currentScene.Instantiate(startRoom);
        }

        public override void DrawInWorld()
        {

            //foreach (Room room in rooms)
            //{
            //    room.Draw();
            //}

            base.DrawInWorld();
            
        }

        public override void Update()
        {
            KeyboardState state = Keyboard.GetState();

            //check if key is pressed
            if (state.IsKeyDown(Keys.Space))
            {
                lvlGen.RemoveRooms();
                lvlGen.GenerateWorld();
            }
           

            base.Update();
        }
    }
}
