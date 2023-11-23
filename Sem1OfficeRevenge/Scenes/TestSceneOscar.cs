using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public class TestSceneOscar : Scene
    {
        Button newBtn;
        public TestSceneOscar()
        {
            
        }

        public override void Initialize()
        {
            newBtn = new Button(new Vector2(100, 100), "", GlobalTextures.textures[TextureNames.GuiButtonBasicBlue], () => { });
            Global.currentScene.Instantiate(newBtn);
        }

        public override void DrawInWorld()
        {
            base.DrawInWorld();
            
        }



        public override void Update()
        {
            base.Update();
            
        }
    }
}
