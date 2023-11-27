using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public enum AnimNames
    {
        //Player Rifle
        PlayerRifleIdle,
        PlayerRifleMove,
        PlayerRifleShoot,
        PlayerRifleReload,

        GuiLoadingScreenIcon,
    }

    public static class GlobalAnimations
    {
        private static Dictionary<AnimNames, List<Texture2D>> animations = new Dictionary<AnimNames, List<Texture2D>>();
        public static float progress = 0f;


        public static async Task LoadContent()
        {
            int totalAnimations = Enum.GetNames(typeof(AnimNames)).Length - 1; //-1 since the loading screen icon already has been loaded.

            await LoadAnimation(AnimNames.PlayerRifleIdle, "Player\\Top_Down_Survivor\\rifle\\idle\\survivor-idle_rifle_", 20, totalAnimations);
            await LoadAnimation(AnimNames.PlayerRifleMove, "Player\\Top_Down_Survivor\\rifle\\move\\survivor-move_rifle_", 20, totalAnimations);
            await LoadAnimation(AnimNames.PlayerRifleShoot, "Player\\Top_Down_Survivor\\rifle\\shoot\\survivor-shoot_rifle_", 3, totalAnimations);
            await LoadAnimation(AnimNames.PlayerRifleReload, "Player\\Top_Down_Survivor\\rifle\\reload\\survivor-reload_rifle_", 20, totalAnimations);
        }

        private static async Task LoadAnimation(AnimNames animationName, string path, int framesInAnim, int totalAnimations)
        {
            List<Texture2D> animList = new List<Texture2D>();
            for (int i = 0; i < framesInAnim; i++)
            {
                try
                {
                    animList.Add(Global.world.Content.Load<Texture2D>(path + i));
                }
                catch (NullReferenceException)
                {
                    throw;
                }
                await Task.Delay(30); // Wait for 100 milliseconds
            }
            animations[animationName] = animList;
            progress += 1f / totalAnimations; // Update the progress after each animation is loaded
        }

        public static void LoadLoadingScreenIcon()
        {
            List<Texture2D> animList = new List<Texture2D>();
            for (int i = 0; i < 3; i++)
            {
                animList.Add(Global.world.Content.Load<Texture2D>("GUI\\Icons\\icon_timeglass_" + i));
            }
            animations[AnimNames.GuiLoadingScreenIcon] = animList;
        }

        public static Animation SetObjAnimation(AnimNames name)
        {
            return new Animation(animations[name]);
        }
    }


    //public static class GlobalAnimations
    //{
    //    private static Dictionary<AnimNames, List<Texture2D>> animations;
    //    public static float progress = 0f;

    //    public static void LoadContent()
    //    {
    //        animations = new Dictionary<AnimNames, List<Texture2D>>();
    //        int totalAnimations = Enum.GetNames(typeof(AnimNames)).Length;

    //        LoadAnimation(AnimNames.PlayerRifleIdle, "Player\\Top_Down_Survivor\\rifle\\idle\\survivor-idle_rifle_", 20, totalAnimations);
    //        LoadAnimation(AnimNames.PlayerRifleMove, "Player\\Top_Down_Survivor\\rifle\\move\\survivor-move_rifle_", 20, totalAnimations);
    //        LoadAnimation(AnimNames.PlayerRifleShoot, "Player\\Top_Down_Survivor\\rifle\\shoot\\survivor-shoot_rifle_", 3, totalAnimations);
    //        LoadAnimation(AnimNames.PlayerRifleReload, "Player\\Top_Down_Survivor\\rifle\\reload\\survivor-reload_rifle_", 20, totalAnimations);
    //    }

    //    private static void LoadAnimation(AnimNames animationName, string path, int framesInAnim, int totalAnimations)
    //    {
    //        List<Texture2D> animList = new List<Texture2D>();
    //        for (int i = 0; i < framesInAnim; i++)
    //        {
    //            animList.Add(Global.world.Content.Load<Texture2D>(path + i));
    //        }
    //        animations[animationName] = animList;
    //        progress += 1f / totalAnimations; // Update the progress after each animation is loaded
    //    }

    //    public static Animation SetObjAnimation(AnimNames name)
    //    {
    //        return new Animation(animations[name]);
    //    }
    //}

}
