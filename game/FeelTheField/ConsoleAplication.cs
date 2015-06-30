using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Engine;
using Logic.GameObjects;


namespace FeelTheField
{
    class ConsoleAplication
    {
        static void Main()
        {
            char ch1 = 'X';
            char ch2 = '$';
            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());
            char[,] matrix = new char[row, col];


            DrowConsole.FillWithChar(matrix, ch1);
            DrowConsole.PrintField(matrix);
        }
    }
}
