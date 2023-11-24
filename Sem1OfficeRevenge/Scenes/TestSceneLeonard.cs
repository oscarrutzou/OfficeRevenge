using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class TestSceneLeonard : Scene
    {
        CombatEnemy comEnm;
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
            base.DrawInWorld();
        }

        public override void Update()
        {
            comEnm.Update();
            base.Update();
        }
    }
}
