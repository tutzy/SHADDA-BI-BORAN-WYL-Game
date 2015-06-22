using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ClickPatterns
{
    public class DefaultStrategy : IStrategyPattern
    {
        public char[,] StrategyPattern
        {
            get 
            {
                return new char[3,3] {
                                        {'X', ' ', 'X'},
                                        {' ', 'X', ' '},
                                        {'X', ' ', 'X'},
                                    };
            }
        }
    }
}
