using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sem1OfficeRevenge
{
    public class TestSceneOscar : Scene
    {
        //Button newBtn;
        TestObj testObj;
        public TestSceneOscar()
        {
            
        }

        public override void Initialize()
        {
            //newBtn = new Button(new Vector2(100, 100), "", GlobalTextures.textures[TextureNames.PlayerIdleRifle_Static], () => { });
            //testObj = new TestObj(GlobalTextures.textures[TextureNames.GuiButtonBasicBlue]);
            //testObj = new TestObj(GlobalAnimations.SetObjAnimation(AnimNames.PlayerRifleIdle));
            //testObj.position = Global.world.camera.Center;
            //Global.currentScene.Instantiate(testObj);
            //testObj.SetCollisionBox(300, 150, new Vector2(-10, 10));
            
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
