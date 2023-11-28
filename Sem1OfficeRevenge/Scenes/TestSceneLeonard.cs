﻿using Microsoft.Xna.Framework.Input;
using Sem1OfficeRevenge.Content.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class TestSceneLeonard : Scene
    {
        CombatEnemy comEnm;
        Player player;
        CivillianEnemy civEnm;
        bool pressed;
        public TestSceneLeonard()
        {
            
        }

        public override void Initialize()
        {
            //comEnm = new CombatEnemy();
            player = new Player();
            Global.currentScene.Instantiate(player);

            civEnm = new CivillianEnemy();
            Global.currentScene.Instantiate(civEnm);
            Global.player = player;
            
        }

        public override void DrawInWorld()
        {
            ScoreManager.Draw(new Vector2(10, 10));
            player.Draw();
            civEnm.Draw();
            base.DrawInWorld();
        }

        public override void Update()
        {
            ScoreManager.UpdateScore();
            civEnm.Update();
            base.Update();
        }
    }
}
