using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// Interaction logic for MainGame.xaml
    /// </summary>
    public partial class MainGame : Window
    {
        private int[,] a;
        private IButton[,] buts;
        private Queues q;
        private UniformGrid grid;
        private int x;
        private int y;
        private bool[,] check;
        private int remainbomb = 0;
        static int bomb;
        static int[] posX = { -1, 0, 1, -1, 1, -1, 0, 1 };
        static int[] posY = { -1, -1, -1, 0, 0, 1, 1, 1 };
        public MainGame(int x, int y, int b)
        {
            InitializeComponent();
            button.Click += (object sender, RoutedEventArgs e) => Button_Click_1(sender, e, x, y, b, layout);
            a = new int[x + 2, y + 2];
            buts = new IButton[x + 2, y + 2];
            q = new Queues(x * y);
            check = new bool[x + 2, y + 2];
            bomb = b;
            this.x = x / 20;
            this.y = y / 20;
            win.Height = x + (23 * 3);
            win.Width = y;
            BeginGameProcess(x, y, b);
            //test(4,3);
            //test(6, 3);
            //test(2, 7);
            //test(6, 7);
            //test(7, 7);
        }

        private void BeginGameProcess(int x, int y, int b)
        {
            Ui(layout, x / 20, y / 20);
            Reset(x / 20, y / 20);
            RandomBomb(x / 20, y / 20, b);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e, int x, int y, int b, StackPanel layout)
        {
            DeleteGame(layout);
            BeginGameProcess(x, y, b);
        }

        private void DeleteGame(StackPanel layout)
        {
            layout.Children.RemoveAt(1);
        }

        private void test(int bx, int by)
        {
            a[bx, by] = -1;
            for (int i =0; i<=7; ++i)
            {
                a[bx + posX[i], by + posY[i]]++;
            }
        }

        private void RandomBomb(int x, int y, int b)
        {
            Random random = new Random();
            int i = 1;
            while (i<=b)
            {
                int h = random.Next(1, x);
                int w = random.Next(1, y);
                if (a[h,w]!=-1)
                {
                    a[h, w] = -1;
                    ++i;
                    if (a[h - 1, w - 1] != -1) 
                    {
                        ++a[h - 1, w - 1];
                    }
                    if (a[h - 1, w] != -1) 
                    {
                        ++a[h - 1, w];
                    }
                    if (a[h - 1, w + 1] != -1)
                        ++a[h - 1, w + 1];
                    if (a[h, w - 1] != -1)
                        ++a[h, w - 1];
                    if (a[h, w + 1] != -1)
                        ++a[h, w + 1];
                    if (a[h + 1, w - 1] != -1)
                        ++a[h + 1, w - 1];
                    if (a[h + 1, w] != -1)
                        ++a[h + 1, w];
                    if (a[h + 1, w + 1] != -1)
                        ++a[h + 1, w + 1];
                }
            }
        }

        private void Reset(int x, int y)
        {
            for (int i = 1; i <= x; ++i)
                for (int j = 1; j <= y; ++j)
                {
                    a[i, j] = 0;
                    check[i, j] = true;
                }
        }
        private void Ui(StackPanel layout, int x, int y)
        {
            grid = new UniformGrid()
            {
                Columns = y,
                Rows = x,
            };
            for (int i = 1; i<=x; ++i)
                for (int j = 1; j<=y; ++j)
                {
                    IButton b = new IButton()
                    {
                        Width = 20,
                        Height = 20,

                        y = j,
                        x = i,
                    };
                    Grid.SetColumn(b, j);
                    Grid.SetRow(b, i);
                    b.Click += (object sender, RoutedEventArgs e) => B_Click(sender, e, b);
                    b.MouseRightButtonDown += (object sender, MouseButtonEventArgs e) => B_MouseRightButtonDown(sender, e, b);
                    buts[i, j] = b;
                    grid.Children.Add(b);
                }
            layout.Children.Add(grid);

        }

        private void B_MouseRightButtonDown(object sender, MouseButtonEventArgs e, IButton button)
        {
            if (button.IsRightClicked == false)
            {
                remainbomb += 1;
                button.Background = Brushes.Red;
                check[button.x, button.y] = false;
                button.IsRightClicked = true;
                if (Condition())
                {
                    GameOver("win");
                };
            }
            else
            {
                --remainbomb;
                // Button's Default Color --------------------//
                GradientStop g = new GradientStop();
                Color color = (Color)ColorConverter.ConvertFromString("#FFF3F3F3");
                g.Color = color;                
                g.Offset = 0;
                LinearGradientBrush l = new LinearGradientBrush();
                l.GradientStops.Add(g);
                l.GradientStops.Add(new GradientStop()
                {
                    Offset = 0.5,
                    Color = (Color)ColorConverter.ConvertFromString("#FFEBEBEB"),
                });
                l.GradientStops.Add(new GradientStop()
                {
                    Offset = 1,
                    Color = (Color)ColorConverter.ConvertFromString("#FFDDDDDD"),
                });
                button.Background = l;
                //------------------------------------//
                check[button.x, button.y] = true;
                button.IsRightClicked = false;
            }
        }

        private void B_Click(object sender, RoutedEventArgs e, IButton button)
        {
            if (a[button.x, button.y] == -1)
            {
                TurnOnAllBomb();
                GameOver("lose");
            }
            else if (a[button.x, button.y] == 0) 
            {
                var t = new Pos(button.x, button.y);
                q.Enqueue(t);
                buts[t.GetX(), t.GetY()].IsEnabled = false;
                while (!q.IsEmpty())
                {
                    var temp = q.Dequeue();
                    for (int i = 0; i<=7; ++i)
                    {
                        if (check[temp.GetX() + posX[i], temp.GetY() + posY[i]] == true)
                            if (a[temp.GetX()+posX[i],temp.GetY()+posY[i]] == 0)
                            {
                                check[temp.GetX() + posX[i], temp.GetY() + posY[i]] = false;
                                buts[temp.GetX() + posX[i], temp.GetY() + posY[i]].IsEnabled = false;
                                var xtemp = new Pos(temp.GetX() + posX[i], temp.GetY() + posY[i]);
                                q.Enqueue(xtemp);
                            }
                            else if (a[temp.GetX() + posX[i], temp.GetY() + posY[i]] != 0 && (a[temp.GetX() + posX[i], temp.GetY() + posY[i]] != -1))
                            {
                                buts[temp.GetX() + posX[i], temp.GetY() + posY[i]].Content = a[temp.GetX() + posX[i], temp.GetY() + posY[i]];
                                check[temp.GetX() + posX[i], temp.GetY() + posY[i]] = false;
                                buts[temp.GetX() + posX[i], temp.GetY() + posY[i]].Click += Button_Click; //disable
                                buts[temp.GetX() + posX[i], temp.GetY() + posY[i]].MouseRightButtonDown += MouseRightButtonDown;
                            }
                    }
                }
            }
            else
            {
                button.Content = a[button.x, button.y];
                check[button.x, button.y] = false;
                button.Click += Button_Click;
            }

            if (Condition())
            {
                GameOver("win");
            };

            


        }
        private bool Condition()
        {
            if (remainbomb == bomb)
            {
                for (int i = 1; i <= x; ++i)
                    for (int j = 1; j <= y; ++j)
                        if (check[i, j] == true)
                            return false;
                return true; 
            }
            else
            {
                return false;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Set Left click to nothing
        }

        private void GameOver(string content)
        {
            OutGame og = new OutGame(content);
            og.Show();
        }

        private void MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Set Right Click to nothing
        }

        private void TurnOnAllBomb()
        {
            for (int i = 1; i <= x; i++)
                for (int j = 1; j <= y; ++j)
                    if (a[i, j] != -1)
                        buts[i, j].IsEnabled = false;
                    else 
                        buts[i, j].Background = Brushes.Black;
        }

        
    }
}
