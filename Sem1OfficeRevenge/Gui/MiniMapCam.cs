using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public class MiniMapCam : Camera
    {
        private Viewport defaultViewport;
        private Viewport viewport;
        private int posbuffer = 10;
        private Rectangle recViewPortWithBuffer;
        private int dimBuffer = 25;
        private int dimension;
        private Vector2 center;
        private float magicNmbScale; //How much the minimap can see.
        private float scale;

        Vector2 texScale;
        private bool smallMap;
        public MiniMapCam(Vector2 origin) : base(origin)
        {
            base.origin = origin;
        }

        public void DrawMiniMap()
        {
            if (Global.world.IsCurrentSceneMenu()) return;

            int x = Global.graphics.PreferredBackBufferWidth - dimension - posbuffer;
   
            // Set the dimension of the minimap depending on the window size.
            if (Global.graphics.PreferredBackBufferWidth > 1300)
            {
                dimension = 330;
                magicNmbScale = 0.37f;
                smallMap = false;
            }
            else
            {
                dimension = 256;
                magicNmbScale = 0.25f;
                smallMap = true;
            }

            // Makes the new viewport and the rec that determines when the minimap room textures stops drawing
            viewport = new Viewport(x, posbuffer, dimension - 2 * posbuffer, dimension - 2 * posbuffer);
            recViewPortWithBuffer = new Rectangle(x + dimBuffer, posbuffer + dimBuffer, dimension - 2 *  dimBuffer, dimension - 2 * dimBuffer);


            defaultViewport = Global.graphics.GraphicsDevice.Viewport;
            Global.graphics.GraphicsDevice.Viewport = viewport;

            // Used later for setting 
            center = new Vector2(viewport.X + viewport.Width / 2, viewport.Y + viewport.Height / 2);
            scale = (dimension / (float)defaultViewport.Width) * magicNmbScale;
            
            //Take a rooms scale
            if (Global.currentSceneData.rooms.Count != 0)
            {
                texScale = Global.currentSceneData.rooms[0].scale * scale;
            }

            // Draw the minimap.
            Texture2D mapTex = smallMap ? GlobalTextures.textures[TextureNames.MiniMapOverLaySmall] : GlobalTextures.textures[TextureNames.MiniMapOverLayBig];

            Global.spriteBatch.Draw(mapTex, new Vector2(x, posbuffer), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pixel], new Vector2(x + dimBuffer, dimBuffer + posbuffer), recViewPortWithBuffer, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            DrawRooms();
            DrawPlayer();
            DrawBullets();
            DrawEnemies();
            
            //Draw minimap overlay

            // Reset the GraphicsDevice's viewport to the default.
            Global.graphics.GraphicsDevice.Viewport = defaultViewport;
        }


        private void DrawPlayer()
        {
            // Draw the player on the minimap.
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
            // Calculate the position of the object on the minimap.
            return center + (gameObject.position - Global.player.position) * scale;
        }

        private void DrawRooms()
        {
            // Draw the rooms on the minimap.
            foreach (Room roomObj in Global.currentSceneData.rooms)
            {
                Vector2 pos = ObjectPos(roomObj);
                Vector2 origin = new Vector2(roomObj.texture.Width / 2, roomObj.texture.Height / 2);
                Rectangle roomBounds = new Rectangle((int)(pos.X - origin.X * texScale.X), (int)(pos.Y - origin.Y * texScale.Y), (int)(roomObj.texture.Width * texScale.X), (int)(roomObj.texture.Height * texScale.Y));

                if (recViewPortWithBuffer.Contains(roomBounds))
                {
                    // If the room is in the viewport, start or continue the fade-in effect
                    roomObj.isFadingIn = true;
                    roomObj.alphaFadeInTimer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    // If the room is not in the viewport, start or continue the fade-out effect
                    roomObj.isFadingIn = false;
                    roomObj.alphaFadeInTimer -= (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
                }

                // Clamp the fade timer between 0 and 1, and calculate the alpha value
                roomObj.alphaFadeInTimer = MathHelper.Clamp(roomObj.alphaFadeInTimer, 0f, 1f);
                roomObj.alpha = roomObj.alphaFadeInTimer;

                // Draw the room with the updated alpha value
                Color color = Color.White * roomObj.alpha; // Apply the alpha value to the color
                Global.spriteBatch.Draw(roomObj.texture, pos, null, color, roomObj.rotation, origin, texScale, SpriteEffects.None, Global.currentScene.GetObjectLayerDepth(LayerDepth.Background) + 0.01f);
            }
        }

        private void DrawEnemies()
        {
            // Draw the enemies on the minimap.
            foreach (GenericEnemy enemObj in Global.currentSceneData.enemies)
            {
                // Create a rectangle for the enemy on the minimap.
                Vector2 pos = ObjectPos(enemObj);
                int dem = 20;
                Rectangle enemRec = new Rectangle((int)pos.X, (int)pos.Y, (int)(dem * texScale.X), (int)(dem * texScale.X));

                // Check if the enemy's rectangle is within the bounds of the viewport.
                if (IsObjectInVisibleRoom(enemRec))
                {
                    Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pixel], pos, enemRec, enemObj.dead ? Color.Red : Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, Global.currentScene.GetObjectLayerDepth(LayerDepth.Enemies));
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
                if (IsObjectInVisibleRoom(bulletRect))
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

        private bool IsObjectInVisibleRoom(Rectangle rec)
        {
            // Check if the object is within the bounds of the viewport.
            foreach (Room roomObj in Global.currentSceneData.rooms)
            {
                Vector2 roomPos = ObjectPos(roomObj);
                Vector2 origin = new Vector2(roomObj.texture.Width / 2, roomObj.texture.Height / 2);
                Rectangle roomBounds = new Rectangle((int)(roomPos.X - origin.X * texScale.X), (int)(roomPos.Y - origin.Y * texScale.Y), (int)(roomObj.texture.Width * texScale.X), (int)(roomObj.texture.Height * texScale.Y));

                if (recViewPortWithBuffer.Contains(roomBounds) && roomBounds.Contains(rec))
                {
                    return true;
                }
            }

            return false;
        }


    }
}
