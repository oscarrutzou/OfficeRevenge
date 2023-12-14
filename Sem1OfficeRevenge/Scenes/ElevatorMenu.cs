using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public class ElevatorMenu : Scene
    {
        #region Variables
        private Button nextLvlBtn;
    
        private Button pistolBtn;
        private Button rifleBtn;
        private Button shotgunBtn;

        private int riflePrice = 50;
        private int shotgunPrice = 100;

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

            //Global.world.Fullscreen();
            // Reset uiCamera's position and origin
            Global.world.uiCamera.position = Vector2.Zero;
            Global.world.uiCamera.origin = Vector2.Zero;


            InitMainMenu();
            SetPositions();
            
        }


        private void InitMainMenu()
        {
            // Instantiate the buttons
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
                    ChangeToPistol);
            
            rifleBtn = new Button(Vector2.Zero,
                    "50",
                    GlobalTextures.textures[TextureNames.GuiEleBtnNormal],
                    true,
                    ChangeToRifle);

            shotgunBtn = new Button(Vector2.Zero,
                    "100",
                    GlobalTextures.textures[TextureNames.GuiEleBtnNormal],
                    true,
                    ChangeToShotgun);

            // Add the buttons to the scene
            Global.currentScene.Instantiate(nextLvlBtn);
            Global.currentScene.Instantiate(pistolBtn);
            Global.currentScene.Instantiate(rifleBtn);
            Global.currentScene.Instantiate(shotgunBtn);
        }

        // Change to the next level
        private async void NextLevel()
        {
            if (Global.world.curfloorLevel < 5) Global.world.curfloorLevel++;
            GlobalSound.PlaySound(SoundNames.ElevatorDing);
            await Task.Delay(1000);
            GlobalSound.PlaySound(SoundNames.ElevatorDoorOpen);
            Global.world.ChangeScene(Scenes.TestBaseScene);
        }

        private void ChangeToPistol()
        {
            if (ScoreManager.killCount > 0)
                Global.world.currentWeapon = Global.world.pistol;
        }
        private void ChangeToRifle()
        {
            if (ScoreManager.killCount > riflePrice)
                Global.world.currentWeapon = Global.world.rifle;
        }
        private void ChangeToShotgun()
        {
            if (ScoreManager.killCount > shotgunPrice)
                Global.world.currentWeapon = Global.world.shotgun;
        }

        private void SetPositions()
        {
            // Set the positions of the buttons
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
            // Change the color of the button based on the cost of the weapon
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
            // Update the score
            ScoreManager.UpdateScore();

            ChangeColorOfBtn(pistolBtn, 0);
            ChangeColorOfBtn(rifleBtn, 50);
            ChangeColorOfBtn(shotgunBtn, 100);

            base.Update();
        }


        public override void  DrawOnScreen()
        {
            // Draw the buttons
            base.DrawOnScreen();
         
            ScoreManager.DrawScore(Color.Black);

            Vector2 bgScale = new Vector2(1f,1f);
            if (Global.graphics.PreferredBackBufferWidth > 1300)
                bgScale = new Vector2(1f,1.3f);
            

            Vector2 bgPos = Global.world.uiCamera.Center - new Vector2(GlobalTextures.textures[TextureNames.ElevatorBgSmall].Width * bgScale.X / 2, GlobalTextures.textures[TextureNames.ElevatorBgSmall].Height * bgScale.Y / 2);

            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.ElevatorBgSmall], bgPos, null, Color.White, 0f, Vector2.Zero, bgScale, SpriteEffects.None, 0f);


            Vector2 textBgPos = Global.world.uiCamera.Center + new Vector2(-GlobalTextures.textures[TextureNames.GuiEleText].Width * bgScale.X / 2, GlobalTextures.textures[TextureNames.GuiEleText].Height * bgScale.Y / 2 - 300);
            Global.spriteBatch.Draw(GlobalTextures.textures[TextureNames.GuiEleText], textBgPos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            string text = $"Floor {Global.world.curfloorLevel}/{Global.world.maxFloorLevels}";
            
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
