using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sem1OfficeRevenge
{
    public static class Global
    {
        public static Scene currentScene;
        public static SceneData currentSceneData;
        public static Random rnd = new Random();
        public static GameWorld world;
        
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public static GameTime gameTime;

        public static Player player;
    }
}
