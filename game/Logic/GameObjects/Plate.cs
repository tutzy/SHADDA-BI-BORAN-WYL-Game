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
    public abstract class Plate : IPlate
    {
        protected const char DEFAULT_CLOSED_PLATE = 'X';
        protected const char DEFAULT_OPENED_PLATE = '@';
        private char plate;

        public Plate(IPosition position)
        {
            this.ObjeState = State.Closed;
            this.Coordinates = position;
        }

        public State ObjeState { get; set; }
        public IPosition Coordinates { get; private set; }

        public char Body
        {
            get
            {
                if (this.ObjeState == Enumerations.State.Closed)
                {
                    return DEFAULT_CLOSED_PLATE;
                }

                return this.plate == ' ' ? DEFAULT_OPENED_PLATE : this.plate;
            }
            set
            {
                this.plate = value;
            }
        }

        public void ChangeState() 
        {
            if (this.ObjeState == State.Closed)
            {
                this.ObjeState = State.Open;
            }
            else
            {
                this.ObjeState = State.Closed;
            }
        }
    }
}
