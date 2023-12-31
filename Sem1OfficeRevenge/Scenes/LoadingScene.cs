﻿using Sem1OfficeRevenge;
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LoadingScene : Scene
{
    private bool isLoading;
    private bool hasLoaded;
    private Icon loadingIcon;
    private Vector2 loadingTextPos;

    public override void Initialize()
    {
        isLoading = true;
        InitLoadingIcon();
        HandleHasLoaded();
    }

    private async void HandleHasLoaded()
    {
        if (!hasLoaded)
        {
            await Task.Run(() => GlobalAnimations.LoadContent()); //Wait for the loading to complete
            await Task.Delay(1000); //Wait for 1sec to make the transition better
            isLoading = false;
            hasLoaded = true;
            Global.world.ChangeScene(Scenes.GameScene);
            Global.world.RefreshWeapons(); 
        }
        else
        {
            isLoading = false;
            Global.world.ChangeScene(Scenes.GameScene);
            Global.world.RefreshWeapons();
        }
    }

    private void InitLoadingIcon()
    {
        Vector2 scale = new Vector2(0.3f, 0.3f);
        Vector2 position = Global.world.uiCamera.BottomRight + new Vector2(-50, -50);
        loadingIcon = new Icon(scale, 
                               position,
                               GlobalAnimations.SetAnimation(AnimNames.GuiLoadingScreenIcon));
        
        loadingIcon.animation.frameRate = 3;

        Global.currentScene.Instantiate(loadingIcon);
    }

    public override void DrawOnScreen()
    {
        base.DrawOnScreen();

        if (!isLoading) return;
        
        string text = $"Loading: {(int)Math.Round(GlobalAnimations.progress * 100, 0)}%";
        Vector2 textSize = GlobalTextures.defaultFont.MeasureString(text);

        // Calculate the position to center the text
        loadingTextPos = Global.world.uiCamera.BottomCenter + new Vector2(0, -50) - textSize / 2;

        Global.spriteBatch.DrawString(GlobalTextures.defaultFont,
                              text,
                              loadingTextPos,
                              Color.Black,
                              0,
                              Vector2.Zero,
                              1,
                              SpriteEffects.None,
                              Global.currentScene.GetObjectLayerDepth(LayerDepth.GuiText));
    }
}
