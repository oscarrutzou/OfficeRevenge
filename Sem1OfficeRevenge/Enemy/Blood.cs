using System;
using Microsoft.Xna.Framework;

namespace Sem1OfficeRevenge
{
     public class Blood : GameObject
     {
        private Random rnd = new Random();
        private Vector2 targetScale;
        

        public Blood(Vector2 position) 
        {
            centerOrigin = true;
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.InteractableObjects);
            this.position = position + new Vector2(rnd.Next(-5, 5), rnd.Next(-5, 5)); 
            texture = GlobalTextures.textures[TextureNames.Blood];
            rotation = rnd.Next((int)Math.PI*2*10)*0.1f;
            targetScale = scale / 2 - new Vector2(0.1f, 0.1f);

            //Randomize scale
            targetScale.X += (float)rnd.Next(0, 4) / 10;
            targetScale.Y += (float)rnd.Next(0, 4) / 10;
            scale = scale / 10;
        }

        public override void Update()
        {
            //Scale up
            if (scale.X < targetScale.X)
            {
                scale.X += (float)rnd.Next(3)/100;
                scale.Y += (float)rnd.Next(3) / 100;
            }

            //Scale down
            foreach (Blood blood in Global.currentSceneData.bloods)
            {
                if (blood != this && Collision.IntersectBox(this, blood))
                {
                    targetScale = scale; break;
                }
            }
        }


    }
}
