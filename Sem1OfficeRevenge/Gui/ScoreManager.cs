using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sem1OfficeRevenge
{
    static internal class ScoreManager
    {
        public static int killCount = 0;
        

        public static void UpdateScore()
        {
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appdataPath, "OfficeRevengeData");
            Directory.CreateDirectory(folder);
            string path = Path.Combine(folder, "data.txt");
            FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            try
            {
                StreamReader reader = new StreamReader(stream);
                int temp; 
                int.TryParse(reader.ReadToEnd(), out temp);
                if (temp > killCount)
                {
                    killCount = temp;
                }
                else
                {
                    stream.SetLength(0);
                    byte[] info = new UTF8Encoding(true).GetBytes(killCount.ToString());
                    stream.Write(info, 0, info.Length);
                }
            }
            finally
            {
                stream.Close();
            }
        }

        public static void DrawScore(Vector2 pos)
        {
            string text = "Kill Count: " + killCount;
            //string text = $"Current animation {Global.player.animation.animationName} \n Frame {Global.player.animation.currentFrame} \n Timer {Global.player.animation.timer}";
            Vector2 scorePosition = Global.world.uiCamera.origin + new Vector2(10, 10); 
            Global.spriteBatch.DrawString(GlobalTextures.defaultFont, text, scorePosition, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }


    }
}
