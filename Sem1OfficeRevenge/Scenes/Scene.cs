using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public virtual void Update() 
        { 
            
        }

        public virtual void Draw()
        {
            Global.graphics.GraphicsDevice.Clear(Color.DimGray);
            

        }

        public virtual void AddObjectsToList()
        {

        }

        private void SortIntoCategories(List<GameObject> gameObjectsToAdd)
        {

        }

        public virtual void RemoveObjectsFromList()
        {

        }


    }
}
