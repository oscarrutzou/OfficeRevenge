using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    public class Button: Gui
    {
        private Action onClick;
        private string text;
        public Button(Vector2 position, string text, Texture2D texture, Action onClick)
        {
            this.position = position;
            this.text = text;
            this.texture = texture;
            this.onClick = onClick;
        }

        public override void Draw()
        {
            base.Draw();
            DrawDebugText();
        }

        public override void OnCollisionBox()
        {
            base.OnCollisionBox();
        }

        public override void Update()
        {
            //scale += 0.001f;
        }

        private void DrawDebugText()
        {
            Global.spriteBatch.DrawString(GlobalTextures.defaultFont, $"Btn pos {position} + {InputManager.mousePositionOnScreen}", new Vector2(100,100), Color.Black);
        }
    }
}
