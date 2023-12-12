namespace Sem1OfficeRevenge
{
    public class TestSceneMarc : Scene
    {

        public TestSceneMarc()
        {
            
        }

        public override void Initialize()
        {

            Global.player = new Player();
            Global.player.centerOrigin = true;
            Global.currentScene.Instantiate(Global.player);


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
