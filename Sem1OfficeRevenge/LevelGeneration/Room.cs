using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public class Room : GameObject
    {
        //public Texture2D map;
        public int width;
        public int height;
        public float alpha;
        public float alphaFadeInTimer;
        public bool isFadingIn = true;
        private Vector2 origin;
        private Vector2 center;
        public Rectangle hallwayCol;

        public Room(Texture2D Map, float rotation)
        {
            this.texture = Map;
            centerOrigin = true;
            this.rotation = rotation;
            this.scale = new Vector2(5f,5f);
            width = this.texture.Width * (int)scale.X;
            height = this.texture.Height * (int)scale.Y;
            layerDepth = Global.currentScene.GetObjectLayerDepth(LayerDepth.Background);
        }
    }
}
