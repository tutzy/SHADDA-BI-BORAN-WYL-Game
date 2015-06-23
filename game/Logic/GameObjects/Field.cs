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
        private bool isFull;

        public Field(int rows, int cols)
        {
            this.Matrix = new GamePlate[rows, cols];
            this.isFull = false;
        }

        public IPlate[,] Matrix { get; set; }

        public bool MatrixIsFull
        {
            get
            {
                return this.isFull;
            }
            set
            {
                this.isFull = value;
            }
        }
    }
}
