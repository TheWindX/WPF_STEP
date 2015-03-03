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
    /// Interaction logic for wndStackPanel.xaml
    /// </summary>
    public partial class wndStackPanel : Window
    {
        public wndStackPanel()
        {
            InitializeComponent();
        }

        private void Horizontal_Checked(object sender, RoutedEventArgs e)
        {
            this.spMain.Orientation = Orientation.Horizontal;
        }

        private void Vertical_Checked(object sender, RoutedEventArgs e)
        {
            this.spMain.Orientation = Orientation.Vertical;
        }
    }
}
