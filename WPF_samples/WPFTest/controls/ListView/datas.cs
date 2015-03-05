using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;

using System.Collections.ObjectModel;

namespace nsListView
{
    public class listViewItem
    {
        public int x{set;get;}
        public int y{set;get;}
        public int z{set;get;}
        public string value{set;get;}
    }

    public class listViewDatas
    {
        public static List<listViewItem> datas = new List<listViewItem>();
        public static void build()
        {
            datas.Add(new listViewItem() { x = 1, y = 2, z = 3, value = "1" });
            datas.Add(new listViewItem() { x = 11, y = 22, z = 33, value = "2" });
            datas.Add(new listViewItem() { x = 100, y = 200, z = 300, value = "3" });
            datas.Add(new listViewItem() { x = 100, y = 200, z = 300, value = "3" });
            datas.Add(new listViewItem() { x = 100, y = 200, z = 300, value = "3" });
            datas.Add(new listViewItem() { x = 100, y = 200, z = 300, value = "3" });

        }
    }


    public enum ComboBoxDataType
    {
        type1,
        type2,
        type3,
    }

    public class ComboBoxData
    {
        public ObservableCollection<string> enumData
        {
            get
            {
                return new ObservableCollection<string>()
                {
                    "item1","item2","item3",
                };
            }
        }
        public static ComboBoxData instance = new ComboBoxData();
    }
}
