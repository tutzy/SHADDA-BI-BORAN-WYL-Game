using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.ClickPatterns;
using Logic.Engine;
using Logic.Enumerations;
using Logic.GameObjects;
using Logic.Interfaces;


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

            DrawConsole.FillWithChar(matrix, 'X');

            UpdateMatrixAndPrintIt(engine, row, col, matrix, open, closed);
        }

        private static void UpdateMatrixAndPrintIt(Engine engine, int row, int col, char[,] matrix, char open, char closed)
        {

            while (true)
            {
                var gameField = engine.Field; // The Matrix
                if (gameField.MatrixIsFull)
                {
                    Console.Clear();
                    Console.WriteLine("Congrats you won!");
                    break;
                }


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

                
                int y = int.Parse(Console.ReadLine());
                int x = int.Parse(Console.ReadLine());

                var newPosition = new Position(y, x);

                engine.PlayWithPosition(newPosition);

                Console.Clear();
            }
        }
    }
}
