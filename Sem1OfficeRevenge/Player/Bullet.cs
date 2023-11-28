using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class Bullet : GameObject
    {
        public float lifespan { get; private set; }
        public float TotalSeconds;
        public Bullet(BulletData data)
        {
            texture = GlobalTextures.textures[TextureNames.Bullet];
            speed = 100;
            rotation = data.rotation;
            direction = new((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            lifespan = 2;
            position = data.position;
        }
        
            
        public override void Update()
        {
            TotalSeconds = (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
            position += direction * speed * TotalSeconds;
        }

        public void SetCorrectBulletPosition()
        {
            
        }

    }
}
