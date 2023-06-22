using Prism.Regions;
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

    private void btnMin_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void btnMax_Click(object sender, RoutedEventArgs e)
    {
        WindowScale();
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Close();
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
            Constants.MAIN_VIEW);
    }
}
