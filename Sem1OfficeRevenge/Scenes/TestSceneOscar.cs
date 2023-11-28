using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sem1OfficeRevenge
{
    public class TestSceneOscar : Scene
    {
        BlackScreenFade fadeInObj;

        public override void Initialize()
        {
            InitBlackOverLayFadeIn();
            
        }
        private void InitBlackOverLayFadeIn()
        {
            float fadeOutTimeSec = 1f;
            fadeInObj = new BlackScreenFade(fadeOutTimeSec, 1, 0, true);
            Global.currentScene.Instantiate(fadeInObj);
            fadeInObj.shouldFade = true;
        }

        public override void DrawInWorld()
        {
            base.DrawInWorld();
            
        }

        public override void Update()
        {
            base.Update();

        }

    }
}
