﻿namespace Sem1OfficeRevenge
{
    public class TestSceneLeonard : Scene
    {
        Player player;
        CivillianEnemy civEnm;
        CombatEnemy comEnm;
        bool pressed;
        public TestSceneLeonard()
        {
            
        }

        public override void Initialize()
        {
            //comEnm = new CombatEnemy();
            player = new Player();
            Global.currentScene.Instantiate(player);
            Global.player = player;

            civEnm = new CivillianEnemy();
            Global.currentScene.Instantiate(civEnm);

            comEnm = new CombatEnemy();
            Global.currentScene.Instantiate(comEnm);

        }


        public override void DrawInWorld()
        {
            ScoreManager.DrawScore();
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
