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
        TileMap2,
        TileMap3,
        TileMap4,
        TileMap5,
        GuiButtonTest,
        
    }

    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> textures;

        public static SpriteFont defaultFont;
        //public static SpriteFont crunchyFont;
        
        public static void LoadContent()
        {
            textures = new Dictionary<TextureNames, Texture2D>
            {
                {TextureNames.TileMap1, Global.world.Content.Load<Texture2D>("Rooms\\TempLobby1") },
                {TextureNames.TileMap2, Global.world.Content.Load<Texture2D>("Rooms\\room2") },
                {TextureNames.TileMap3, Global.world.Content.Load<Texture2D>("Rooms\\room3") },
                {TextureNames.TileMap4, Global.world.Content.Load<Texture2D>("Rooms\\room4p") },
                {TextureNames.TileMap5, Global.world.Content.Load<Texture2D>("Rooms\\room5") },

                {TextureNames.GuiButtonTest, Global.world.Content.Load<Texture2D>("GUI\\TestBtn") },

            };

            //defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\Friday13SH");
            //defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\Friday13v12");
            defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\SlencoBlack");
            //defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\CrunchyFont");
            //crunchyFont = Global.world.Content.Load<SpriteFont>("Fonts\\CrunchyFont");
        }
    }
}
