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
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// Interaction logic for OutGame.xaml
    /// </summary>
    public partial class OutGame : Window
    {
        public OutGame(string content)
        {
            InitializeComponent();
            if (content == "win")
            {
                TextBlock text = new TextBlock()
                {
                    Text = "You Won!",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                layout.Children.Add(text);
            }
            else
            {
                TextBlock text = new TextBlock()
                {
                    Text = "You lose",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                layout.Children.Add(text);
            }
            
        }
    }
}
