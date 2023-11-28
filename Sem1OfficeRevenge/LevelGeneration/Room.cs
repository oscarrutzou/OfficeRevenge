using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Sem1OfficeRevenge
{
    internal class Room : GameObject
    {
        //public Texture2D map;
        int width;
        int height;
        private Vector2 origin;
        private Vector2 center;

        public Room(Texture2D Map, float rotation)
        {
            this.texture = Map;
            CenterOrigin = true;
            this.rotation = rotation;
            this.scale = new Vector2(1f, 1f);

        }
        
        public override void Update()
        {
            
        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
