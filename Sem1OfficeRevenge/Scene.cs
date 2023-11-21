using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Revenge
{
    internal abstract class Scene
    {
        public abstract void Initialize();

        public virtual void Update() 
        { 
        
        }

        public virtual void Draw()
        {

        }

        public virtual void RemoveObjectsFromList()
        {

        }

        private void SortIntoCategories(List<GameObject> gameObjectsToAdd)
        {

        }

    }
}
