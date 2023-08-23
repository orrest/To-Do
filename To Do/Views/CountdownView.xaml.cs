﻿using System;
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
    /// EmailView.xaml 的交互逻辑
    /// </summary>
    public partial class CountdownView : UserControl
    {
        public const string Title = "倒计时";
        public const string Color = "#9b2a46";
        public const string Icon = "TimerStarOutline";

        public CountdownView()
        {
            InitializeComponent();
        }
    }
}
