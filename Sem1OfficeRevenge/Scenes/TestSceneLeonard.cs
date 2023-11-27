using Microsoft.Xna.Framework.Input;
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
        bool pressed;
        public TestSceneLeonard()
        {
            
        }

        public override void Initialize()
        {
            comEnm = new CombatEnemy();
            Global.currentScene.Instantiate(comEnm);
        }

        public override void DrawInWorld()
        {
            ScoreManager.Draw(new Vector2(10, 10));
            base.DrawInWorld();
        }

        public override void Update()
        {
            ScoreManager.UpdateScore();
            comEnm.Update();
            base.Update();
        }
    }
}
