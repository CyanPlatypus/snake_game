using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace SnakeGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game myGame;

        SolidColorBrush mainCellColor = Brushes.White;

        Timer timer;
        bool timerIsOn = false;

        // disables the maximize button
        //*
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x10000;

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper((Window)sender).Handle;
            var value = GetWindowLong(hwnd, GWL_STYLE);
            SetWindowLong(hwnd, GWL_STYLE, (int)(value & ~WS_MAXIMIZEBOX));
        }
        //*
        // disables the maximize button

        public MainWindow()
        {
            InitializeComponent();
            myGame = new Game();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new Timer(110); //110
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            PlaceRowsAndColumns(boardGrid);
            PlaceRectangles(boardGrid);
            ColorBoard(boardGrid, myGame.Board.BoardArray);
            DrawSnakesHeadAndTail(boardGrid);
            DrawFood(boardGrid);
            ShowScore();

            Window_SourceInitialized(sender, e); // disables the maximize button
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //throw new NotImplementedException();
            Application.Current.Dispatcher.BeginInvoke(new Action
                ( () =>
                {
                    Move(boardGrid);
                }));
            
        }

        void ReloadGame() 
        {
            myGame = new Game();

            ColorBoard(boardGrid, myGame.Board.BoardArray);
            DrawSnakesHeadAndTail(boardGrid);
            DrawFood(boardGrid);
            ShowScore();
        }

        void PlaceRowsAndColumns(Grid currentGrid)
        {
            for (int i = 0; i < myGame.Board.BoardHeight; i++)
            {
                RowDefinition rDf = new RowDefinition();
                //rDf.Height = new GridLength(1, GridUnitType.Star);
                currentGrid.RowDefinitions.Add(rDf);
            }
            for (int i = 0; i < myGame.Board.BoardWidth; i++)
            {
                ColumnDefinition cDf = new ColumnDefinition();
                //cDf.Width = new GridLength(1, GridUnitType.Star);
                currentGrid.ColumnDefinitions.Add(cDf);
            }
        }

        void PlaceRectangles(Grid gridToPlaceOn)
        {
            for (int x = 0; x < myGame.Board.BoardHeight; x++)
            {
                for (int y = 0; y < myGame.Board.BoardWidth; y++)
                {
                    Rectangle rect = new Rectangle();
                    Grid.SetColumn(rect, y);
                    Grid.SetRow(rect, x);
                    gridToPlaceOn.Children.Add(rect);
                }
            }
        }

        void ColorBoard(Grid grid, Cell[,] field)
        {
            foreach (UIElement rectang in grid.Children)
            {
                if (rectang is Rectangle)
                {
                    switch (field[Grid.GetRow(rectang), Grid.GetColumn(rectang)].Type)
                    {
                        case CellType.empty:
                            {
                                ((Rectangle)rectang).Fill = mainCellColor;
                                break;
                            }
                        case CellType.solid:
                            {
                                ((Rectangle)rectang).Fill = Brushes.Black;
                                break;
                            }
                    }
                }
            }
        }

        void DrawSnakesHeadAndTail(Grid grid) 
        {
            ((Rectangle)grid.Children[myGame.Snake.HeadCoordinate.X * myGame.Board.BoardWidth + myGame.Snake.HeadCoordinate.Y]).Fill = Brushes.Coral;
            ((Rectangle)grid.Children[myGame.Snake.TailCoordinate.X * myGame.Board.BoardWidth + myGame.Snake.TailCoordinate.Y]).Fill = Brushes.Indigo;
        }

        void DrawFood(Grid grid) 
        {
            ((Rectangle)grid.Children[myGame.FoodCoordinate.X * myGame.Board.BoardWidth + myGame.FoodCoordinate.Y]).Fill = Brushes.LightSeaGreen; //new SolidColorBrush(Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255))); 
        }

        void ShowScore() 
        {
            scoreBox.Content = "Score: " + Convert.ToString(myGame.Score);
        }

        void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left) || (e.Key == Key.A)) myGame.Snake.Direction = Direction.left;
            else
            {
                if ((e.Key == Key.Right) || (e.Key == Key.D)) myGame.Snake.Direction = Direction.right;
                else
                {
                    if ((e.Key == Key.Up) || (e.Key == Key.W)) myGame.Snake.Direction = Direction.up;
                    else
                    {
                        if ((e.Key == Key.Down) || (e.Key == Key.S)) myGame.Snake.Direction = Direction.down;
                    }
                }
            }

            if (!timerIsOn)
            {
                timer.Start();
                timerIsOn = true;
            }

            //Move(boardGrid);
        }

        void Move(Grid grid)
        {
            ((Rectangle)grid.Children[myGame.Snake.HeadCoordinate.X * myGame.Board.BoardWidth + myGame.Snake.HeadCoordinate.Y]).Fill = Brushes.Indigo;
            ((Rectangle)grid.Children[myGame.Snake.TailCoordinate.X * myGame.Board.BoardWidth + myGame.Snake.TailCoordinate.Y]).Fill = mainCellColor;

            bool fed = false;
            bool safe = true;
            myGame.MoveSnake(ref fed, ref safe);

            DrawSnakesHeadAndTail(grid);

            if (!safe)
            {
                timer.Stop();
                timerIsOn = false;

                myGame.Snake.Direction = Direction.no;
                MessageBox.Show("Game over, dude.", Convert.ToString(myGame.Score));
                ReloadGame(); return;
            }
            if (fed)
            {
                DrawFood(grid);
                ShowScore();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double totalWidth = this.Width - scoreBox.Height - 30;
            double totalHeight = this.Height - scoreBox.Height - 60;
            double cellSize = (myGame.Board.BoardHeight < myGame.Board.BoardWidth) ? (totalWidth / myGame.Board.BoardWidth) : (totalHeight / myGame.Board.BoardHeight);

            if (totalHeight < myGame.Board.BoardHeight * cellSize)
            {
                cellSize = totalHeight / myGame.Board.BoardHeight;
            }

            if (totalWidth < myGame.Board.BoardWidth * cellSize) 
            {
                cellSize = totalWidth / myGame.Board.BoardWidth;
            }
            
            boardGrid.Height = myGame.Board.BoardHeight * cellSize;
            boardGrid.Width = myGame.Board.BoardWidth * cellSize;
        }
    }
}
