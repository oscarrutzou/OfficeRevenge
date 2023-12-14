using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Sem1OfficeRevenge 
{
    internal class Magazin : GameObject
    {

        public Magazin(Vector2 position, float rotation) 
        {
            Random rnd = new Random();
            this.position = position;
            this.rotation = rotation + rnd.Next(-360, 361);
            scale = new Vector2(0.07f, 0.07f);
            texture = GlobalTextures.textures[TextureNames.magazin];
            centerOrigin = true;
            layerDepth = 0.2f;
        }





    }
}
