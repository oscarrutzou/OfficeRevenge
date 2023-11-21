using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

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

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
