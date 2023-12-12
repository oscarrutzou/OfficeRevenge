using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sem1OfficeRevenge
{
    public static class InputManager
    {
        public static KeyboardState keyboardState;
        public static KeyboardState previousKeyboardState;
        public static MouseState mouseState;
        // Prevents multiple click when clicking a button
        public static MouseState previousMouseState;

        public static Vector2 mousePositionInWorld;
        public static Vector2 mousePositionOnScreen;
        public static bool mouseClicked;
        public static bool mouseRightClicked;

        private static bool noClip = true;
        private static float eleTimer = 0;
        private static int eleMovePlayerTime = 3;

        public static bool anyMoveKeyPressed;
        /// <summary>
        /// Gets called in GameWorld, at the start of the update
        /// </summary>
        public static void HandleInput()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            
            //Sets the mouse position
            mousePositionOnScreen = GetMousePositionOnUI();
            mousePositionInWorld = GetMousePositionInWorld();

            if (keyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape) && Global.player != null)
            {
                Global.currentScene.isPaused = !Global.currentScene.isPaused;

                if (Global.currentScene.isPaused)
                {
                    Global.world.pauseScreen.ShowPauseMenu();
                }
                else
                {
                    Global.world.pauseScreen.HidePauseMenu();
                }
            }

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                CheckButtons();
                //if (Global.player != null && !Global.currentScene.isPaused && !Global.world.blackScreenFadeInOut.isFadingIn) Global.player.DamagePlayer(20);
            }           

            PlayerInput();

            mouseClicked = (Mouse.GetState().LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released);
            mouseRightClicked = (Mouse.GetState().RightButton == ButtonState.Pressed) && (previousMouseState.RightButton == ButtonState.Released);

            previousMouseState = mouseState;
            previousKeyboardState = keyboardState;
        }

        public static void PlayerInput()
        {
            if (Global.currentScene.isPaused) return;

            if (Global.player != null)
            {
                Vector2 dir = mousePositionInWorld - Global.player.position;
                dir.Normalize();

                // Calculate the offset vector perpendicular to the direction vector
                Vector2 offset = new Vector2(-dir.Y, dir.X) * -Global.player.textureOffset; // 50 is the offset distance in px
                Vector2 tempPosition = Global.player.position; // Store the current position

                Global.player.RotateTowardsTargetWithOffset(mousePositionInWorld, offset);

                if (keyboardState.IsKeyDown(Keys.A))
                {
                    Global.player.position.X -= Global.player.playerSpeed;
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    Global.player.position.X += Global.player.playerSpeed;
                }
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    Global.player.position.Y -= Global.player.playerSpeed;                    
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    Global.player.position.Y += Global.player.playerSpeed;
                }

                //Noclip
                if (keyboardState.IsKeyDown(Keys.N) && !previousKeyboardState.IsKeyDown(Keys.N))
                {
                    noClip = !noClip;
                }

                CheckPlayerMoveColRoom(tempPosition);
                

                if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.D))
                {
                    anyMoveKeyPressed = true;
                }
                else
                {
                    anyMoveKeyPressed = false;
                }
            }
        }


        private static void CheckPlayerMoveColRoom(Vector2 tempPosition)
        {
            if (noClip) return;
            bool isInsideRoom = false;
            //bool isInsideHallway = false;

            // Check if the player's collision box is contained within any room's collision box or hallway collision box
            foreach (Room room in Global.currentSceneData.rooms)
            {
                if (Collision.ContainsEitherBox(Global.player, room.collisionBox, room.hallwayCol))
                {
                    isInsideRoom = true;
                }

                if (room.texture == GlobalTextures.textures[TextureNames.TileMap6] && Collision.ContainsBox(Global.player, room))
                {
                    eleTimer += (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            // If the player's collision box is not contained within any room's collision box or hallway collision box, revert the position
            if (!isInsideRoom)
            {
                Global.player.position = tempPosition;
            }

            if (eleTimer >= eleMovePlayerTime)
            {
                eleTimer = 0;
                if (Global.world.curfloorLevel == Global.world.maxFloorLevels)
                {
                    Global.world.playerWon = true;
                    Global.world.ChangeScene(Scenes.EndMenu);
                }
                else
                {
                    Global.world.ChangeScene(Scenes.ElevatorMenu);
                }
            }
        }


        private static void CheckButtons()
        {
            if (Global.currentSceneData.guis == null) return;

            foreach (Gui gui in Global.currentSceneData.guis)
            {
                if (gui is Button button)
                {
                    if (button.IsMouseOver() && button.isVisible)
                    {
                        button.OnClick();
                        return;  // Return early if a button was clicked
                    }
                }
            }
        }

       

        /// <summary>
        /// Translates the mouse's position into world space coordinates.
        /// </summary>
        /// <returns></returns>
        private static Vector2 GetMousePositionInWorld()
        {
            Vector2 pos = new Vector2(mouseState.X, mouseState.Y);
            Matrix invMatrix = Matrix.Invert(Global.world.playerCamera.GetMatrix());

            return Vector2.Transform(pos, invMatrix);
        }

        private static Vector2 GetMousePositionOnUI()
        {
            Vector2 pos = new Vector2(mouseState.X, mouseState.Y);
            Matrix invMatrix = Matrix.Invert(Global.world.uiCamera.GetMatrix());
            return Vector2.Transform(pos, invMatrix);
        }


    }
}
