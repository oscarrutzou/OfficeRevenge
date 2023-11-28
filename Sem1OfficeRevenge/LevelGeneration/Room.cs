﻿using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Sem1OfficeRevenge
{
    internal class Room : GameObject
    {
        //public Texture2D map;
        int width;
        int height;
        private Vector2 origin;
        private Vector2 center;

        //private List<CivillianEnemy> CivEnemies = new List<CivillianEnemy>();
        //private CivillianEnemy civEnm;

        public Room(Texture2D Map, float rotation)
        {
            this.texture = Map;
            centerOrigin = true;
            this.rotation = rotation;
            this.scale = new Vector2(5f, 5f);

            layerDepth = Global.currentScene.GetObjectLayerDepth(LayerDepth.Background);


        }
        
        public override void Update()
        {
            //foreach (CivillianEnemy civ in CivEnemies)
            //{
            //    civ.Update();
            //}
        }

        public override void Draw()
        {
            //foreach (CivillianEnemy civ in CivEnemies)
            //{
            //    civ.Draw();
            //}

            base.Draw();
            
        }

    }
}
