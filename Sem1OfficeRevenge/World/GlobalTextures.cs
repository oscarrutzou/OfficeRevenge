using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.Collections.Generic;


namespace Sem1OfficeRevenge
{
    public enum TextureNames
    {
        TileMap1,
        TileMap2,
        TileMap3,
        TileMap4,
        TileMap5,
        TileMap6,
        TileMap7,
        GuiButtonTest,
        GuiSliderBase,
        GuiSliderOver,
        GuiSliderHandle,
        GuiEleBtnNormal,
        GuiEleBtnUp,
        GuiEleText,
        ElevatorBgSmall,

        MiniMapOverLaySmall,
        MiniMapOverLayBig,

        ElevatorMenu,
        Pixel,

        Blood,
        Shoe,
        Sight,

        Pistol,
        ShotGun,
        Rifle,

        Bullet,
        Pellet,
    }

    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> textures { get; private set; }
        public static SpriteFont defaultFont { get; private set; }
        public static SpriteFont defaultFontMid { get; private set; }
        public static SpriteFont defaultFontBig { get; private set; }

        public static void LoadContent()
        {

            textures = new Dictionary<TextureNames, Texture2D>
            {
                {TextureNames.TileMap1, Global.world.Content.Load<Texture2D>("Rooms\\TempLobby1") },
                {TextureNames.TileMap2, Global.world.Content.Load<Texture2D>("Rooms\\room2") },
                {TextureNames.TileMap3, Global.world.Content.Load<Texture2D>("Rooms\\room3") },
                {TextureNames.TileMap4, Global.world.Content.Load<Texture2D>("Rooms\\room4p") },
                {TextureNames.TileMap5, Global.world.Content.Load<Texture2D>("Rooms\\room5") },
                {TextureNames.TileMap6, Global.world.Content.Load<Texture2D>("Rooms\\Elevator") },
                {TextureNames.TileMap7, Global.world.Content.Load<Texture2D>("Rooms\\ElevatorReverse") },

                {TextureNames.GuiButtonTest, Global.world.Content.Load<Texture2D>("GUI\\TestBtn") },
                {TextureNames.GuiSliderBase, Global.world.Content.Load<Texture2D>("GUI\\Slider_frame") },
                {TextureNames.GuiSliderOver, Global.world.Content.Load<Texture2D>("GUI\\Slider_frame_over") },
                {TextureNames.GuiSliderHandle, Global.world.Content.Load<Texture2D>("GUI\\Slider_Handle") },
                {TextureNames.MiniMapOverLaySmall, Global.world.Content.Load<Texture2D>("GUI\\MiniMapOverLay") },
                {TextureNames.MiniMapOverLayBig, Global.world.Content.Load<Texture2D>("GUI\\MiniMapOverLay330") },
                {TextureNames.Pixel, Global.world.Content.Load<Texture2D>("GUI\\Pixel") },
                {TextureNames.Pistol, Global.world.Content.Load<Texture2D>("GUI\\pistolPic") },
                {TextureNames.Rifle, Global.world.Content.Load<Texture2D>("GUI\\ak47Pic") },
                {TextureNames.ShotGun, Global.world.Content.Load<Texture2D>("GUI\\shotgunPic") },

                {TextureNames.ElevatorMenu, Global.world.Content.Load<Texture2D>("GUI\\Elevatormenu") },
                {TextureNames.GuiEleBtnNormal, Global.world.Content.Load<Texture2D>("GUI\\SimpleBtn") },
                {TextureNames.GuiEleBtnUp, Global.world.Content.Load<Texture2D>("GUI\\SimpleBtnUp") },
                {TextureNames.GuiEleText, Global.world.Content.Load<Texture2D>("GUI\\ElevatorText") },
                {TextureNames.ElevatorBgSmall, Global.world.Content.Load<Texture2D>("GUI\\ElevatorBgSmall") },


                {TextureNames.Bullet, Global.world.Content.Load<Texture2D>("Bullet\\Bullet") },
                {TextureNames.Pellet, Global.world.Content.Load<Texture2D>("Bullet\\pellet") },


                {TextureNames.Blood, Global.world.Content.Load<Texture2D>("Fonts\\BloodPool") },
                {TextureNames.Shoe, Global.world.Content.Load<Texture2D>("Fonts\\shoeprint") },
                {TextureNames.Sight, Global.world.Content.Load<Texture2D>("Fonts\\sight") }


            };

            defaultFont = Global.world.Content.Load<SpriteFont>("Fonts\\SlencoBlack");
            defaultFontMid = Global.world.Content.Load<SpriteFont>("Fonts\\SlencoBlackMid");
            defaultFontBig = Global.world.Content.Load<SpriteFont>("Fonts\\SlencoBlackBig");

        }
    }
}
