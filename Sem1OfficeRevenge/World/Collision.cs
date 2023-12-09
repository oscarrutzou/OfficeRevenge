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
        static public bool IntersectBox(GameObject sender, GameObject other)
        {
            if (sender == other
                || sender == null
                || other == null) return false;

            return sender.collisionBox.Intersects(other.collisionBox);
        }

        static public bool ContainsBox(GameObject sender, GameObject other)
        {
            if (sender == other
                || sender == null
                || other == null) return false;

            return other.collisionBox.Contains(sender.collisionBox);
        }

        static public bool ContainsEitherBox(Rectangle sender, Rectangle box1, Rectangle box2)
        {
            bool col1 = box1.Contains(sender);
            bool col2 = box2.Contains(sender);
            return (col1 || col2);
        }

        static public bool ContainsEitherBox(GameObject sender, Rectangle box1, Rectangle box2)
        {
            bool col1 = box1.Contains(sender.collisionBox);
            bool col2 = box2.Contains(sender.collisionBox);
            return (col1 || col2);
        }
    }
}
