using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    internal class Shell : GameObject
    {
        public Shell(int type, Vector2 position) 
        {
            this.position = position;
            switch (type)
            {
                    case 1: //shotgun
                    texture = GlobalTextures.textures[TextureNames.shotgunBullet];
                    break;
                    case 2: //pistol
                    texture = GlobalTextures.textures[TextureNames.pistolBullet];
                    break;
                    case 3: //rifle
                    texture = GlobalTextures.textures[TextureNames.rifleBullet];
                    break;
                default:
                    break;
            }



        }  

    }
}
