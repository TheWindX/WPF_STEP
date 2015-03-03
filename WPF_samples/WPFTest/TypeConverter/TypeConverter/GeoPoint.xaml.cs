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

namespace WPFSampleDemo.TypeConverter
{
    /// <summary>
    /// Interaction logic for GeoPoint.xaml
    /// </summary>
    public partial class GeoPoint : UserControl
    {


        public static readonly DependencyProperty GeoPointValueProperty = DependencyProperty.Register("GeoPointValue", typeof(GeoPointItem), typeof(GeoPoint), new PropertyMetadata(new GeoPointItem(0.0, 0.0)));

        public GeoPoint()
        {
            InitializeComponent();
        }
        
        public GeoPointItem GeoPointValue
        {
            get
            {
                return this.GetValue(GeoPointValueProperty) as GeoPointItem;
            }
            set
            {
                this.SetValue(GeoPointValueProperty, value);
            }
        }

        private void txtlat_TextChanged(object sender, TextChangedEventArgs e)
        {
            GeoPointItem item = this.GeoPointValue;

            item.Latitude = Convert.ToDouble(txtlat.Text);
            this.GeoPointValue = item;
        }

        private void txtlon_TextChanged(object sender, TextChangedEventArgs e)
        {
            GeoPointItem item = this.GeoPointValue;

            item.Longitude = Convert.ToDouble(txtlon.Text);
            this.GeoPointValue = item;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GeoPointItem item = this.GeoPointValue;
            this.txtlat.Text = item.Latitude.ToString();
            this.txtlon.Text = item.Longitude.ToString();

        }
    }
}
