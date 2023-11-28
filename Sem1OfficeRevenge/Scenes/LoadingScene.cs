using Sem1OfficeRevenge;
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LoadingScene : Scene
{
    private bool isLoading = false;
    private Icon loadingIcon;
    private Vector2 loadingTextPos;
    private BlackScreenFade fadeInObj;
    private int fadeInTimeInMilisec = 500;
    public override async void Initialize()
    {
        isLoading = true;
        InitLoadingIcon();
        InitBlackOverLayFade();
        await Task.Run(() => LoadContent());
        OnContentLoaded();
    }

    private async Task LoadContent()
    {
        await GlobalAnimations.LoadContent();
    }

    private async void OnContentLoaded()
    {
        // Switch to the main menu when the content is loaded after a delay

        fadeInObj.isVisible = true;
        fadeInObj.shouldFade = true;
        await Task.Delay(fadeInTimeInMilisec);
        isLoading = false;
        Global.world.ChangeScene(Scenes.TestOscar);
    }

    private void InitLoadingIcon()
    {
        Vector2 scale = new Vector2(0.3f, 0.3f);
        Vector2 position = Global.world.worldCamera.BottomRight + new Vector2(-50, -50);
        loadingIcon = new Icon(scale, 
                               position,
                               GlobalAnimations.SetAnimation(AnimNames.GuiLoadingScreenIcon));
        
        loadingIcon.animation.frameRate = 3;

        Global.currentScene.Instantiate(loadingIcon);
    }
    private void InitBlackOverLayFade()
    {
        float fadeInInSec = (float)fadeInTimeInMilisec / 1000;
        fadeInObj = new BlackScreenFade(fadeInInSec, 0, 1);
        Global.currentScene.Instantiate(fadeInObj);
        fadeInObj.isVisible = false;
        fadeInObj.shouldFade = false;
    }

    public override void DrawOnScreen()
    {
        base.DrawOnScreen();
        // Draw your loading screen here
        if (!isLoading) return;

        // Draw loading screen
        // Measure the size of the text
        string text = $"Loading: {GlobalAnimations.progress * 100}%";
        Vector2 textSize = GlobalTextures.defaultFont.MeasureString(text);

        // Calculate the position to center the text
        loadingTextPos = Global.world.worldCamera.BottomCenter + new Vector2(0, -50) - textSize / 2;

        Global.spriteBatch.DrawString(GlobalTextures.defaultFont,
                              text,
                              loadingTextPos,
                              Color.Black,
                              0,
                              Vector2.Zero,
                              1,
                              SpriteEffects.None,
                              Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
        //if (hasInitIcon) return;

        //InitLoadingIcon();

    }
}
