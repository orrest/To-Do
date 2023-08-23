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
    /// WeekView.xaml 的交互逻辑
    /// </summary>
    public partial class WeekView : UserControl
    {
        public const string Title = "周任务";
        public const string Color = "#78b1ad";
        public const string Icon = "CalendarWeekOutline";
        public WeekView()
        {
            InitializeComponent();
        }
    }
}
