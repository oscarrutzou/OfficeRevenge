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
    }

    public static class GlobalAnimations
    {
        private static Dictionary<AnimNames, List<Texture2D>> animations;

        public static void LoadContent()
        {
            animations = new Dictionary<AnimNames, List<Texture2D>>();

            SetAnimation(AnimNames.PlayerRifleIdle, "Player\\Top_Down_Survivor\\rifle\\idle\\survivor-idle_rifle_", 20);
            SetAnimation(AnimNames.PlayerRifleMove, "Player\\Top_Down_Survivor\\rifle\\move\\survivor-move_rifle_", 20);
            SetAnimation(AnimNames.PlayerRifleShoot, "Player\\Top_Down_Survivor\\rifle\\shoot\\survivor-shoot_rifle_", 3);
            SetAnimation(AnimNames.PlayerRifleReload, "Player\\Top_Down_Survivor\\rifle\\reload\\survivor-reload_rifle_", 20);
        }

        private static void SetAnimation(AnimNames animationName, string path, int framesInAnim)
        {
            List<Texture2D> animList = new List<Texture2D>();
            
            //In a sperate function to easily add the animations to the animations dictionary
            for (int i = 0; i < framesInAnim; i++)
            {
                animList.Add(Global.world.Content.Load<Texture2D>(path + i));
                await Task.Delay(100);
            }
            
            animations[animationName] = animList;
        }

        public static Animation SetObjAnimation(AnimNames name)
        {
            return new Animation(animations[name]);
        }
    }

}
