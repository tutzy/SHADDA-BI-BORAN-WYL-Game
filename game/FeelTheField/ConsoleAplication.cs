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
            char[,] matrix = new char[row,col];
            FillWithChar(matrix, ch1);
            PrintField(matrix);

        }

        public static void PrintField(char[,] field)
        {
            StringBuilder fillTheField = new StringBuilder();
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    fillTheField.Append(field[i, j]);
                }
                fillTheField.AppendLine();
            }
            Console.WriteLine(fillTheField);
        }

        public static void FillWithChar(char[,] inputChar, char addChar)
        {
            for (int i = 0; i < inputChar.GetLength(0); i++)
            {
                for (int j = 0; j < inputChar.GetLength(1); j++)
                {
                    inputChar[i, j] = addChar;
                }
            }
        }



    }
}
