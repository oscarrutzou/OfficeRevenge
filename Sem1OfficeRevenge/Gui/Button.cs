using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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

        public override void Update()
        {
            
        }
    }
}
