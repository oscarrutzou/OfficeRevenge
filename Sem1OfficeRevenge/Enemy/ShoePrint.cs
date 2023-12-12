using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Sem1OfficeRevenge
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
