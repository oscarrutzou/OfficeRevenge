using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    public class SceneData
    {
        //All gameObjects in the scene
        public List<GameObject> gameObjects = new List<GameObject>(); 
        //This list sorts the gameobject into the right list and adds it to the gameObject list
        public List<GameObject> gameObjectsToAdd = new List<GameObject>(); 

        public List<GenericEnemy> enemies = new List<GenericEnemy>();
        public List<Bullet> bullets = new List<Bullet>();
        public List<Blood> bloods = new List<Blood>();
        public List<Gui> guis = new List<Gui>();
        public List<Room> rooms = new List<Room>();

        public List<GameObject> defaults = new List<GameObject>();
    }
}
