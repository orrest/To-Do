using System.Windows;
using System.Windows.Controls;

namespace To_Do.Controls;

public class LoadingProgressBar : Control
{
    static LoadingProgressBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(LoadingProgressBar),
            new FrameworkPropertyMetadata(typeof(LoadingProgressBar)));
    }

    public bool IsLoading
    {
        get { return (bool)GetValue(IsLoadingProperty); }
        set { SetValue(IsLoadingProperty, value); }
    }

    public static readonly DependencyProperty IsLoadingProperty =
        DependencyProperty.Register(
            "IsLoading",
            typeof(bool),
            typeof(LoadingProgressBar),
            new PropertyMetadata(false));


}
