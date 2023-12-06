using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sem1OfficeRevenge
{
    /// <summary>
    /// We could use a static instance on stuff like the world and scenedata, but it gives the same result, so on a smaller project like this one, we can just use this global class.
    /// </summary>
    public static class Global
    {
        public static Scene currentScene;
        public static SceneData currentSceneData;
        public static GameWorld world;
        public static Player player;

        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public static GameTime gameTime;
        public static Random rnd = new Random();
    }
}
