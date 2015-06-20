using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IPosition
    {
        int Row { get; set; }
        int Col { get; set; }
    }
}
