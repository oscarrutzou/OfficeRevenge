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
        }

        private static void SetAnimation(AnimNames animationName, string path, int framesInAnim)
        {
            List<Texture2D> animList = new List<Texture2D>();
            for (int i = 0; i < framesInAnim; i++)
            {
                animList.Add(Global.world.Content.Load<Texture2D>(path + i));
            }

            animations[animationName] = animList;
        }

        public static Animation SetObjAnimation(AnimNames name)
        {
            return new Animation(animations[name]);
        }
    }

}
