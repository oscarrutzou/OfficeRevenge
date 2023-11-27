using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class GenericEnemy: GameObject
    {
        private int health;
        //private float speed; Dette variable er også i gameobjects
        public bool dead;

        public GenericEnemy()
        {
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.Enemies);
        }

        //Skal vi fjernne denne så det bare er update?
        public void EnemyUpdate() { }

        public override void Update()
        {
            
        }
    }
}
