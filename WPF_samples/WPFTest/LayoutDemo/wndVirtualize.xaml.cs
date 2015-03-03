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
using System.Collections.ObjectModel;

namespace LayoutDemo
{
    /// <summary>
    /// Interaction logic for wndVirtualize.xaml
    /// </summary>
    public partial class wndVirtualize : Window
    {
        public wndVirtualize()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<int> obs = new ObservableCollection<int>();
            Random rnd = new Random(1000);
            for (int i = 0; i < 100000; i++)
                obs.Add(rnd.Next());
            this.lstElements.DataContext = obs;
        }
    }
}
