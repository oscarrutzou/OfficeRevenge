using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class TestObj: GameObject
    {
        public TestObj(Texture2D textureStatic)
        {
            texture = textureStatic;
            CenterOrigin = true;
        }

        public TestObj(Animation anim)
        {
            animation = anim;
            CenterOrigin = true;
        }

    }
}
