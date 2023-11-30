using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Sem1OfficeRevenge
{
    public enum Scenes
    {
        MainMenu,
        LoadingScreen,
        Level,
        EndMenu,
        TestJasper,
        TestLeonard,
        TestMarc,
        TestOscar,
        TestBaseScene,
    }

    public enum LayerDepth
    {
        Background,
        InteractableObjects,
        Enemies,
        Player,
        ScreenOverLay,
        GuiObjects,
        GuiText,
        FullOverlay
    }

    public abstract class Scene
    {
        // We have a data stored on each scene, to make it easy to add and remove gameObjects
        public bool hasFadeOut;
        public bool isPaused;

        public abstract void Initialize();

        /// <summary>
        /// The base update on the scene handles all the gameobjects and calls Update on them all. 
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
                gameObject.animation?.AnimationUpdate();
                gameObject.Update();
            }
        }

        public virtual void DrawInWorld()
        {
            Global.graphics.GraphicsDevice.Clear(Color.DarkRed);
            
            foreach (GameObject gameObject in Global.currentSceneData.gameObjects)
            {
                if (gameObject is Gui) return;
                gameObject.Draw();
            }
        }
        public virtual void DrawOnScreen()
        {
            foreach (GameObject guiGameObject in Global.currentSceneData.guis)
            {
                guiGameObject.Draw();
            }
        }


        #region Instantiate and Object LayerDepths
        public void Instantiate(GameObject gameObject) => Global.currentSceneData.gameObjectsToAdd.Add(gameObject);
        public void Instantiate(GameObject[] gameObjects) => Global.currentSceneData.gameObjectsToAdd.AddRange(gameObjects);
        public void SetObjectLayerDepth(GameObject gameObject, LayerDepth layer)
        {
            switch (layer)
            {
                case LayerDepth.Background:
                    gameObject.layerDepth = 0.1f;
                    break;
                case LayerDepth.InteractableObjects:
                    gameObject.layerDepth = 0.2f;
                    break;
                case LayerDepth.Enemies:
                    gameObject.layerDepth = 0.3f;
                    break;
                case LayerDepth.Player:
                    gameObject.layerDepth = 0.4f;
                    break;
                case LayerDepth.ScreenOverLay:
                    gameObject.layerDepth = 0.8f;
                    break;
                case LayerDepth.GuiObjects:
                    gameObject.layerDepth = 0.9f;
                    break;
                case LayerDepth.GuiText:
                    gameObject.layerDepth = 0.99f;
                    break;
                case LayerDepth.FullOverlay:
                    gameObject.layerDepth = 1f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }
        }
        public float GetObjectLayerDepth(LayerDepth layer)
        {
            switch (layer)
            {
                case LayerDepth.Background:
                    return 0.1f;
                case LayerDepth.InteractableObjects:
                    return 0.2f;
                case LayerDepth.Enemies:
                    return 0.3f;
                case LayerDepth.Player:
                    return 0.4f;
                case LayerDepth.ScreenOverLay:
                    return 0.8f;
                case LayerDepth.GuiObjects:
                    return 0.9f;
                case LayerDepth.GuiText:
                    return 0.99f;
                case LayerDepth.FullOverlay:
                    return 1f;
                default:
                    throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }
        }
        #endregion

        #region Sort Objects
        /// <summary>
        /// If the list has the bool "isRemoved" then it removes it from the list
        /// </summary>
        private void RemoveObjectsFromList()
        {
            Global.currentSceneData.enemies.RemoveAll(enemy => enemy.isRemoved);
            Global.currentSceneData.bullets.RemoveAll(bullet => bullet.isRemoved);
            Global.currentSceneData.guis.RemoveAll(gui => gui.isRemoved);
            Global.currentSceneData.defults.RemoveAll(defultsObj => defultsObj.isRemoved);
        }

        /// <summary>
        /// Add all lists to the the gameObjects list that contains all gameObjects on the scene
        /// </summary>
        private void AddObjectsToList()
        {
            Global.currentSceneData.gameObjects.AddRange(Global.currentSceneData.enemies);
            Global.currentSceneData.gameObjects.AddRange(Global.currentSceneData.bullets);
            Global.currentSceneData.gameObjects.AddRange(Global.currentSceneData.guis);
            Global.currentSceneData.gameObjects.AddRange(Global.currentSceneData.defults);
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

                    default:
                        Global.currentSceneData.defults.Add(obj);
                        break;

                }
            }
        }
        #endregion


    }
}
