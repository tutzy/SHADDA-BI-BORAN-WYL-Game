using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Engine;
using Logic.Enumerations;
using Logic.GameObjects;


namespace FeelTheField
{
    class ConsoleAplication
    {
        static void Main()
        {
            char closed = 'X';
            char open = '$';

            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());
            Console.Clear();

            char[,] matrix = new char[row, col];
            
            var engine = Engine.Instance;
            engine.ConfigureGameFieldSize(row, col);

            var gameField = engine.Field; // The Matrix

            DrawConsole.FillWithChar(matrix,'X');

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (gameField.Matrix[i, j].ObjeState == State.Open)
                    {
                        matrix[i, j] = open;
                        //matrix[i, j] = gameField.Matrix[i, j].Body;
                    }
                    else
                    {
                        matrix[i, j] = closed;
                        //matrix[i, j] = gameField.Matrix[i, j].Body;
                    }
                }
            }

            DrawConsole.PrintField(matrix);

           


        }
    }
}
