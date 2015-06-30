using Logic.GameObjects;
using Logic.ClickPatterns;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Engine
{
    public class Engine
    {
        private const int DEFAULT_ROWS = 3;
        private const int DEFAULT_COLS = 3;

        private static Engine instance;

        private int rows;
        private int cols;

        

        private IField field;
        private IStrategyPattern strategy;

        private Engine()
        {
            this.Field = new GameField(DEFAULT_ROWS, DEFAULT_COLS);
            this.strategy = InitDefaultStrategy();
        }

        public void ResetGame()
        {
            this.Field = new GameField(this.rows, this.cols);
            this.ConfigureGameFieldSize(rows, cols);
        }

        private IStrategyPattern InitDefaultStrategy()
        {
            return new DefaultStrategy();
        }

        public static Engine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Engine();
                }
                return instance;
            }
        }

        public IField Field
        {
            get
            {
                return this.field;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException();
                }
                this.field = value;
            }
        }

        //optional
        public void ConfigureGameFieldSize(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;

            this.Field = new GameField(rows, cols);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var currentPosition = new Position(row, col);
                    var currentGamePlate = new GamePlate(currentPosition);
                    this.Field.Matrix[row, col] = currentGamePlate;
                }
            }
        }

        public void ConfigureStrategy(IStrategyPattern strategy) 
        {
            this.strategy = strategy;
        }

        public IField PlayWithPosition(IPosition position)
        {
            if (!isValidPosition(position))
            {
                //TODO: implement what happens when position isn't valid
            }

            if (!isValidStrategy(this.strategy))
            {
                //TODO: implement what happens when strategy isn't valid
            }

            var strategyRows = this.strategy.StrategyPattern.GetLength(0);
            var strategyCols = this.strategy.StrategyPattern.GetLength(1);

            var rowsOffset = position.Row;
            var colsOffset = position.Col;
            for (int row = 0; row < strategyRows; row++)
            {
                var currentRow = row + rowsOffset - strategyRows / 2;

                if (isBetween(currentRow, 0, rows))
                {
                    for (int col = 0; col < strategyCols; col++)
                    {
                        var currentCol = col + colsOffset - strategyRows / 2;

                        if (isBetween(currentCol, 0, cols))
                        {
                            var currentTile = this.Field.Matrix[currentRow, currentCol];
                            
                            if (this.strategy.StrategyPattern[row, col] != ' ')
                            {
                                currentTile.ChangeState();
                            }
                        }
                    }
                }
            }

            CheckIfMatrixIsFull();
            return this.Field;
        }

        private void CheckIfMatrixIsFull()
        {
            var tilesCount = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (this.Field.Matrix[r, c].ObjeState == Enumerations.State.Open)
                    {
                        tilesCount++;
                    }
                }
            }

            if (tilesCount == rows * cols)
            {
                this.Field.MatrixIsFull = true;
            }
        }

        private bool isValidStrategy(IStrategyPattern strategy)
        {
            var rowsCountIsOdd = strategy.StrategyPattern.GetLength(0) % 2 == 1;
            var colsCountIsOdd = strategy.StrategyPattern.GetLength(1) % 2 == 1;

            return rowsCountIsOdd && colsCountIsOdd ? true : false;
        }

        private bool isBetween(int index, int min, int max)
        {
            return (index >= min && index < max) ? true : false;
        }

        private bool isValidPosition(IPosition position)
        {
            //TODO: implement validation of position
            return true;
        }
    }
}
