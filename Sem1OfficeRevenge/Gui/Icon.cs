﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public class Icon : Gui
    {
        public Icon(Vector2 scale, Vector2 position, Texture2D texture)
        {
            this.scale = scale;
            this.position = position;
            this.texture = texture;
            centerOrigin = true;
        }

        public Icon(Vector2 scale, Vector2 position, Animation animation)
        {
            this.scale = scale;
            this.position = position;
            this.animation = animation;
            centerOrigin = true;
        }
    }
}
