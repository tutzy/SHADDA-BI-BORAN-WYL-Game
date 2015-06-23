
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wpf_client.Config
{
    public class Configuration
    {
        private static Configuration instance;

        public static string SEPARATOR = "I";
        public static string CLOSED_SYMBOL = "X";
        public static string OPEN_SYMBOL = "@";
        public static Brush OPEN_COLOR = Brushes.LightGreen;
        public static Brush CLOSED_COLOR = Brushes.LightBlue;
        private MainWindow mainWindowContext;
        private Grid mainGrid;
        private double phisHight;
        private double phisWidth;
        private int rows;
        private int cols;

        private Configuration()
        {
        }

        public static Configuration Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Configuration();
                }

                return instance;
            }
        }

        public void SetContext(MainWindow mainWindow)
        {
            this.mainWindowContext = mainWindow;
            this.mainGrid = mainWindowContext.main_grid;

            SetPhisicalMonitorSize();//set windows size and global width and hight
        }

        public void SetUpGrid(int rows, int cols, Action<Object, RoutedEventArgs> handler)
        {
            this.rows = rows;
            this.cols = cols;
            SetUpSizeOfGrid();
            SetUpRowColDefinitions(rows, cols);
            SetUpMouseEventListener(handler);
        }

        public void FillGridWithButtons()
        {
            for (int row = 0; row < this.rows; row++)
            {
                for (int col = 0; col < this.cols; col++)
                {
                    var b = new Button();
                    b.Content = CLOSED_SYMBOL;
                    b.Background = Brushes.LightBlue;
                    b.Name = SEPARATOR + row.ToString() + SEPARATOR + col.ToString();
                    this.mainGrid.Children.Add(b);

                    Grid.SetRow(b, row);
                    Grid.SetColumn(b, col);
                }
            }
        }

        private void SetPhisicalMonitorSize()
        {
            phisHight = System.Windows.SystemParameters.PrimaryScreenHeight * 0.60;
            phisWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 0.60;
            this.mainWindowContext.Width = phisWidth * (phisHight + cols) / (phisWidth - cols);
            this.mainWindowContext.Height = phisHight + rows * rows / 2.7 + phisWidth / phisHight; //no idea how this happened ... just works for now
        }


        private void SetUpMouseEventListener(Action<Object, RoutedEventArgs> handler)
        {
            var mouseClickHandlerDelegate = new RoutedEventHandler(handler);
            this.mainGrid.AddHandler(Grid.MouseDownEvent, mouseClickHandlerDelegate, true);
        }

        private void SetUpRowColDefinitions(int rows, int cols)
        {
            var rowDefinitionsList = new List<RowDefinition>();
            var colDefinitionsList = new List<ColumnDefinition>();

            //add row definitions
            for (int row = 0; row < rows; row++)
            {
                var rd = new RowDefinition();
                GridLength height = new GridLength(this.mainGrid.ActualHeight / rows);
                rd.Height = height;
                rowDefinitionsList.Add(rd);
                this.mainGrid.RowDefinitions.Add(rd);
            }

            //add col definitions
            for (int col = 0; col < cols; col++)
            {
                var cd = new ColumnDefinition();
                GridLength width = new GridLength(this.mainGrid.ActualHeight / cols);
                cd.Width = width;
                colDefinitionsList.Add(cd);
                this.mainGrid.ColumnDefinitions.Add(cd);
            }
        }

        private void SetUpSizeOfGrid()
        {
            this.mainGrid.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            this.mainGrid.Arrange(new Rect(0, 0, phisWidth, phisHight));
        }
    }
}
