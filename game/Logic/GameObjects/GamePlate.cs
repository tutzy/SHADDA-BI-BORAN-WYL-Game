using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.GameObjects
{
    public class GamePlate : Plate, IPlate
    {
        public GamePlate(IPosition position) 
            :base(position)
        {

        }
    }
}
