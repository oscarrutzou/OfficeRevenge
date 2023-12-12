using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    public enum AnimNames
    {
        //Player Handgun
        PlayerHandgunIdle,
        PlayerHandgunMove,
        PlayerHandgunShoot,
        PlayerHandgunReload,

        //Player ShotGun
        PlayerShotGunIdle,
        PlayerShotGunMove,
        PlayerShotGunShoot,
        PlayerShotGunReload,

        //Player Rifle
        PlayerRifleIdle,
        PlayerRifleMove,
        PlayerRifleShoot,
        PlayerRifleReload,

        //NPC
        ChairAttack,
        ChairDeath,
        ChairWalk,
        CivDeath,
        CivWalk,
        CivInjured,
        NPCIdle,
        


        GuiLoadingScreenIcon,
        //GuiCircleButtonClickAnim,
    }

    public static class GlobalAnimations
    {
        private static Dictionary<AnimNames, List<Texture2D>> animations = new Dictionary<AnimNames, List<Texture2D>>();
        public static float progress = 0f;


        public static async Task LoadContent()
        {
            int totalAnimations = Enum.GetNames(typeof(AnimNames)).Length - 1; //-1 since the loading screen icon already has been loaded.

            await LoadAnimation(AnimNames.PlayerHandgunIdle, "Player\\Top_Down_Survivor\\handgun\\idle\\survivor-idle_handgun_", 20, totalAnimations);
            await LoadAnimation(AnimNames.PlayerHandgunMove, "Player\\Top_Down_Survivor\\handgun\\move\\survivor-move_handgun_", 20, totalAnimations);
            await LoadAnimation(AnimNames.PlayerHandgunShoot, "Player\\Top_Down_Survivor\\handgun\\shoot\\survivor-shoot_handgun_", 3, totalAnimations);
            await LoadAnimation(AnimNames.PlayerHandgunReload, "Player\\Top_Down_Survivor\\handgun\\reload\\survivor-reload_handgun_", 15, totalAnimations);

            await LoadAnimation(AnimNames.PlayerShotGunIdle, "Player\\Top_Down_Survivor\\shotgun\\idle\\survivor-idle_shotgun_", 20, totalAnimations);
            await LoadAnimation(AnimNames.PlayerShotGunMove, "Player\\Top_Down_Survivor\\shotgun\\move\\survivor-move_shotgun_", 20, totalAnimations);
            await LoadAnimation(AnimNames.PlayerShotGunShoot, "Player\\Top_Down_Survivor\\shotgun\\shoot\\survivor-shoot_shotgun_", 3, totalAnimations);
            await LoadAnimation(AnimNames.PlayerShotGunReload, "Player\\Top_Down_Survivor\\shotgun\\reload\\survivor-reload_shotgun_", 20, totalAnimations);

            await LoadAnimation(AnimNames.PlayerRifleIdle, "Player\\Top_Down_Survivor\\rifle\\idle\\survivor-idle_rifle_", 20, totalAnimations);
            await LoadAnimation(AnimNames.PlayerRifleMove, "Player\\Top_Down_Survivor\\rifle\\move\\survivor-move_rifle_", 20, totalAnimations);
            await LoadAnimation(AnimNames.PlayerRifleShoot, "Player\\Top_Down_Survivor\\rifle\\shoot\\survivor-shoot_rifle_", 3, totalAnimations);
            await LoadAnimation(AnimNames.PlayerRifleReload, "Player\\Top_Down_Survivor\\rifle\\reload\\survivor-reload_rifle_", 20, totalAnimations);


            await LoadAnimation(AnimNames.ChairAttack, "npctextures\\attack02\\attack02_00", 20, totalAnimations);
            await LoadAnimation(AnimNames.ChairDeath, "npctextures\\chairdeath\\death02_00", 17, totalAnimations);
            await LoadAnimation(AnimNames.ChairWalk, "npctextures\\chairwalk\\run00", 32, totalAnimations);
            await LoadAnimation(AnimNames.CivDeath, "npctextures\\civdeath\\death01_00", 13, totalAnimations);
            await LoadAnimation(AnimNames.CivWalk, "npctextures\\civwalk\\walk00", 32, totalAnimations);
            await LoadAnimation(AnimNames.NPCIdle, "npctextures\\idle\\idle00", 32, totalAnimations);
            await LoadAnimation(AnimNames.CivInjured, "npctextures\\injured\\eating00", 24, totalAnimations);




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
                await Task.Delay(5); //Ændre dette for at skifte for hurtigt det loader
            }
            animations[animationName] = animList;
            progress += 1f / totalAnimations; // Update the progress after each animation is loaded
        }

        public static Animation SetAnimation(AnimNames name)
        {
            return new Animation(animations[name], name);
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

        public static void LoadContentTestScenes()
        {
            int totalAnimations = Enum.GetNames(typeof(AnimNames)).Length - 1; //-1 since the loading screen icon already has been loaded.

            LoadAnimationTest(AnimNames.PlayerRifleIdle, "Player\\Top_Down_Survivor\\rifle\\idle\\survivor-idle_rifle_", 20, totalAnimations);
            LoadAnimationTest(AnimNames.PlayerRifleMove, "Player\\Top_Down_Survivor\\rifle\\move\\survivor-move_rifle_", 20, totalAnimations);
            LoadAnimationTest(AnimNames.PlayerRifleShoot, "Player\\Top_Down_Survivor\\rifle\\shoot\\survivor-shoot_rifle_", 3, totalAnimations);
            LoadAnimationTest(AnimNames.PlayerRifleReload, "Player\\Top_Down_Survivor\\rifle\\reload\\survivor-reload_rifle_", 20, totalAnimations);
        }

        private static void LoadAnimationTest(AnimNames animationName, string path, int framesInAnim, int totalAnimations)
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
            }
            animations[animationName] = animList;
            progress += 1f / totalAnimations; // Update the progress after each animation is loaded
        }


    
    }
}
