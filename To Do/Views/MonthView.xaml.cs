using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace To_Do.Views
{
    /// <summary>
    /// MonthView.xaml 的交互逻辑
    /// </summary>
    public partial class MonthView : UserControl
    {
        public const string Title = "月任务";
        public const string Color = "#84b09d";
        public const string Icon = "CalendarMonthOutline";

        public MonthView()
        {
            InitializeComponent();
        }
    }
}
