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
        private int dimension;
        private Vector2 center;
        private float magicNmbScale; //How much the minimap can see.
        private float scale;
        Vector2 texScale;

        public MiniMapCam(Vector2 origin) : base(origin)
        {
            base.origin = origin;
        }

        public void DrawMiniMap()
        {
            int x = Global.graphics.PreferredBackBufferWidth - dimension;
            dimension = Global.graphics.PreferredBackBufferWidth / 5;
            if (dimension > 300)
            {
                dimension = 300;
                magicNmbScale = 0.35f;
            }
            else
            {
                magicNmbScale = 0.25f;
            }

            viewport = new Viewport(x, 0, dimension, dimension);

            defaultViewport = Global.graphics.GraphicsDevice.Viewport;
            Global.graphics.GraphicsDevice.Viewport = viewport;

            center = new Vector2(viewport.X + viewport.Width / 2, viewport.Y + viewport.Height / 2);
            scale = (dimension / (float)defaultViewport.Width) * magicNmbScale;
            
            if (Global.currentSceneData.rooms != null)
            {
                texScale = Global.currentSceneData.rooms[0].scale * scale;
            }

            // Draw the minimap.
            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pixel], new Vector2(x,0), viewport.Bounds, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            DrawRooms();
            DrawPlayer();
            DrawBullets();
            DrawEnemies();
            
            // Reset the GraphicsDevice's viewport to the default.
            Global.graphics.GraphicsDevice.Viewport = defaultViewport;
        }


        private void DrawPlayer()
        {
            int playerDem = (int)(30 * texScale.X);
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
            return center + (gameObject.position - Global.player.position) * scale;
        }

        private void DrawRooms()
        {
            foreach (Room roomObj in Global.currentSceneData.rooms)
            {
                Vector2 pos = ObjectPos(roomObj);
                Vector2 origin = new Vector2(roomObj.texture.Width / 2, roomObj.texture.Height / 2);

                // Calculate the room's bounds in the screen's coordinates.
                Rectangle roomBounds = new Rectangle((int)(pos.X - origin.X * texScale.X), (int)(pos.Y - origin.Y * texScale.Y), (int)(roomObj.texture.Width * texScale.X), (int)(roomObj.texture.Height * texScale.Y));

                if (viewport.Bounds.Contains(roomBounds))
                {
                    Global.spriteBatch.Draw(roomObj.texture, pos, null, Color.White, roomObj.rotation, origin, texScale, SpriteEffects.None, Global.currentScene.GetObjectLayerDepth(LayerDepth.Background) + 0.01f);
                }
            }
        }




        private void DrawEnemies()
        {
            foreach (GenericEnemy enemObj in Global.currentSceneData.enemies)
            {
                Vector2 pos = ObjectPos(enemObj);
                int dem = (int)(20 * texScale.X);

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
                int dem = (int)(10 * texScale.X);

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
