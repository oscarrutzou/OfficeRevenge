using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct3D9;
using static System.Net.Mime.MediaTypeNames;

namespace Sem1OfficeRevenge
{
    public class ElevatorMenu : Scene
    {
        #region Variables
        private Button nextLvlBtn;
    
        private Button pistolBtn;
        private Button rifleBtn;
        private Button shotgunBtn;


        private float pistolScale = 0.15f;
        private float rifleScale = 0.22f;
        private float shotgunScale = 0.2f;
        Vector2 pistolPos;
        Vector2 riflePos;
        Vector2 shotgunPos;
        #endregion

        public override void Initialize()
        {
            GlobalSound.PlaySound(SoundNames.ElevatorDoorOpen); //Spil når man skifter scene i stedet for.

            Global.world.Fullscreen();
            // Reset uiCamera's position and origin
            Global.world.uiCamera.position = Vector2.Zero;
            Global.world.uiCamera.origin = Vector2.Zero;


            InitMainMenu();
            SetPositions();
            
        }


        private void InitMainMenu()
        {
            nextLvlBtn = new Button(Vector2.Zero,
                    "",
                    GlobalTextures.textures[TextureNames.GuiEleBtnUp],
                    true,
                    NextLevel);
            
            //Få farve med i text også
            pistolBtn = new Button(Vector2.Zero,
                    "0",
                    GlobalTextures.textures[TextureNames.GuiEleBtnNormal],
                    true,
                    ChangeWeapon);
            
            rifleBtn = new Button(Vector2.Zero,
                    "50",
                    GlobalTextures.textures[TextureNames.GuiEleBtnNormal],
                    true,
                    ChangeWeapon);

            shotgunBtn = new Button(Vector2.Zero,
                    "100",
                    GlobalTextures.textures[TextureNames.GuiEleBtnNormal],
                    true,
                    ChangeWeapon);


            Global.currentScene.Instantiate(nextLvlBtn);
            Global.currentScene.Instantiate(pistolBtn);
            Global.currentScene.Instantiate(rifleBtn);
            Global.currentScene.Instantiate(shotgunBtn);
        }



        private async void NextLevel()
        {
            if (Global.world.FloorLevel < 5) Global.world.FloorLevel++;
            GlobalSound.PlaySound(SoundNames.ElevatorDing);
            await Task.Delay(1000);
            GlobalSound.PlaySound(SoundNames.ElevatorDoorOpen);
            Global.world.ChangeScene(Scenes.TestBaseScene);
        }

        private void ChangeWeapon()
        {
            if (ScoreManager.killCount > 0)
            {
                //Change weapon on player?
            }
        }

        private void SetPositions()
        {
            pistolBtn.position = Global.world.uiCamera.Center + new Vector2(-200, 50);
            rifleBtn.position = Global.world.uiCamera.Center + new Vector2(0, 50);
            shotgunBtn.position = Global.world.uiCamera.Center + new Vector2(200, 50);
            nextLvlBtn.position = Global.world.uiCamera.Center + new Vector2(0, 200);

            pistolPos = new Vector2(pistolBtn.position.X - 55, pistolBtn.position.Y - 100);
            riflePos = new Vector2(rifleBtn.position.X - 90, rifleBtn.position.Y - 120);
            shotgunPos = new Vector2(shotgunBtn.position.X - 100, shotgunBtn.position.Y - 110);
        }

        private void ChangeColorOfBtn(Button button, int costOfWeapon)
        {
            if (ScoreManager.killCount >= costOfWeapon)
            {
                button.textColor = Color.Black;
            }
            else
            {
                button.textColor = Color.DarkRed;
            }
        }

        public override void Update()
        {
            ScoreManager.UpdateScore();

            ChangeColorOfBtn(pistolBtn, 0);
            ChangeColorOfBtn(rifleBtn, 50);
            ChangeColorOfBtn(shotgunBtn, 100);

            base.Update();
        }


        public override void  DrawOnScreen()
        {
            base.DrawOnScreen();
         
            ScoreManager.DrawScore(Color.Black);

            Vector2 bgScale = new Vector2(1f,1f);
            if (Global.graphics.PreferredBackBufferWidth > 1300)
                bgScale = new Vector2(1f,1.3f);
            

            Vector2 bgPos = Global.world.uiCamera.Center - new Vector2(GlobalTextures.textures[TextureNames.ElevatorBgSmall].Width * bgScale.X / 2, GlobalTextures.textures[TextureNames.ElevatorBgSmall].Height * bgScale.Y / 2);

            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.ElevatorBgSmall], bgPos, null, Color.White, 0f, Vector2.Zero, bgScale, SpriteEffects.None, 0f);


            Vector2 textBgPos = Global.world.uiCamera.Center + new Vector2(-GlobalTextures.textures[TextureNames.GuiEleText].Width * bgScale.X / 2, GlobalTextures.textures[TextureNames.GuiEleText].Height * bgScale.Y / 2 - 300);
            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.GuiEleText], textBgPos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            string text = $"Floor {Global.world.FloorLevel}/5";
            
            // Measure the size of the text
            Vector2 textSize = GlobalTextures.defaultFontMid.MeasureString(text);

            // Calculate the position to center the text
            Vector2 textPosition = textBgPos + new Vector2(20, 20);

            Global.spriteBatch.DrawString(GlobalTextures.defaultFontMid,
                                              text,
                                              textPosition,
                                              Color.Black,
                                              0,
                                              Vector2.Zero,
                                              1f,
                                              SpriteEffects.None,
                                              Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));

            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Pistol], pistolPos, null, Color.White, 0f, Vector2.Zero, pistolScale, SpriteEffects.None, 0.1f);
            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.Rifle], riflePos, null, Color.White, 0f, Vector2.Zero, rifleScale, SpriteEffects.None, 0.1f);
            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.ShotGun], shotgunPos, null, Color.White, 0f, Vector2.Zero, shotgunScale, SpriteEffects.None, 0.1f);
        }


    }
}
