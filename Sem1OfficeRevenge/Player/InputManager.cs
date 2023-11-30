using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sem1OfficeRevenge.World;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata;

namespace Sem1OfficeRevenge
{
    public static class InputManager
    {
        public static KeyboardState keyboardState;
        public static MouseState mouseState;
        // Prevents multiple click when clicking a button
        public static MouseState previousMouseState;

        public static Vector2 mousePositionInWorld;
        public static Vector2 mousePositionOnScreen;
        public static bool mouseClicked;

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

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Global.world.Exit();
            }

            PlayerInput();

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                CheckButtons();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                keyPressed = true;
            }
            else
            {
                keyPressed = false;
            }

            mouseClicked = (Mouse.GetState().LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released);

            previousMouseState = mouseState;
        }

        public static void PlayerInput()
        {
            if (Global.player != null)
            {
                Vector2 dir = mousePositionInWorld - Global.player.position;
                dir.Normalize();

                // Calculate the offset vector perpendicular to the direction vector
                Vector2 offset = new Vector2(-dir.Y, dir.X) * -50; // 50 is the offset distance in px

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

                if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.D))
                {
                    anyMoveKeyPressed = true;
                }
                else
                {
                    anyMoveKeyPressed = false;
                }

                //Fjern
                if (Mouse.GetState().RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
                {
                    Global.world.ChangeScene(Scenes.MainMenu);
                }

            }
        }


        private static void CheckButtons()
        {
            if (Global.currentSceneData.guis != null)
            {
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
