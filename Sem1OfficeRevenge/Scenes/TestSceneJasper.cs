using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        Room startRoom;
        private List<Room> rooms = new List<Room>();

        public TestSceneJasper()
        {
            
        }

        public override void Initialize()
        {
            // lvlGen.GenerateLevel();
            
            Global.graphics.IsFullScreen = false;
            Global.graphics.PreferredBackBufferWidth = 1920;
            Global.graphics.PreferredBackBufferHeight = 1080;
            Global.graphics.ApplyChanges();

            startRoom = new Room(GlobalTextures.textures[TextureNames.TileMap1]);
            rooms.Add(startRoom);
            Global.currentScene.Instantiate(startRoom);
        }

        public override void DrawInWorld()
        {

            foreach (Room room in rooms)
            {
                room.Draw();
            }

            base.DrawInWorld();
            
        }

        public override void Update()
        {

            base.Update();
        }
    }
}
