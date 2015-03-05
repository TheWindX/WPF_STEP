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
    /// Interaction logic for Header.xaml
    /// </summary>

    public enum SortType : byte
    {
        Ascending,
        Descending,
        NotOrdered
    }

    public delegate SortType HeaderClickedDelegate(Header header, SortType SortType);

    public partial class Header : UserControl
    {
        private HeaderClickedDelegate HeadClickDel;
            
        private SortType CurrentSortType = SortType.NotOrdered;
 
        public Header(string title, HeaderClickedDelegate HeadClickDel)
        {
            InitializeComponent();
            Title.Content = title;
            this.HeadClickDel = HeadClickDel;
        }

        private void UpdateSortArrow()
        {
            switch (CurrentSortType)
            {
                case SortType.NotOrdered:
                    Arrow.Style = (Style)TryFindResource("NoArrowStyle");
                    break;
                case SortType.Ascending:
                    Arrow.Style = (Style)TryFindResource("DGP_Head_UpArrowStyle");
                    break;
                case SortType.Descending:
                    Arrow.Style = (Style)TryFindResource("DGP_Head_DownArrowStyle");
                    break;
            }
        }

        private void UserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentSortType = HeadClickDel(this, CurrentSortType);
            UpdateSortArrow();
        }

        public void ResetSortType()
        {
            if (CurrentSortType != SortType.NotOrdered)
            {
                CurrentSortType = SortType.NotOrdered;
                UpdateSortArrow();
            }
        }

        private void UserControl_GotMouseCapture(object sender, MouseEventArgs e)
        {
            HeaderBorder.CaptureMouse();
        }
    }
}
