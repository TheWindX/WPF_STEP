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
    /// Interaction logic for Grid.xaml
    /// </summary>
    public partial class wndGrid : Window
    {
        public wndGrid()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int row = Convert.ToInt32(this.txtRow.Text);
                int col = Convert.ToInt32(this.txtCol.Text);

                if (row > 2 || col > 2)
                    MessageBox.Show("Row or Column value should be less or equal to 2 for 3x3 Grid.");
                Grid.SetRow(this.brdElement, row);
                Grid.SetColumn(this.brdElement, col);
                tbElement.Text = string.Format("Row {0}, Column {1}", row, col);
            }
            catch { }
        }
    }
}
