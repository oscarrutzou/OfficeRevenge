using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.Collections.Generic;


namespace Sem1OfficeRevenge
{
    public enum TextureNames
    {
        //Gui
        GuiButtonTest,
        GuiSliderBase,
        GuiSliderOver,
        GuiSliderHandle,
        Pixel,


        //test
        Bullet,
        
    }

    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> textures;

        public static SpriteFont defaultFont;
        
        public static void LoadContent()
        {
            textures = new Dictionary<TextureNames, Texture2D>
            {
                {TextureNames.GuiButtonTest, Global.world.Content.Load<Texture2D>("GUI\\TestBtn") },
                {TextureNames.GuiSliderBase, Global.world.Content.Load<Texture2D>("GUI\\Slider_frame") },
                {TextureNames.GuiSliderOver, Global.world.Content.Load<Texture2D>("GUI\\Slider_frame_over") },
                {TextureNames.GuiSliderHandle, Global.world.Content.Load<Texture2D>("GUI\\Slider_Handle") },
                {TextureNames.Pixel, Global.world.Content.Load<Texture2D>("GUI\\Pixel") },
                {TextureNames.Bullet, Global.world.Content.Load<Texture2D>("Bullet\\Bullet") }
            };

            //defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\Arial");
            
            
            //defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\Friday13SH");
            //defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\Friday13v12");
            defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\SlencoBlack");

        }
    }
}
