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

            Global.spriteBatch.DrawString(GlobalTextures.defaultFont, "Hej", Global.world.camera.Center + new Vector2(0, -300), Color.Black);
        }
    }
}
