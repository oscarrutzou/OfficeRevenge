using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class TestObjectCollide : GameObject
    {

        public TestObjectCollide(Vector2 pos) {
            position = pos;
            SetObjectAnimation(AnimNames.PlayerRifleShoot);
        }


        //public override void Update()
        //{
        //    CheckCollisionBox();
        //}

        //public override void CheckCollisionBox()
        //{
            
        //}

    }
}
