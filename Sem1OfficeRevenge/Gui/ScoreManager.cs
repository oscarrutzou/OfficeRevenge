﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Text;


namespace Sem1OfficeRevenge
{
    static public class ScoreManager
    {
        public static int killCount;
        
        public static void UpdateScore()
        {
            //Get score from file
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
                    //Write score to file
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

        public static void ResetScore()
        {
            //Delete file
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appdataPath, "OfficeRevengeData");
            Directory.CreateDirectory(folder);
            string path = Path.Combine(folder, "data.txt");
            killCount = 0;
            File.Delete(path);
        }

        public static void DrawScore()
        {
            //Draw score
            string text = $"Kill count {killCount}";
            Vector2 scorePosition = Global.world.uiCamera.origin + new Vector2(10, 10); 
            Global.spriteBatch.DrawString(GlobalTextures.defaultFont, text, scorePosition, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }

        public static void DrawScore(Color color)
        {
            //Draw score with custom color
            string text = $"Kill count {killCount}";
            Vector2 scorePosition = Global.world.uiCamera.origin + new Vector2(10, 10);
            Global.spriteBatch.DrawString(GlobalTextures.defaultFont, text, scorePosition, color, 0, Vector2.Zero, 1, SpriteEffects.None, Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }

    }
}
