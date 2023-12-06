using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge.Enemy
{
    public class ShoePrint : GameObject
    {
        

        public ShoePrint(bool direction, Vector2 position, float rotation) 
        {
            centerOrigin = true;
            scale = new Vector2 (0.15f, 0.15f);
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.InteractableObjects);
            this.position = position;
            texture = GlobalTextures.textures[TextureNames.Shoe];
            this.rotation = rotation-((float) Math.PI/2);
            Global.currentScene.Instantiate(this);
            switch (direction) 
            {
                case true:

                    break;

                case false:
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    break;

            }
        
        }


    }
}
