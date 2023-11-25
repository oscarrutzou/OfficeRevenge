using Sem1OfficeRevenge;
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

public class LoadingScene : Scene
{
    private bool isLoading = false;
    //private float progress = 0f;

    public override void Initialize()
    {
        // Start loading content in a separate task
        Task.Run(() => LoadContent()).ContinueWith(t => OnContentLoaded());
        isLoading = true;
    }

    private void LoadContent()
    {
        // Load your content here
        GlobalAnimations.LoadContent();
    }

    private void OnContentLoaded()
    {
        // Switch to the main menu when the content is loaded
        //Global.world.ChangeScene(Scenes.MainMenu);
        //isLoading = false;
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
            Vector2 pos = Global.world.camera.Center + new Vector2(0, -300);
            Global.spriteBatch.DrawString(GlobalTextures.defaultFont, $"Loading: {GlobalAnimations.progress * 100}%", pos, Color.Black);
        }
    }
}
