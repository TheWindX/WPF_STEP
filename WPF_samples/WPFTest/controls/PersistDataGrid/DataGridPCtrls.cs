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
using System.Windows.Controls.Primitives;

namespace PersistDataGrid
{
    public class DataGridPCtrls : Control
    {
        private static readonly DependencyProperty GridLinesColorProperty =
                DependencyProperty.Register("GridLinesColor", typeof(Brush), typeof(DataGridPCtrls), new PropertyMetadata(Brushes.Black));

        private static readonly DependencyProperty GridBackColorProperty =
                DependencyProperty.Register("GridBackColor", typeof(Brush), typeof(DataGridPCtrls));

        private static readonly DependencyProperty CanUserResizeColumnsProperty =
                DependencyProperty.Register("CanUserResizeColumns", typeof(bool), typeof(DataGridPCtrls));

        private static readonly DependencyProperty CanUserSortColumnsProperty =
                DependencyProperty.Register("CanUserSortColumns", typeof(bool), typeof(DataGridPCtrls));
        
        static DataGridPCtrls()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataGridPCtrls), new FrameworkPropertyMetadata(typeof(DataGridPCtrls)));

            try
            {
                FrameworkPropertyMetadata GridLinesColorMetaData = new FrameworkPropertyMetadata();
                GridLinesColorMetaData.CoerceValueCallback = new CoerceValueCallback(CoerceGridLinesColor);
                GridLinesColorProperty.OverrideMetadata(typeof(DataGridPCtrls), GridLinesColorMetaData);

                FrameworkPropertyMetadata GridBackColorMetaData = new FrameworkPropertyMetadata();
                GridBackColorMetaData.CoerceValueCallback = new CoerceValueCallback(CoerceGridBackColor);
                GridBackColorProperty.OverrideMetadata(typeof(DataGridPCtrls), GridBackColorMetaData);

                FrameworkPropertyMetadata CanUserResizeColumnsMetaData = new FrameworkPropertyMetadata();
                CanUserResizeColumnsMetaData.CoerceValueCallback = new CoerceValueCallback(CoerceCanUserResizeColumns);
                CanUserResizeColumnsProperty.OverrideMetadata(typeof(DataGridPCtrls), CanUserResizeColumnsMetaData);

                FrameworkPropertyMetadata CanUserSortColumnsMetaData = new FrameworkPropertyMetadata();
                CanUserSortColumnsMetaData.CoerceValueCallback = new CoerceValueCallback(CoerceCanUserSortColumns);
                CanUserSortColumnsProperty.OverrideMetadata(typeof(DataGridPCtrls), CanUserSortColumnsMetaData);

                FrameworkPropertyMetadata BorderThicknessMetaData = new FrameworkPropertyMetadata();
                BorderThicknessMetaData.CoerceValueCallback = new CoerceValueCallback(CoerceBorderThickness);
                UserControl.BorderThicknessProperty.OverrideMetadata(typeof(DataGridPCtrls), BorderThicknessMetaData);
            }
            catch (Exception)
            {
            }
        
        }
        private static object CoerceGridLinesColor(DependencyObject d, object value)
        {
            DataGridPCtrls DatagridPer = d as DataGridPCtrls;
            
            if (DatagridPer != null && value is Brush)
            {
                StackPanel CtrlStack = (StackPanel)DatagridPer.GetTemplateChild("CtrlStack");

                if (CtrlStack != null)
                {
                    for (int IdxCol = 0; IdxCol < CtrlStack.Children.Count; IdxCol++)
                    {
                        Column col = CtrlStack.Children[IdxCol] as Column;

                        if (col != null)
                            col.GridLinesColor = (Brush)value;
                    }
                }
            }
            return value;
        }
        private static object CoerceGridBackColor(DependencyObject d, object value)
        {
            DataGridPCtrls DatagridPer = d as DataGridPCtrls;

            if (DatagridPer != null && value is Brush)
            {
                StackPanel CtrlStack = (StackPanel)DatagridPer.GetTemplateChild("CtrlStack");

                if (CtrlStack != null)
                {
                    for (int Idx = 0; Idx < CtrlStack.Children.Count; Idx++)
                    {
                        Column col = CtrlStack.Children[Idx] as Column;

                        if (col != null)
                            col.GridBackColor = (Brush)value;
                    }
                }
            }
            return value;
        }
        private static object CoerceCanUserResizeColumns(DependencyObject d, object value)
        {
            DataGridPCtrls DatagridPer = d as DataGridPCtrls;

            if (DatagridPer != null && value is bool)
            {
                SplitterGrid Headers = (SplitterGrid)DatagridPer.GetTemplateChild("Headers");

                if (Headers != null)
                    Headers.CanUserResizeColumns = (bool)value;
            }
            return value;
        }
        private static object CoerceCanUserSortColumns(DependencyObject d, object value)
        {
            DataGridPCtrls DatagridPer = d as DataGridPCtrls;

            if (DatagridPer != null && value is bool)
            {
                SplitterGrid Headers = (SplitterGrid)DatagridPer.GetTemplateChild("Headers");

                if (Headers != null)
                    Headers.CanUserSortColumns = (bool)value;
            }
            return value;
        }
        private static object CoerceBorderThickness(DependencyObject d, object value)
        {
            DataGridPCtrls DatagridPer = d as DataGridPCtrls;

            if (DatagridPer != null && value is Thickness)
                DatagridPer.BorderThickness = (Thickness)value;

            return new Thickness(0.0);
        }

        private bool HeadersInitialized = false;
        private bool HorizScrolling = false;
        private new bool IsLoaded = false;

        private struct SortStruct
        {
            public List<UIElement> RowList;
            public object SortingObj;
        }

        private struct ColumnInfo
        {
            public string strHeader;
            public ColumnType ct;
            public double width;
        }

        private Queue<ColumnInfo> ColumnInfoQueue = new Queue<ColumnInfo>();
        private Queue<object[]> RowDetailsQueue = new Queue<object[]>();

        public Brush GridLinesColor
        {
            get { return (Brush)GetValue(GridLinesColorProperty); }
            set { SetValue(GridLinesColorProperty, value); }
        }

        public Brush GridBackColor
        {
            get { return (Brush)GetValue(GridBackColorProperty); }
            set { SetValue(GridBackColorProperty, value); }
        }

        public bool CanUserResizeColumns
        {
            get { return (bool)GetValue(CanUserResizeColumnsProperty); }
            set { SetValue(CanUserResizeColumnsProperty, value); }
        }

        public bool CanUserSortColumns
        {
            get { return (bool)GetValue(CanUserSortColumnsProperty); }
            set { SetValue(CanUserSortColumnsProperty, value); }
        }
        public DataGridPCtrls()
        {
            this.Loaded += new RoutedEventHandler(DataGridPCtrls_Loaded);
            this.Unloaded += new RoutedEventHandler(DataGridPCtrls_Unloaded);
            this.MouseMove += new MouseEventHandler(DataGridPCtrls_MouseMove);
        }

        void DataGridPCtrls_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
            {
                IsLoaded = true;

                SplitterGrid Headers = (SplitterGrid)this.GetTemplateChild("Headers");

                if (Headers != null)
                    Headers.WidthChanged += new WidthChangedEventHandler(Headers_WidthChanged);

                ScrollViewer HeaderScroll = (ScrollViewer)this.GetTemplateChild("HeaderScroll");

                if (HeaderScroll != null)
                    HeaderScroll.AddHandler(RangeBase.ValueChangedEvent, new RoutedPropertyChangedEventHandler<double>(HeaderScroll_ValueChanged));

                ScrollViewer CtrlScroll = (ScrollViewer)this.GetTemplateChild("CtrlScroll");

                if (CtrlScroll != null)
                {
                    ScrollBar PART_VerticalScrollBar = (ScrollBar)CtrlScroll.Template.FindName("PART_VerticalScrollBar", CtrlScroll);

                    if (PART_VerticalScrollBar != null)
                        PART_VerticalScrollBar.IsEnabledChanged += new DependencyPropertyChangedEventHandler(PART_VerticalScrollBar_IsEnabledChanged);

                    ScrollBar PART_HorizontalScrollBar = (ScrollBar)CtrlScroll.Template.FindName("PART_HorizontalScrollBar", CtrlScroll);

                    if (PART_HorizontalScrollBar != null)
                        PART_HorizontalScrollBar.AddHandler(RangeBase.ValueChangedEvent, new RoutedPropertyChangedEventHandler<double>(PART_HorizontalScrollBar_ValueChanged));
                }

                while (ColumnInfoQueue.Count != 0)
                {
                    ColumnInfo CI = ColumnInfoQueue.Dequeue();
                    AddColumn(CI.strHeader, CI.ct, CI.width);
                }
                while (RowDetailsQueue.Count != 0)
                {
                    object[] RowDets = RowDetailsQueue.Dequeue();
                    if (RowDets != null)
                        AddRow(RowDets);
                    else
                        AddRow();
                }
            }
        }
        void DataGridPCtrls_Unloaded(object sender, RoutedEventArgs e)
        {
        /*    if (IsLoaded)
            {
                StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");

                if (CtrlStack != null)
                {
                    for (int row = 0; row < ((Column)CtrlStack.Children[0]).RowCount(); row++)
                    {
                        object[] RowDetails;
                        GetRowDetails(row, out RowDetails);
                        RowDetailsQueue.Enqueue(RowDetails);
                    }

                }
                SplitterGrid Headers = (SplitterGrid)this.GetTemplateChild("Headers");

                if (Headers != null)
                {
                    for (int column = 0; column < Headers.Count; column++)
                    {
                        Column col = (Column)Headers[column];
                     //   col.Width;
                        int yu = 0;
                    }
                }
                IsLoaded = false;
            }*/
        }
        private void PART_HorizontalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!HorizScrolling)
            {
                HorizScrolling = true;
                ScrollViewer HeaderScroll = (ScrollViewer)this.GetTemplateChild("HeaderScroll");
                
                if (HeaderScroll != null)
                    HeaderScroll.ScrollToHorizontalOffset(e.NewValue);

                HorizScrolling = false;
            }
        }
        private void HeaderScroll_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // To see why this is necessary: comment this, and try SHIFT+Tab repeatedly until tab into header,
            // which may result in it scrolling independently of CtrlScroll.
            // This code insures CtrlScroll is always mapped to header.
            // HorizScrolling variable is for insuring functions don't repeatedly call each other. 
            if (!HorizScrolling)
            {
                HorizScrolling = true;

                ScrollViewer CtrlScroll = (ScrollViewer)this.GetTemplateChild("CtrlScroll");
                
                if (CtrlScroll != null)
                    CtrlScroll.ScrollToHorizontalOffset(e.NewValue);

                HorizScrolling = false;
            }
        }
        private void PART_VerticalScrollBar_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            System.Windows.Controls.Primitives.ScrollBar sb = sender as System.Windows.Controls.Primitives.ScrollBar;
            ScrollViewer HeaderScroll = (ScrollViewer)this.GetTemplateChild("HeaderScroll");

            if (sb != null && HeaderScroll != null)
            {
                if (sb.IsVisible)
                    HeaderScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                else
                    HeaderScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }
        public void AddColumn(string strHeader, ColumnType ct = ColumnType.TextBox, double width = 145.0)
        {
            if (!IsLoaded)
            {
                ColumnInfo CI;
                CI.strHeader = strHeader;
                CI.ct = ct;
                CI.width = width;
                ColumnInfoQueue.Enqueue(CI);
                return;
            }
            Header Head = new Header(strHeader, OnHeaderClicked);

            SplitterGrid Headers = (SplitterGrid)this.GetTemplateChild("Headers");
            StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");

            object obj = this.FindName("Headers");

            if (Headers == null || CtrlStack == null)
                return;

            Headers.AddColumn(Head, width - Headers.SplitWidth);

            if (!HeadersInitialized)
            {
                object NormalBrush = TryFindResource("DGP_Head_NormalBorderBrush");

                if (NormalBrush != null)
                    Headers.SplitterBrush = (Brush)NormalBrush;

                // Alternative notation:
                // Header.SplitterBrush = (Brush)this.Resources["DGP_Head_NormalBorderBrush"];

                object SelectBrush = TryFindResource("DGP_Head_HighlightBorderBrush");

                if (SelectBrush != null)
                    Headers.HighlightSplitterBrush = (Brush)SelectBrush;

                HeadersInitialized = true;
            }
            Column col = new Column(this, OnKeyDown, ct, width);
            col.GridLinesColor = GridLinesColor;
            col.GridBackColor = GridBackColor;

            if (CtrlStack.Children.Count != 0)
            {
                int Rows = ((Column)CtrlStack.Children[0]).RowCount();

                 while (Rows-- != 0)
                    col.AddRow();
            }
            CtrlStack.Children.Add(col);
        }
        public void AddRow()
        {
            if (!IsLoaded)
            {
                RowDetailsQueue.Enqueue(null);
                return;
            }
            StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");

            if (CtrlStack == null)
                return;

            foreach (Column col in CtrlStack.Children)
                col.AddRow();
        }
        public void AddRow(object[] RowDetails)
        {
            if (!IsLoaded)
            {
                RowDetailsQueue.Enqueue(RowDetails);
                return;
            }
            StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");

            if (CtrlStack == null)
                return;

            for (int Idx = 0; Idx < CtrlStack.Children.Count; Idx++)
            {
                if (RowDetails != null && Idx < RowDetails.Count())
                    ((Column)CtrlStack.Children[Idx]).AddRow(RowDetails[Idx]);
                else
                    ((Column)CtrlStack.Children[Idx]).AddRow();
            }
        }
        public void GetRowDetails(int row, out object[] RowDetails)
        {
            StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");

            if (CtrlStack != null && row >= 0 && CtrlStack.Children.Count >= 1 && row < ((Column)CtrlStack.Children[0]).RowCount())
            {
                RowDetails = new object[CtrlStack.Children.Count];

                for (int Idx = 0; Idx < CtrlStack.Children.Count; Idx++)
                    RowDetails[Idx] = ((Column)CtrlStack.Children[Idx]).GetRowValue(row);
            }
            else
                RowDetails = null;
        }
        public void SetRowDetails(int row, object[] RowDetails)
        {
            StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");

            if (CtrlStack == null)
                return;

            if (row >= 0 && CtrlStack.Children.Count >= 1 && row < ((Column)CtrlStack.Children[0]).RowCount())
            {
                for (int Idx = 0; Idx < CtrlStack.Children.Count; Idx++)
                    ((Column)CtrlStack.Children[Idx]).SetRowValue(row, RowDetails[Idx]);
            }
        }
        public int GetRowCount(int iCol = 0)
        {
            StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");

            if (CtrlStack != null && iCol >= 0 && iCol < CtrlStack.Children.Count)
                return ((Column)CtrlStack.Children[iCol]).RowCount();

            return 0;
        }
        public Control GetControl(int iRow, int iCol)
        {
            Control Ctrl = null;
            StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");

            if (CtrlStack != null && iCol >= 0 && iCol < CtrlStack.Children.Count && iRow >= 0 && iRow < ((Column)CtrlStack.Children[iCol]).RowCount())
                Ctrl = ((Column)CtrlStack.Children[iCol]).GetRowControl(iRow);

            return Ctrl;
        }
        private void DataGridPCtrls_MouseMove(object sender, MouseEventArgs e)
        {
            ScrollViewer HeaderScroll = (ScrollViewer)this.GetTemplateChild("HeaderScroll");
            ScrollViewer CtrlScroll = (ScrollViewer)this.GetTemplateChild("CtrlScroll");
            SplitterGrid Headers = (SplitterGrid)this.GetTemplateChild("Headers");

            if (HeaderScroll == null || CtrlScroll == null || Headers == null)
                return;

            Point pt = e.GetPosition(HeaderScroll);

            double Scrollwidth = HeaderScroll.ActualWidth;

            ScrollBar VertScrollBar = CtrlScroll.Template.FindName("PART_VerticalScrollBar", CtrlScroll) as ScrollBar;

            if (HeaderScroll.ComputedVerticalScrollBarVisibility == Visibility.Visible && VertScrollBar != null)
                Scrollwidth -= VertScrollBar.Width; 
            
            if (pt.Y >= 0 && pt.Y <= HeaderScroll.ActualHeight)
            {
                if (pt.X >= 0 && pt.X <= Scrollwidth)
                    Headers.SplitGrid_MouseMove(sender, e);
                else if (HeaderScroll.ComputedVerticalScrollBarVisibility == Visibility.Visible && pt.X > Scrollwidth && pt.X <= HeaderScroll.ActualWidth)
                    Mouse.Capture(VertScrollBar, CaptureMode.None);
            }
        }
        private void Headers_WidthChanged(object sender, WidthChangedEventArgs e)
        {
            StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");
            Column col = CtrlStack.Children[e.Index] as Column;

            if (CtrlStack != null && col != null)
            {
                col.Width = e.Width;
            }
        }
        private double GetHeaderHeight() 
        {
            object Obj = TryFindResource("DGP_Head_Height");
            return (Obj is GridLength) ? ((GridLength)Obj).Value : 0.0;
        }
        private int GetVisibleRowCount()
        {
            ScrollViewer CtrlScroll = (ScrollViewer)this.GetTemplateChild("CtrlScroll");

            if (CtrlScroll == null)
                return 0;

            double VisibleHeight = CtrlScroll.ActualHeight - GetHeaderHeight();

            if (CtrlScroll.ComputedHorizontalScrollBarVisibility == Visibility.Visible)
                VisibleHeight -= SystemParameters.HorizontalScrollBarHeight;
            // Assume a row is visible if half or more of it is visible, not visible otherwise.
            return (int)Math.Floor(VisibleHeight/Column.RowHeight + 0.5);
        }
        private bool OnKeyDown(Key key, int iRow, Column Col, bool ShiftPressed)
        {
            StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");

            if (CtrlStack == null) return false;

            int iCol = CtrlStack.Children.IndexOf((UIElement)Col);
            bool handled = true;

            if (key == Key.Tab)
            {
                if (!ShiftPressed)
                {
                    if (iCol < CtrlStack.Children.Count - 1)
                    {
                        Column newCol = (Column)CtrlStack.Children[iCol + 1];
                        newCol.SetActive(iRow);
                    }
                    else
                    {
                        Column newCol = (Column)CtrlStack.Children[0];

                        if (iRow < newCol.RowCount() - 1)
                            newCol.SetActive(iRow + 1);
                        else
                            handled = false;
                    }
                }
                else
                {
                    if (iCol > 0)
                    {
                        Column newCol = (Column)CtrlStack.Children[iCol - 1];
                        newCol.SetActive(iRow);
                    }
                    else
                    {
                        Column newCol = (Column)CtrlStack.Children[CtrlStack.Children.Count - 1];

                        if (iRow > 0)
                            newCol.SetActive(iRow - 1);
                        else
                            handled = false;
                    }
                }
            }
            else if (key == Key.Up)
            {
                if (iRow != 0)
                    ((Column)CtrlStack.Children[iCol]).SetActive(iRow - 1);
            }
            else if (key == Key.Down)
            {
                if (iRow != ((Column)CtrlStack.Children[iCol]).RowCount() - 1)
                    ((Column)CtrlStack.Children[iCol]).SetActive(iRow + 1);
            }
            else if (key == Key.Left)
            {
                if (iCol != 0)
                    ((Column)CtrlStack.Children[iCol - 1]).SetActive(iRow);
            }
            else if (key == Key.Right)
            {
                if (iCol != CtrlStack.Children.Count - 1)
                    ((Column)CtrlStack.Children[iCol + 1]).SetActive(iRow);
            }
            else if (key == Key.PageDown)
            {
                int VisibleRows = GetVisibleRowCount();

                if (iRow + VisibleRows < ((Column)CtrlStack.Children[iCol]).RowCount())
                    ((Column)CtrlStack.Children[iCol]).SetActive(iRow + VisibleRows);
                else
                    ((Column)CtrlStack.Children[iCol]).SetActive(((Column)CtrlStack.Children[iCol]).RowCount() - 1);
            }
            else if (key == Key.PageUp)
            {
                int VisibleRows = GetVisibleRowCount();

                if (iRow - VisibleRows > 0)
                    ((Column)CtrlStack.Children[iCol]).SetActive(iRow - VisibleRows);
                else
                    ((Column)CtrlStack.Children[iCol]).SetActive(0);
            }
            else if (key == Key.Home)
            {
                ((Column)CtrlStack.Children[iCol]).SetActive(0);
            }
            else if (key == Key.End)
            {
                ((Column)CtrlStack.Children[iCol]).SetActive(((Column)CtrlStack.Children[iCol]).RowCount() - 1);
            }
            return handled;
        }
        private void Sort(int ColumnIndex, bool Ascending = true)
        {
            StackPanel CtrlStack = (StackPanel)this.GetTemplateChild("CtrlStack");

            if (CtrlStack == null) return;
            // Assume all rows are of same length - this is currently always the case.
            Column SelCol = CtrlStack.Children[ColumnIndex] as Column;

            if (SelCol != null)
            {
                SortStruct[] AllData = new SortStruct[SelCol.RowCount()];

                for (int Idx = 0; Idx < SelCol.RowCount(); Idx++)
                {
                    AllData[Idx].RowList = new List<UIElement>();
                    AllData[Idx].SortingObj = SelCol.GetRowValue(Idx);
                }

                for (int ColIdx = 0; ColIdx < CtrlStack.Children.Count; ColIdx++)
                {
                    Column Col = CtrlStack.Children[ColIdx] as Column;

                    if (Col != null)
                    {
                        for (int RowIdx = 0; RowIdx < SelCol.RowCount(); RowIdx++)
                        {
                            AllData[RowIdx].RowList.Add(Col.GetRowControl(RowIdx));
                        }
                    }
                }
                
                IEnumerable<SortStruct> Data;

                Data = (Ascending)? from d in AllData
                                    orderby d.SortingObj ascending select d 
                                  : from d in AllData
                                    orderby d.SortingObj descending select d;

                for (int ColIdx = 0; ColIdx < CtrlStack.Children.Count; ColIdx++)
                {
                    Column Col = CtrlStack.Children[ColIdx] as Column;

                    if (Col != null)
                    {
                        for (int RowIdx = 0; RowIdx < Col.RowCount(); RowIdx++)
                            Col.SetRowControl(RowIdx, null); 
                    }
                }
                    
                int RIdx = 0;
                foreach (SortStruct st in Data)
                {
                    for (int ColIdx = 0; ColIdx < CtrlStack.Children.Count; ColIdx++)
                    {
                        Column Col = CtrlStack.Children[ColIdx] as Column;

                        if (Col != null)
                            Col.SetRowControl(RIdx, (Control)st.RowList[ColIdx]);
                    }
                    RIdx++;
                }
            }
        }
        SortType OnHeaderClicked(Header header, SortType PreviousSortType)
        {
            SortType NewSortType;

            SplitterGrid Headers = (SplitterGrid)this.GetTemplateChild("Headers");

            if (Headers == null)
                return SortType.NotOrdered;
            
            int ColIdx = Headers.IndexOf((UIElement)header);

            if (PreviousSortType == SortType.NotOrdered)
            {
                // set all columns to unsorted.
                foreach (UIElement ui in Headers)
                    ((Header)ui).ResetSortType();

            // Alternative notation:
            //  for (int Idx = 0; Idx < Headers.Count; Idx++)
            //      ((Header)Headers[Idx]).ResetSortType();

                Sort(ColIdx, true);
                NewSortType = SortType.Ascending;
            }
            else if (PreviousSortType == SortType.Ascending)
            {
                Sort(ColIdx, false);
                NewSortType = SortType.Descending;
            }
            else //if (PreviousSortType == SortType.Descending)
            {
                Sort(ColIdx, true);
                NewSortType = SortType.Ascending;
            }
            return NewSortType;
        }
    }
}
