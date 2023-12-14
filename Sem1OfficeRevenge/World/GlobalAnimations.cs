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
    }

    public static class GlobalAnimations
    {
        // Dictionary of all animations
        private static Dictionary<AnimNames, List<Texture2D>> animations = new Dictionary<AnimNames, List<Texture2D>>();
        public static float progress = 0f;

        public static void LoadContent()
        {
            LoadAnimation(AnimNames.PlayerHandgunIdle, "Player\\Top_Down_Survivor\\handgun\\idle\\survivor-idle_handgun_", 20);
            LoadAnimation(AnimNames.PlayerHandgunMove, "Player\\Top_Down_Survivor\\handgun\\move\\survivor-move_handgun_", 20);
            LoadAnimation(AnimNames.PlayerHandgunShoot, "Player\\Top_Down_Survivor\\handgun\\shoot\\survivor-shoot_handgun_", 3);
            LoadAnimation(AnimNames.PlayerHandgunReload, "Player\\Top_Down_Survivor\\handgun\\reload\\survivor-reload_handgun_", 15);

            LoadAnimation(AnimNames.PlayerShotGunIdle, "Player\\Top_Down_Survivor\\shotgun\\idle\\survivor-idle_shotgun_", 20);
            LoadAnimation(AnimNames.PlayerShotGunMove, "Player\\Top_Down_Survivor\\shotgun\\move\\survivor-move_shotgun_", 20);
            LoadAnimation(AnimNames.PlayerShotGunShoot, "Player\\Top_Down_Survivor\\shotgun\\shoot\\survivor-shoot_shotgun_", 3);
            LoadAnimation(AnimNames.PlayerShotGunReload, "Player\\Top_Down_Survivor\\shotgun\\reload\\survivor-reload_shotgun_", 20);

            LoadAnimation(AnimNames.PlayerRifleIdle, "Player\\Top_Down_Survivor\\rifle\\idle\\survivor-idle_rifle_", 20);
            LoadAnimation(AnimNames.PlayerRifleMove, "Player\\Top_Down_Survivor\\rifle\\move\\survivor-move_rifle_", 20);
            LoadAnimation(AnimNames.PlayerRifleShoot, "Player\\Top_Down_Survivor\\rifle\\shoot\\survivor-shoot_rifle_", 3);
            LoadAnimation(AnimNames.PlayerRifleReload, "Player\\Top_Down_Survivor\\rifle\\reload\\survivor-reload_rifle_", 20);

            LoadAnimation(AnimNames.ChairAttack, "npctextures\\attack02\\attack02_00", 20);
            LoadAnimation(AnimNames.ChairDeath, "npctextures\\chairdeath\\death02_00", 17);
            LoadAnimation(AnimNames.ChairWalk, "npctextures\\chairwalk\\run00", 32);
            LoadAnimation(AnimNames.CivDeath, "npctextures\\civdeath\\death01_00", 13);
            LoadAnimation(AnimNames.CivWalk, "npctextures\\civwalk\\walk00", 32);
            LoadAnimation(AnimNames.NPCIdle, "npctextures\\idle\\idle00", 32);
            LoadAnimation(AnimNames.CivInjured, "npctextures\\injured\\eating00", 24);
        }

        private static void LoadAnimation(AnimNames animationName, string path, int framesInAnim)
        {
            // Load all frames in the animation
            List<Texture2D> animList = new List<Texture2D>();
            for (int i = 0; i < framesInAnim; i++)
            {
                animList.Add(Global.world.Content.Load<Texture2D>(path + i));
            }
            animations[animationName] = animList;

            //-1 since the loading screen icon already has been loaded.
            int totalAnimations = Enum.GetNames(typeof(AnimNames)).Length - 1; 
            progress += 1f / totalAnimations; // Update the progress after each animation is loaded
        }

        public static Animation SetAnimation(AnimNames name)
        {
            // Check if the animation exists
            return new Animation(animations[name], name);
        }

        //Loaded before all other animations
        public static void LoadLoadingScreenIcon()
        {
            // Load the loading screen icon
            List<Texture2D> animList = new List<Texture2D>();
            for (int i = 0; i < 3; i++)
            {
                animList.Add(Global.world.Content.Load<Texture2D>("GUI\\Icons\\icon_timeglass_" + i));
            }
            animations[AnimNames.GuiLoadingScreenIcon] = animList;
        }
    }
}
