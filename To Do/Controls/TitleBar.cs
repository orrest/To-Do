using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace To_Do.Controls;

public class TitleBar : Control
{
    static TitleBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(typeof(TitleBar)));
    }

    #region Title
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            "Title", 
            typeof(string), 
            typeof(TitleBar), 
            new PropertyMetadata(string.Empty));
    #endregion

    #region Icon Kind
    public PackIconKind IconKind
    {
        get { return (PackIconKind)GetValue(IconKindProperty); }
        set { SetValue(IconKindProperty, value); }
    }

    public static readonly DependencyProperty IconKindProperty =
        DependencyProperty.Register(
            "IconKind", 
            typeof(PackIconKind), 
            typeof(TitleBar), 
            new PropertyMetadata(PackIconKind.Abacus));
    #endregion
}
