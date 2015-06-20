using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Enumerations;
using Logic.Interfaces;

namespace Logic.GameObjects
{
    public abstract class GameObject : IGameObject
    {
        public IGameObject GetGameObject()
        {
            throw new NotImplementedException();
        }

        public State ObjeState
        {
            get;
            set;
        }
        public IPosition Coordinates
        {
            get;
            set;
        }
    }
}


