using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Input;
using To_Do.Helpers;

namespace To_Do.Views;

public partial class MainWindow : Window
{
    private readonly IRegionManager regionManager;

    public MainWindow(IRegionManager regionManager)
    {
        InitializeComponent();
        this.regionManager = regionManager;
    }

    private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void TitleBar_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        WindowScale();
    }

    private void WindowScale()
    {
        WindowState = WindowState == WindowState.Maximized ?
            WindowState.Normal : WindowState.Maximized;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        regionManager.RequestNavigate(
            Constants.MAIN_CONTENT_REGION,
            nameof(MainView));
    }

    protected override void OnStateChanged(EventArgs e)
    {
        if (this.WindowState == WindowState.Minimized)
        {
            this.Hide();
        }
        base.OnStateChanged(e);
    }

    private void TrayIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
    {
        if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
        {
            this.Show();
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            Application.Current.MainWindow.Activate();
        }
        else
        {
            Application.Current.MainWindow.Activate();
        }
    }

    private void MenuExit_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
