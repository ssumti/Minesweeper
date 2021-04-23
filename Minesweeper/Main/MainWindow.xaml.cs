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

namespace Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            play.Click += (object sender, RoutedEventArgs e) => Button_Click(sender,e,Convert.ToInt32(x.Text),Convert.ToInt32(y.Text), Convert.ToInt32(bomb.Text));
        }

        private void Button_Click(object sender, RoutedEventArgs e, int x, int y, int b)
        {
            x = x < 10 ? 10 : x;
            x = x > 25 ? 25 : x;
            y = y < 10 ? 10 : y;
            y = y > 30 ? 30 : y;
            b = b > (x * y) - 1 ? (x * y) - 1 : b;
            MainGame mg = new MainGame(x * 20, y * 20, b);
            mg.Show(); 
        }
    }
}
