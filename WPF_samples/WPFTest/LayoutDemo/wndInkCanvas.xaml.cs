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
    /// Interaction logic for wndInkCanvas.xaml
    /// </summary>
    public partial class wndInkCanvas : Window
    {
        public wndInkCanvas()
        {
            InitializeComponent();
        }

        private void Pen_Checked(object sender, RoutedEventArgs e)
        {
            icBox.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Erase_Checked(object sender, RoutedEventArgs e)
        {
            icBox.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void EraseByStroke_Checked(object sender, RoutedEventArgs e)
        {
            icBox.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }
    }
}
