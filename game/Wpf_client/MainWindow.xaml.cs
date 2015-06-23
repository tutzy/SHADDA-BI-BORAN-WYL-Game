using Logic.ClickPatterns;
using Logic.Engine;
using Logic.Enumerations;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_client.Config;
using Wpf_client.Helpers;

namespace Wpf_client
{
    public partial class MainWindow : Window
    {
        private Configuration configuration;
        private int rows = 3;
        private int cols = 3;
        private Engine engine;

        public MainWindow()
        {
            InitializeComponent(); //returns nothing so the grids actual size is still 0 so we need to set it up manualy
            this.Hide();
            SetGameFieldSize();
        }

        private void SetGameFieldSize()
        {
            var gff = new GameConfigForm();
            gff.OnFieldsSet += OnFieldsSetHandler;
            gff.BringToFront();
            gff.Show();
        }

        private void OnFieldsSetHandler(Object sender, EventArgs ea)
        {
            var configForm = (GameConfigForm)sender;
            this.rows = configForm.Rows;
            this.cols = configForm.Cols;

            this.Show();
            SetUpGameEngine();
            SetUpGridAndWindow();
        }

        private void SetUpGridAndWindow()
        {
            this.configuration = Configuration.Instance;
            this.configuration.SetContext(this);
            this.configuration.SetUpGrid(rows, cols, HandleMouseDown);
            this.configuration.FillGridWithButtons();
        }

        private void SetUpGameEngine()
        {
            this.engine = Engine.Instance;
            this.engine.ConfigureStrategy(null);
            this.engine.ConfigureGameFieldSize(rows, cols);
            this.engine.ConfigureStrategy(new DefaultStrategy());
        }

        private void HandleMouseDown(Object sender, RoutedEventArgs args)
        {
            //get clicked button
            var b = (Button)args.Source;
            var name = b.Name;

            var currentPosition = GetClickedPosition(name);

            //ask engine what happens after clicked position
            var updatedMatrix = this.engine.PlayWithPosition(currentPosition);

            UpdateFieldAfterClick(updatedMatrix);

            HandleGameWon(updatedMatrix);
        }

        private void HandleGameWon(IField updatedMatrix)
        {
            if (updatedMatrix.MatrixIsFull)
            {
                CreatePopUpWithText("You won");
                UpdateFieldAfterClick(this.engine.Field);
            }
        }

        private void CreatePopUpWithText(string text)
        {
            var playAgainin = "do you want to play again?";
            MessageBoxResult result = MessageBox.Show(playAgainin, text, MessageBoxButton.YesNo, MessageBoxImage.Question);
            var hasClickedYes = result.HasFlag(MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                this.engine.ResetGame();
            }
            else
            {
                this.Close();
            }
        }

        private void UpdateFieldAfterClick(Logic.Interfaces.IField res)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    var currentTile = res.Matrix[r, c];
                    var cb = FindVisualChildByName<Button>(this.main_grid, string.Format("{0}{1}{0}{2}", Configuration.SEPARATOR, currentTile.Coordinates.Row, currentTile.Coordinates.Col));
                    if (currentTile.ObjeState == State.Closed)
                    {
                        cb.Background = Configuration.CLOSED_COLOR;
                        cb.Content = Configuration.CLOSED_SYMBOL;
                    }
                    else
                    {
                        cb.Background = Configuration.OPEN_COLOR;
                        cb.Content = Configuration.OPEN_SYMBOL;
                    }
                }
            }
        }

        private static Pos GetClickedPosition(string name)
        {
            var rowCol = name.Split(Configuration.SEPARATOR.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var row = int.Parse(rowCol[0]);
            var col = int.Parse(rowCol[1]);
            var currentPosition = new Pos(row, col);
            return currentPosition;
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
