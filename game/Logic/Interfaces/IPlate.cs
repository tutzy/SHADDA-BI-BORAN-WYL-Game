﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
   public interface IPlate:IState
    {
         IPosition Coordinates { get; }

         char Body { get; set; }
    }
}
