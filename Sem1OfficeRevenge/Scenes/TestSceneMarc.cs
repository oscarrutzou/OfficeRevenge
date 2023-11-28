using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sem1OfficeRevenge
{
    public class TestSceneMarc : Scene
    {

        public TestSceneMarc()
        {
            
        }

        public override void Initialize()
        {

            Global.player = new Player(Vector2.Zero);
            Global.player.centerOrigin = true;
            Global.currentScene.Instantiate(Global.player);

            TestObjectCollide testObj = new TestObjectCollide(new Vector2(400,200));
            Global.currentScene.Instantiate(testObj);

        }

        public override void Update()
        {
            
            base.Update();
            //if (InputManager.keyboardState.IsKeyDown(Keys.A))
            //{
            //    testObj.SetPlayerAnimation(AnimNames.PlayerRifleIdle);
            //}
            //if (!init)
            //{
            //    init = true;
            //Global.world.blackScreenFadeInOut?.StartFadeOut();
            //}

            //if (InputManager.keyboardState.IsKeyDown(Keys.W))
            //{
            //    player.SetObjectAnimation(AnimNames.PlayerRifleMove);
                

            //}

            //if (InputManager.keyboardState.IsKeyDown(Keys.D))
            //{
            //    testObj.SetPlayerAnimation(AnimNames.PlayerRifleShoot);
            //}

        }
    }
}
