using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sem1OfficeRevenge
{
    public class TestSceneJasper : Scene
    {
        //public List<Texture2D> maps = new List<Texture2D>();
        //public List<Room> rooms = new List<Room>();

        private Player player;
        private Texture2D texture;
        private Vector2 position;

        LevelGeneration lvlGen;
        public bool isDown = false;

        public TestSceneJasper()
        {
            
        }

        public override void Initialize()
        {

            lvlGen = new LevelGeneration();
            lvlGen.GenerateSecondWorld();
            
            Global.graphics.IsFullScreen = false;
            Global.graphics.PreferredBackBufferWidth = 1920;
            Global.graphics.PreferredBackBufferHeight = 1080;
            Global.graphics.ApplyChanges();

            player = new Player();
            Global.currentScene.Instantiate(player);
            Global.player = player;
            player.centerOrigin = true;

            //startRoom = new Room(GlobalTextures.textures[TextureNames.TileMap1], MathHelper.PiOver2);
            //rooms.Add(startRoom);
            //Global.currentScene.Instantiate(startRoom);
        }


        public override void DrawInWorld()
        {
            Global.graphics.GraphicsDevice.Clear(Color.Black);

            base.DrawInWorld();
            
        }

        public override void Update()
        {
            KeyboardState state = Keyboard.GetState();

            ////check if key is pressed
            //if (state.IsKeyDown(Keys.Space))
            //{
            //    lvlGen.RemoveRooms();
            //    lvlGen.GenerateWorld();
            //}
           

            base.Update();
        }
    }
}
