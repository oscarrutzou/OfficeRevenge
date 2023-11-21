using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    public abstract class Scene
    {
        // We have a data stored on each scene, to make it easy to add and remove gameObjects
        private SceneData data = new SceneData();
        
        public Scene() 
        {
            Global.currentSceneData = data;
        }

        public abstract void Initialize();

        /// <summary>
        /// The base update on the scene handles all the gameobjects and calls Update on them all. 
        /// Very importnant that you call base.Update on a new scene, or nothing would work
        /// </summary>
        public virtual void Update() 
        {
            //Remove GameObjects marked for removal
            RemoveObjectsFromList();
            Global.currentSceneData.gameObjects.Clear();

            //Add GameObjects and sort them into the right categories
            AddObjectsToList();
            SortIntoCategories(Global.currentSceneData.gameObjectsToAdd);
            Global.currentSceneData.gameObjectsToAdd.Clear();

            // Call update on every GameObject in the active scene.
            foreach (GameObject gameObject in Global.currentSceneData.gameObjects)
            {
                gameObject.Update();
            }
        }

        public virtual void Draw()
        {
            Global.graphics.GraphicsDevice.Clear(Color.DimGray);
            
            foreach (GameObject gameObject in Global.currentSceneData.gameObjects)
            {
                gameObject.Draw();
            }
        }


        public void Instantiate(GameObject gameObject) => Global.currentSceneData.gameObjectsToAdd.Add(gameObject);
        public void Instantiate(GameObject[] gameObjects) => Global.currentSceneData.gameObjectsToAdd.AddRange(gameObjects);


        /// <summary>
        /// If the list has the bool "isRemoved" then it removes it from the list
        /// </summary>
        private void RemoveObjectsFromList()
        {
            Global.currentSceneData.enemies.RemoveAll(enemy => enemy.isRemoved);
            Global.currentSceneData.bullets.RemoveAll(bullet => bullet.isRemoved);
            Global.currentSceneData.guis.RemoveAll(gui => gui.isRemoved);
        }

        /// <summary>
        /// Add all lists to the the gameObjects list that contains all gameObjects on the scene
        /// </summary>
        private void AddObjectsToList()
        {
            Global.currentSceneData.gameObjects.AddRange(Global.currentSceneData.enemies);
            Global.currentSceneData.gameObjects.AddRange(Global.currentSceneData.bullets);
            Global.currentSceneData.gameObjects.AddRange(Global.currentSceneData.guis);
        }

        /// <summary>
        /// Sorts the gameObjects and adds them to the correct lists
        /// </summary>
        /// <param name="gameObjectsToAdd"></param>
        private void SortIntoCategories(List<GameObject> gameObjectsToAdd)
        {
            foreach (GameObject obj in gameObjectsToAdd)
            {
                switch (obj)
                {
                    case GenericEnemy:
                        Global.currentSceneData.enemies.Add((GenericEnemy)obj);
                        break;
                    case Bullet:
                        Global.currentSceneData.bullets.Add((Bullet)obj);
                        break;
                    case Gui:
                        Global.currentSceneData.guis.Add((Gui)obj);
                        break;
                }
            }
        }

    }
}
