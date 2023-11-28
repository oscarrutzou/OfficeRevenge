using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sem1OfficeRevenge
{
    static public class Collision
    {
        /// <summary>
        /// Bool if the sender object id colliding with the other gameObject, using a collisionBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="other">The target of a collision check</param>
        /// <returns></returns>
        static public bool IsCollidingBox(GameObject sender, GameObject other)
        {
            if (sender == other
                || sender == null
                || other == null) return false;

            return sender.collisionBox.Intersects(other.collisionBox);
        }
    }
}
