using Logic.Engine;
using Logic.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_client.Helpers;

namespace Wpf_client
{
    public partial class MainWindow : Window
    {
        private const string SEPARATOR = "I";
        private const string CLOSED_SYMBOL = "X";
        private const string OPEN_SYMBOL = "@";
        private readonly Brush OPEN_COLOR = Brushes.LightGreen;
        private readonly Brush CLOSED_COLOR = Brushes.LightBlue;
        private int rows = 3;
        private int cols = 3;
        private double phisHight;
        private double phisWidth;
        private Engine engine;

        public MainWindow()
        {
            InitializeComponent(); //returns nothing so the grids actual size is still 0 so we need to set it up manualy

            this.engine = Engine.Instance;
            this.engine.ConfigureStrategy(null);
            this.engine.ConfigureGameFieldSize(rows, cols);

            SetPhisicalMonitorSize(); //set windows size and global width and hight
            //read user rows, cols

            SetUpGrid(rows, cols);
            FillGridWithButtons();
        }
        private void SetPhisicalMonitorSize()
        {
            phisHight = System.Windows.SystemParameters.PrimaryScreenHeight * 0.60;
            phisWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 0.60;
            this.Width = phisWidth * (phisHight + cols) / (phisWidth - cols);
            this.Height = phisHight + rows * rows / 2.7 + phisWidth / phisHight; //no idea how this happened ... just works for now
        }

        private void FillGridWithButtons()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var b = new Button();
                    b.Content = CLOSED_SYMBOL;
                    b.Background = Brushes.LightBlue;
                    b.Name = SEPARATOR + row.ToString() + SEPARATOR + col.ToString();
                    this.main_grid.Children.Add(b);

                    Grid.SetRow(b, row);
                    Grid.SetColumn(b, col);
                }
            }
        }

        private void SetUpGrid(int rows, int cols)
        {
            SetUpSizeOfGrid();
            SetUpRowColDefinitions(rows, cols);
            SetUpMouseEventListener();
        }

        private void SetUpMouseEventListener()
        {
            var mouseClickHandlerDelegate = new RoutedEventHandler(HandleMouseDown);
            this.main_grid.AddHandler(Grid.MouseDownEvent, mouseClickHandlerDelegate, true);
        }

        private void SetUpRowColDefinitions(int rows, int cols)
        {
            var rowDefinitionsList = new List<RowDefinition>();
            var colDefinitionsList = new List<ColumnDefinition>();

            //add row definitions
            for (int row = 0; row < rows; row++)
            {
                var rd = new RowDefinition();
                GridLength height = new GridLength(this.main_grid.ActualHeight / rows);
                rd.Height = height;
                rowDefinitionsList.Add(rd);
                this.main_grid.RowDefinitions.Add(rd);
            }

            //add col definitions
            for (int col = 0; col < cols; col++)
            {
                var cd = new ColumnDefinition();
                GridLength width = new GridLength(this.main_grid.ActualHeight / cols);
                cd.Width = width;
                colDefinitionsList.Add(cd);
                this.main_grid.ColumnDefinitions.Add(cd);
            }
        }

        private void SetUpSizeOfGrid()
        {
            this.main_grid.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            this.main_grid.Arrange(new Rect(0, 0, phisWidth, phisHight));
        }

        private void HandleMouseDown(Object sender, RoutedEventArgs args)
        {
            var b = (Button)args.Source;
            var name = b.Name;
            var tag = b.Tag; //use tag(or some other prop) to save state

            var rowCol = name.Split(SEPARATOR.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var row = int.Parse(rowCol[0]);
            var col = int.Parse(rowCol[1]);

            var currentPosition = new Pos(row, col);

            var res = this.engine.PlayWithPosition(currentPosition);
            //send coordinates as Position
            //recieve new state of matrix
            //update buttons acordingly

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    var currentTile = res.Matrix[r, c];
                    //var cb = (Button)this.main_grid.FindName();
                    var cb = FindVisualChildByName<Button>(this.main_grid, string.Format("{0}{1}{0}{2}", SEPARATOR, currentTile.Coordinates.Row, currentTile.Coordinates.Col));
                    if (currentTile.ObjeState == State.Closed)
                    {
                        cb.Background = CLOSED_COLOR;
                        cb.Content = CLOSED_SYMBOL;
                    }
                    else
                    {
                        cb.Background = OPEN_COLOR;
                        cb.Content = OPEN_SYMBOL;
                    }
                }
            }

            //testing
            //if (b.Content.Equals(CLOSED_SYMBOL))
            //{
            //    b.Background = OPEN_COLOR;
            //    b.Content = OPEN_SYMBOL;
            //}
            //else if (b.Content.Equals(OPEN_SYMBOL))
            //{
            //    b.Background = CLOSED_COLOR;
            //    b.Content = CLOSED_SYMBOL;
            //}
        }

        public static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(Control.NameProperty) as string;
                if (controlName == name)
                {
                    return child as T;
                }
                else
                {
                    T result = FindVisualChildByName<T>(child, name);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}
