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

        //Hvis der er problemer med dette variabel, så bare lige sig det til mig Oscar, så det kan fikses.
        public static GameTime gameTime;

        public static Player player;
    }
}
