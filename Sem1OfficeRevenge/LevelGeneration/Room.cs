using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Sem1OfficeRevenge
{
    internal class Room : GameObject
    {
        public Texture2D map;
        int width;
        int height;

        public Room(Texture2D Map)
        {
            this.map = Map;

            width = Global.graphics.PreferredBackBufferWidth;
            height = Global.graphics.PreferredBackBufferHeight;
        }
        
        public override void Update()
        {

        }

        public override void Draw()
        {
            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.TileMap1], new Vector2(width / 2, height / 2), Color.White);
        }

    }
}
