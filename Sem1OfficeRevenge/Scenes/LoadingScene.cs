using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    public class LoadingScene : Scene
    {
        // Start loading content in a separate task
        Task.Run(() => LoadContent()).ContinueWith(t => OnContentLoaded());
        isLoading = true;
    }

    private async void LoadContent()
    {
        // Load your content here
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

    public override void DrawOnScreen()
    {
        base.DrawOnScreen();
        // Draw your loading screen here
        if (isLoading)
        {
            // Draw loading screen
            Vector2 pos = Global.world.camera.Center + new Vector2(0, -100);
            Global.spriteBatch.DrawString(GlobalTextures.defaultFont, $"Loading: {GlobalAnimations.progress * 100}%", pos, Color.Black);
        }
    }
}
