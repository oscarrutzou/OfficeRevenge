using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class MiniMapCam : Camera
    {
        private Viewport defaultViewport;
        private Viewport viewport;
        private int dimension = 300;
        private Vector2 center;
        private float magicNmbScale = 0.8f; //How much the minimap can see.
        private float scale;

        public MiniMapCam(Vector2 origin) : base(origin)
        {
            base.origin = origin;
        }

        public void DrawMiniMap()
        {
            int x = Global.graphics.PreferredBackBufferWidth - dimension;
            viewport = new Viewport(x, 0, dimension, dimension);

            defaultViewport = Global.graphics.GraphicsDevice.Viewport;
            Global.graphics.GraphicsDevice.Viewport = viewport;

            center = new Vector2(viewport.X + viewport.Width / 2, viewport.Y + viewport.Height / 2);
            scale = dimension / (float)defaultViewport.Width;

            // Draw the minimap.
            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pixel],
                    new Vector2(x,0),
                    viewport.Bounds,
                    Color.Gray,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f);


            //DrawRooms();
            DrawPlayer();
            DrawBullets();
            DrawEnemies();
            
            // Reset the GraphicsDevice's viewport to the default.
            Global.graphics.GraphicsDevice.Viewport = defaultViewport;
        }


        private void DrawPlayer()
        {
            int playerDem = 15;
            Vector2 playerPos = ObjectPos(Global.player) - new Vector2(playerDem / 2, playerDem / 2);

            Rectangle playerRec = new Rectangle((int)playerPos.X, (int)playerPos.Y, playerDem, playerDem);
 
            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pixel],
                                    playerPos,
                                    playerRec,
                                    Color.Blue,
                                    0f,
                                    Vector2.Zero,
                                    1f,
                                    SpriteEffects.None,
                                    Global.currentScene.GetObjectLayerDepth(LayerDepth.Player));
        }


        private Vector2 ObjectPos(GameObject gameObject)
        {
            Vector2 relativePos;
            if (gameObject == Global.player)
            {
                relativePos = Vector2.Zero;
            }
            else
            {
                relativePos = gameObject.position - Global.player.position;
            }
            return center + new Vector2((int)(relativePos.X * scale * magicNmbScale), (int)(relativePos.Y * scale * magicNmbScale));
        }


        private void DrawRooms()
        {
            foreach (Room roomObj in Global.currentSceneData.rooms)
            {
                Vector2 pos = ObjectPos(roomObj);

                // Draw the room on the minimap.
                Global.spriteBatch.Draw(roomObj.texture, pos, null, Color.White, roomObj.rotation, roomObj.origin, 1f, SpriteEffects.None, Global.currentScene.GetObjectLayerDepth(LayerDepth.Background) + 0.01f);
            }
        }


        private void DrawEnemies()
        {
            foreach (GenericEnemy enemObj in Global.currentSceneData.enemies)
            {
                Vector2 pos = ObjectPos(enemObj);
                int dem = 10;

                // Create a rectangle for the enemy on the minimap.
                Rectangle enemyRect = new Rectangle((int)pos.X, (int)pos.Y, dem, dem);

                // Check if the enemy's rectangle is within the bounds of the viewport.
                if (viewport.Bounds.Contains(enemyRect))
                {
                    // Draw the enemy on the minimap.
                    Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pixel],
                                            pos,
                                            enemyRect,
                                            enemObj.dead ? Color.Red : Color.Green,
                                            0f,
                                            Vector2.Zero,
                                            1f,
                                            SpriteEffects.None,
                                            Global.currentScene.GetObjectLayerDepth(LayerDepth.Enemies));
                }
            }
        }

        private void DrawBullets()
        {
            foreach (Bullet bulletObj in Global.currentSceneData.bullets)
            {
                Vector2 pos = ObjectPos(bulletObj);
                int dem = 2;

                // Create a rectangle for the bullet on the minimap.
                Rectangle bulletRect = new Rectangle((int)pos.X, (int)pos.Y, dem, dem);

                // Check if the bullet's rectangle is within the bounds of the viewport.
                if (viewport.Bounds.Contains(bulletRect))
                {
                    // Draw the bullet on the minimap.
                    Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pixel],
                                            pos,
                                            bulletRect,
                                            Color.Turquoise,
                                            0f,
                                            Vector2.Zero,
                                            1f,
                                            SpriteEffects.None,
                                            Global.currentScene.GetObjectLayerDepth(LayerDepth.Bullets));
                }
            }
        }

    }
}
