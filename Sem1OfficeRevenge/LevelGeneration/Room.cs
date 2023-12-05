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
    public class Room : GameObject
    {
        //public Texture2D map;
        public int width;
        public int height;
        private Vector2 origin;
        private Vector2 center;
        public Rectangle hallwayCol;


        public Room(Texture2D Map, float rotation)
        {
            this.texture = Map;
            centerOrigin = true;
            this.rotation = rotation;
            this.scale = new Vector2(1f, 1f);
            width = this.texture.Width * (int)scale.X;
            height = this.texture.Height * (int)scale.Y;
            layerDepth = Global.currentScene.GetObjectLayerDepth(LayerDepth.Background);

        }
        
        public override void Update()
        {
            
        }

        public override void Draw()
        {
            base.Draw();
            DrawDebugCollisionBox(hallwayCol);
            DrawDebugCollisionBox();
        }

    }
}
