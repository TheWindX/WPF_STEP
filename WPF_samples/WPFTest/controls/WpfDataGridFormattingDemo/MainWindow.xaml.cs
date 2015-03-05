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


namespace Demo {

  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow: Window {


    public MainWindow() {
      InitializeComponent();

      //create business data
      var itemList = new List<StockItem>();
      itemList.Add(new StockItem {Name= "Many items",      Quantity=100, IsObsolete=false});
      itemList.Add(new StockItem {Name= "Enough items",    Quantity=10,  IsObsolete=false});
      itemList.Add(new StockItem {Name= "Shortage item",   Quantity=1,   IsObsolete=false});
      itemList.Add(new StockItem {Name= "Item with error", Quantity=-1,  IsObsolete=false});
      itemList.Add(new StockItem {Name= "Obsolete item",   Quantity=200, IsObsolete=true });

      //link business data to CollectionViewSource
      CollectionViewSource itemCollectionViewSource;
      itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
      itemCollectionViewSource.Source = itemList;
      
    }


  }
}
