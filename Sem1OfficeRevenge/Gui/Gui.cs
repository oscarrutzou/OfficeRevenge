using System;

namespace Sem1OfficeRevenge
{
    // This class could be a interface but we chose to make it a class, since it's possible we want to add more function later on.
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
