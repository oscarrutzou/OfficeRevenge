using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Sem1OfficeRevenge
{
    public static class InputManager
    {
        public static KeyboardState keyboardState;
        public static MouseState mouseState;
        /// <summary>
        /// Prevents multiple click when clicking a button
        /// </summary>
        public static MouseState previousMouseState;
        public static Vector2 mousePositionOnScreen;

        /// <summary>
        /// Gets called in GameWorld, at the start of the update
        /// </summary>
        public static void HandleInput()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            //Sets the mouse position
            mousePositionOnScreen = new Vector2(mouseState.X, mouseState.Y);

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Global.world.Exit();
            }

            previousMouseState = mouseState;
        }
    }
}
