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
using System.Windows.Shapes;

namespace LayoutDemo
{
    /// <summary>
    /// Interaction logic for wndWrapPanel.xaml
    /// </summary>
    public partial class wndWrapPanel : Window
    {
        public wndWrapPanel()
        {
            InitializeComponent();
        }
        private void Horizontal_Checked(object sender, RoutedEventArgs e)
        {
            this.wpMain.Orientation = Orientation.Horizontal;
        }

        private void Vertical_Checked(object sender, RoutedEventArgs e)
        {
            this.wpMain.Orientation = Orientation.Vertical;
        }
    }
}
