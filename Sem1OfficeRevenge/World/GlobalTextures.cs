using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sem1OfficeRevenge
{
    public enum TextureNames
    {
        //Gui
        GuiButtonBasicBlue,
        TileMap1,
    }

    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> textures;

        public static SpriteFont defaultFont;
        
        public static void LoadContent()
        {
            textures = new Dictionary<TextureNames, Texture2D>
            {
                {TextureNames.GuiButtonBasicBlue, Global.world.Content.Load<Texture2D>("PlaceHolder\\buttonBlue") },
                {TextureNames.TileMap1, Global.world.Content.Load<Texture2D>("Rooms\\TempLobby1") },
            };

            defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\Arial");
        }
    }
}
