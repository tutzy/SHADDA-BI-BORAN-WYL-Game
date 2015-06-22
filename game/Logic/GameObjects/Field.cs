using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Interfaces;

namespace Logic.GameObjects
{
    public abstract class Field : IField
    {
        public Field(int rows, int cols)
        {
            this.Matrix = new GamePlate[rows, cols];
        }

        public IPlate[,] Matrix { get; set; }
    }
}
