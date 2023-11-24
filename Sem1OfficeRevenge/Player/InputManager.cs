﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Metadata;

namespace Sem1OfficeRevenge
{
    public static class InputManager
    {
        public static KeyboardState keyboardState;
        public static MouseState mouseState;
        // Prevents multiple click when clicking a button
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
            mousePositionOnScreen = GetMousePositionInWorld();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Global.world.Exit();
            }

            PlayerInput();

            //var direction = new Vector2((float)Math.Cos(Player.rotation), -(float)Math.Sin(Player.rotation));
            //if (keyboardState.IsKeyDown(Keys.A))
            //{
            //    Player.rotation -= MathHelper.ToRadians(Player.rotationVelocity);
            //}
            //else if (keyboardState.IsKeyDown(Keys.D))
            //{
            //    Player.rotation += MathHelper.ToRadians(Player.rotationVelocity);
            //}

            //if (keyboardState.IsKeyDown(Keys.W))
            //{
            //    Player.position += direction * 4f;
            //}
            //if (keyboardState.IsKeyDown(Keys.S))
            //{
            //    Player.position -= direction * 4f;
            //}

            previousMouseState = mouseState;


        }
        
        public static void PlayerInput()
        {
            if (Global.player != null)
            {
                var direction = new Vector2((float)Math.Cos(Global.player.rotation), -(float)Math.Sin(Global.player.rotation));
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    Global.player.rotation -= MathHelper.ToRadians(Global.player.rotationVelocity);
                }
                else if (keyboardState.IsKeyDown(Keys.D))
                {
                    Global.player.rotation += MathHelper.ToRadians(Global.player.rotationVelocity);
                }

                if (keyboardState.IsKeyDown(Keys.W))
                {
                    Global.player.position += direction * 4f;
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    Global.player.position -= direction * 4f;
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
            Matrix invMatrix = Matrix.Invert(Global.world.camera.GetMatrix());

            return Vector2.Transform(pos, invMatrix);
        }

    }
}
