﻿using System;
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
        private Player player;
        private Texture2D texture;
        private Vector2 position;
        
        public TestSceneMarc()
        {
            
        }

        public override void Initialize()
        {

            position = new Vector2(100, 100);
            player = new Player(position);
            Global.currentScene.Instantiate(player);
            Global.player = player;
            player.centerOrigin = true;

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
