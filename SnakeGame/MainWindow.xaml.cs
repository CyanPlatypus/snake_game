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

namespace SnakeGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game myGame;

        SolidColorBrush mainCellColor = Brushes.White;

        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myGame = new Game();

            PlaceRowsAndColumns(boardGrid);
            PlaceRectangles(boardGrid);
            ColorBoard(boardGrid, myGame.Board.BoardArray);
            DrawSnakesHeadAndTail(boardGrid);
            DrawFood(boardGrid);
            ShowScore();
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
                rDf.Height = new GridLength(1, GridUnitType.Star);
                currentGrid.RowDefinitions.Add(rDf);
            }
            for (int i = 0; i < myGame.Board.BoardWidht; i++)
            {
                ColumnDefinition cDf = new ColumnDefinition();
                cDf.Width = new GridLength(1, GridUnitType.Star);
                currentGrid.ColumnDefinitions.Add(cDf);
            }
        }

        void PlaceRectangles(Grid gridToPlaceOn)
        {
            for (int x = 0; x < myGame.Board.BoardHeight; x++)
            {
                for (int y = 0; y < myGame.Board.BoardWidht; y++)
                {
                    Rectangle rect = new Rectangle();
                    //this.SizeToContent = SizeToContent.WidthAndHeight; 

                    //rect.Height = 10;
                    //rect.Width = 10;
                    //rect.Stretch = Stretch.Fill;
                    //rect.Fill = Brushes.Black;
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
                                //((Button)btn).Content = "";
                                ((Rectangle)rectang).Fill = mainCellColor;
                                break;
                            }
                        case CellType.solid:
                            {
                                ((Rectangle)rectang).Fill = Brushes.Black;
                                //((Button)btn).Content = new Ellipse() { Height = 5, Width = 5, Fill = Brushes.Black };
                                break;
                            }
                    }
                }
            }
        }

        void DrawSnakesHeadAndTail(Grid grid) 
        {
            //grid[myGame.Snake.HeadCoordinate.X,myGame.Snake.HeadCoordinate.Y]
            ((Rectangle)grid.Children[myGame.Snake.HeadCoordinate.X * myGame.Board.BoardWidht + myGame.Snake.HeadCoordinate.Y]).Fill = Brushes.Coral;
            ((Rectangle)grid.Children[myGame.Snake.TailCoordinate.X * myGame.Board.BoardWidht + myGame.Snake.TailCoordinate.Y]).Fill = Brushes.Indigo;
        }

        void DrawFood(Grid grid) 
        {
            ((Rectangle)grid.Children[myGame.FoodCoordinate.X * myGame.Board.BoardWidht + myGame.FoodCoordinate.Y]).Fill = Brushes.LightSeaGreen; //new SolidColorBrush(Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255))); 
        }

        void ShowScore() 
        {
            scoreBox.Text = "Score:" + Environment.NewLine + Convert.ToString(myGame.Score);
        }

        void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left) || (e.Key == Key.A)) myGame.Snake.Direction = Direction.left;
            if ((e.Key == Key.Right) || (e.Key == Key.D)) myGame.Snake.Direction = Direction.right;
            if ((e.Key == Key.Up) || (e.Key == Key.W)) myGame.Snake.Direction = Direction.up;
            if ((e.Key == Key.Down) || (e.Key == Key.S)) myGame.Snake.Direction = Direction.down;

            Move(boardGrid);
        }

        void Move(Grid grid)
        {
            ((Rectangle)grid.Children[myGame.Snake.HeadCoordinate.X * myGame.Board.BoardWidht + myGame.Snake.HeadCoordinate.Y]).Fill = Brushes.Indigo;
            ((Rectangle)grid.Children[myGame.Snake.TailCoordinate.X * myGame.Board.BoardWidht + myGame.Snake.TailCoordinate.Y]).Fill = mainCellColor;

            bool fed = false;
            bool safe = true;
            myGame.MoveSnake(ref fed, ref safe);

            DrawSnakesHeadAndTail(grid);

            if (!safe)
            {
                MessageBox.Show("Game over, dude.", Convert.ToString(myGame.Score)); ReloadGame(); return;
            }
            if (fed)
            {
                DrawFood(grid);
                ShowScore();
            }
        }
    }
}
