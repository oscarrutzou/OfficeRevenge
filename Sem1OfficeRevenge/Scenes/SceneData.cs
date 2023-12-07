using Sem1OfficeRevenge.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1OfficeRevenge
{
    
    public class SceneData
    {
        public int score;
        public int highScore;

        public List<GameObject> gameObjects = new List<GameObject>(); //All gameObjects in the scene
        public List<GameObject> gameObjectsToAdd = new List<GameObject>(); //This list sorts the gameobject into the right list and adds it to the gameObject list

        public List<GenericEnemy> enemies = new List<GenericEnemy>();
        public List<Bullet> bullets = new List<Bullet>();
        public List<Blood> bloods = new List<Blood>();
        public List<Gui> guis = new List<Gui>();
        public List<Room> rooms = new List<Room>();

        public List<GameObject> defaults = new List<GameObject>();
    }
}
