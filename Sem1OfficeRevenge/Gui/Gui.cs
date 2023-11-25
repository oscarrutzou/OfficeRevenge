using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public abstract class Gui : GameObject
    {
        //Variabler til at kunne vælge mellem når man laver UI
        public string text;
        public Action onClick;

        public Gui() {
            Global.currentScene.SetObjectLayerDepth(this, LayerDepth.GuiObjects);
        }

    }
}
