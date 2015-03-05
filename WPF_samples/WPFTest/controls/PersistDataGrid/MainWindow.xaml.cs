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

namespace PersistDataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyPerDataGrid.AddColumn("Genus", ColumnType.TextBox, 110.0);
            MyPerDataGrid.AddColumn("Specie", ColumnType.TextBox, 100.0);
            MyPerDataGrid.AddColumn("Family", ColumnType.ComboBox, 110.0);
            MyPerDataGrid.AddColumn("Acquired", ColumnType.DatePicker, 100.0);
            MyPerDataGrid.AddColumn("Damaged?", ColumnType.CheckBox, 70.0);
            MyPerDataGrid.AddColumn("Age", ColumnType.TextBox, 50.0);

            MyPerDataGrid.AddRow(new object[] { "Mammilaria", "Plumosa", "Cactaceae", new DateTime(1999, 9, 22), false, "20"});
            MyPerDataGrid.AddRow(new object[] { "Mammilaria", "Tetragona", "Cactaceae", new DateTime(2005, 8, 15), false, "12" });
            MyPerDataGrid.AddRow(new object[] { "Notocactus", "Leninghausii", "Cactaceae", new DateTime(1987, 7, 28), true, "30" });
            MyPerDataGrid.AddRow(new object[] { "Ferocactus", "Pilosii", "Cactaceae", new DateTime(1989, 8, 9), false, "27" });
            MyPerDataGrid.AddRow(new object[] { "Echinofossulocactus", "Multicostatus", "Cactaceae", new DateTime(1990, 3, 21), false});
            MyPerDataGrid.AddRow(new object[] { "Gymnocalycium", "Mihanovichii", "Cactaceae", new DateTime(2006, 8, 20), false});
            MyPerDataGrid.AddRow(new object[] { "Euphorbia", "Obesa", "Euphorbiaceae", null, false });
            MyPerDataGrid.AddRow(new object[] { "Pachypodium", "Multiflorum", "Apocynaceae", new DateTime(2003, 8, 25), false});
            MyPerDataGrid.AddRow(new object[] { "Ipomoea", "Platensis", "Convolvulaceae", new DateTime(1997, 4, 19), false});
            MyPerDataGrid.AddRow(new object[] { "Lithop", null, "Aizoaceae", new DateTime(2010, 12, 28), false});

            for (int i = 0; i < MyPerDataGrid.GetRowCount(2); i++)
            {
                ComboBox FamCombo = (ComboBox)MyPerDataGrid.GetControl(i, 2);
                
                if (i != 8)
                {
                    FamCombo.Items.Add("Cactaceae");
                    FamCombo.Items.Add("Euphorbiaceae");
                    FamCombo.Items.Add("Apocynaceae");
                    FamCombo.Items.Add("Aizoaceae");
                }
                else // This is for testing whether items in combo correctly change when sorted
                {
                    FamCombo.Items.Add("Convolvulaceae");
                    FamCombo.Items.Add("Liliaceae");
                }
            }
            //int rowcount = MyPerDataGrid.GetRowCount();
            //MyPerDataGrid.AddRow();
            //object[] rowdets;
            //MyPerDataGrid.GetRowDetails(2, out rowdets);
            //PerDataGrid.SetRowDetails(rowcount, rowdets); // Drop down list not populated, but that is expected!      
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            MyPerDataGrid.AddRow();
        }

        private void DisplayRow_Click(object sender, RoutedEventArgs e)
        {
            object[] RowDetails;
            int iRow;
            int.TryParse(Row.Text, out iRow);
            MyPerDataGrid.GetRowDetails(iRow, out RowDetails);

            if (RowDetails != null)
            {
                DisplayedRow.Text = "";
                foreach (object obj in RowDetails)
                {
                    if (obj != null)
                        DisplayedRow.Text += obj.ToString() + ' ';
                }
            }
        }
    }
}
