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
            testObj = new TestObj(GlobalAnimations.SetObjAnimation(AnimNames.PlayerRifleIdle));
            Global.currentScene.Instantiate(testObj);
        }

        public override void DrawInWorld()
        {
            base.DrawInWorld();
            
        }



        public override void Update()
        {
            base.Update();
            
            if (InputManager.keyboardState.IsKeyDown(Keys.A))
            {
                testObj.SetPlayerAnimation(AnimNames.PlayerRifleIdle);
            }

            if (InputManager.keyboardState.IsKeyDown(Keys.S))
            {
                testObj.SetPlayerAnimation(AnimNames.PlayerRifleMove);
            }

            if (InputManager.keyboardState.IsKeyDown(Keys.D))
            {
                testObj.SetPlayerAnimation(AnimNames.PlayerRifleShoot);
            }

        }
    }
}
