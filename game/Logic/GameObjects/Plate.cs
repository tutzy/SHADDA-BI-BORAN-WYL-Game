using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Logic.Enumerations;
using Logic.Interfaces;

namespace Logic.GameObjects
{
    public abstract class Plate : GameObject, IPlate
    {
        public State ObjeState { get; set; }
        public IPosition Coordinates { get; set; }

        public IGameObject GetGameObject()
        {
          throw new NotImplementedException();
        }
        
        //[modifiers: public, private] [declaringType static] [void, int, IRenredable] [Name] [Parameters]
    }
}
