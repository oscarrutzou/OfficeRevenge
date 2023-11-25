using Sem1OfficeRevenge;
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Xna.Framework.Graphics;

public class LoadingScene : Scene
{
    private bool isLoading = false;
    //private float progress = 0f;

    public override async void Initialize()
    {
        isLoading = true;
        await Task.Run(() => LoadContent());
        OnContentLoaded();
    }

    private async Task LoadContent()
    {
        await GlobalAnimations.LoadContent();
    }

    private void OnContentLoaded()
    {
        // Switch to the main menu when the content is loaded
        Global.world.ChangeScene(Scenes.TestOscar);
        isLoading = false;
    }

    public override void Update()
    {
        base.Update();

        // Optionally, update your loading screen animation here
        if (isLoading)
        {
            // Update loading screen animation
        }
    }

    public override void DrawOnScreen()
    {
        base.DrawOnScreen();
        // Draw your loading screen here
        if (isLoading)
        {
            // Draw loading screen
            // Measure the size of the text
            string text = $"Loading: {GlobalAnimations.progress * 100}%";
            Vector2 textSize = GlobalTextures.defaultFont.MeasureString(text);

            // Calculate the position to center the text
            Vector2 textPosition = Global.world.camera.Center + new Vector2(0, -300) - textSize / 2;

            Global.spriteBatch.DrawString(GlobalTextures.defaultFont,
                                  text,
                                  textPosition,
                                  Color.Black,
                                  0,
                                  Vector2.Zero,
                                  1,
                                  SpriteEffects.None,
                                  Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        }
    }
}
