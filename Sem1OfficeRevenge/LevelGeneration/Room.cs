using System;
using System.Collections.Generic;
using System.Drawing.Design;
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
        private Vector2 origin;
        private Vector2 center;

        public Room(Texture2D Map)
        {
            this.map = Map;
            //center = new Vector2(Global.graphics.GraphicsDevice.Viewport.Width / 2, Global.graphics.GraphicsDevice.Viewport.Height / 2);

        }
        
        public override void Update()
        {

        }

        public override void Draw()
        {
            Vector2 origin = new Vector2(GlobalTextures.textures[TextureNames.TileMap1].Width / 2, GlobalTextures.textures[TextureNames.TileMap1].Height / 2);
            center = new Vector2(Global.graphics.GraphicsDevice.Viewport.Width / 2, Global.graphics.GraphicsDevice.Viewport.Height / 2);
            //UseCenterOrigin = true;

            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.TileMap1], center, null, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }

    }
}
