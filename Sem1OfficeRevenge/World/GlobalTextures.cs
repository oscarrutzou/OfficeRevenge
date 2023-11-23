﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public enum TextureNames
    {
        //Player
        PlayerIdleRifle_Static,

        //Gui
        GuiButtonBasicBlue,
    }

    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> textures;

        public static SpriteFont defaultFont;
        
        public static void LoadContent()
        {
            textures = new Dictionary<TextureNames, Texture2D>
            {
                {TextureNames.PlayerIdleRifle_Static, Global.world.Content.Load<Texture2D>("Player\\Top_Down_Survivor\\rifle\\idle\\survivor-idle_rifle_0") },
                {TextureNames.GuiButtonBasicBlue, Global.world.Content.Load<Texture2D>("PlaceHolder\\buttonBlue") },

            };

            defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\Arial");
        }
    }
}
