using System;
using Microsoft.Xna.Framework;

namespace Sem1OfficeRevenge
{
    public class MainMenu : Scene
    {



        public override void Initialize()
        {
            
        }

        public override void Update()
        {
            base.Update();
        }
        public override void DrawOnScreen()
        {
            base.DrawOnScreen();

            Vector2 pos = Global.world.camera.Center + new Vector2(0, -300);
            Global.spriteBatch.DrawString(GlobalTextures.defaultFont, "Hej", pos, Color.Black);
        }
    }
}
