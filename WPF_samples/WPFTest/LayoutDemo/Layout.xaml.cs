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

namespace LayoutDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();            
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            wndGrid gd = new wndGrid();
            gd.ShowDialog();
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            wndStackPanel sp = new wndStackPanel();
            sp.ShowDialog();
        }
        private void WrapPanel_Click(object sender, RoutedEventArgs e)
        {
            wndWrapPanel wp = new wndWrapPanel();
            wp.ShowDialog();
        }
        private void DockPanel_Click(object sender, RoutedEventArgs e)
        {
            wndDockPanel wp = new wndDockPanel();
            wp.ShowDialog();
        }
        private void Virtualize_Click(object sender, RoutedEventArgs e)
        {
            wndVirtualize wp = new wndVirtualize();
            wp.ShowDialog();
        }
        private void Canvas_Click(object sender, RoutedEventArgs e)
        {
            wndCanvas wp = new wndCanvas();
            wp.ShowDialog();
        }

        private void UniformGrid_Click(object sender, RoutedEventArgs e)
        {
            wndUniformGrid ug = new wndUniformGrid();
            ug.ShowDialog();
        }
        private void ScrollViewer_Click(object sender, RoutedEventArgs e)
        {
            wndScrollViewer ug = new wndScrollViewer();
            ug.ShowDialog();
        }
        private void GroupBox_Click(object sender, RoutedEventArgs e)
        {
            wndGroupBox ug = new wndGroupBox();
            ug.ShowDialog();
        }
        private void Expander_Click(object sender, RoutedEventArgs e)
        {
            wndExpander ug = new wndExpander();
            ug.ShowDialog();
        }
        private void ViewBox_click(object sender, RoutedEventArgs e)
        {
            wndViewBox ug = new wndViewBox();
            ug.ShowDialog();
        }
        private void Popup_Click(object sender, RoutedEventArgs e)
        {
            wndPopup ug = new wndPopup();
            ug.ShowDialog();
        }
        private void InkCanvas_Click(object sender, RoutedEventArgs e)
        {
            wndInkCanvas ug = new wndInkCanvas();
            ug.ShowDialog();
        }
    }
}
