using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public static class Global
    {
        public static Scene currentScene;
        public static SceneData currentSceneData;
        
        public static GameWorld world;
        
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        //Hvis der er problermer med dette variabel, så bare lige sig det til mig Oscar, så det kan fikses.
        public static GameTime gameTime;

        public static Player player;
    }
}
