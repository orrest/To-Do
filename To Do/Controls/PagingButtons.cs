using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace To_Do.Controls;

public class PagingButtons : Control
{
    static PagingButtons()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(PagingButtons),
            new FrameworkPropertyMetadata(typeof(PagingButtons)));
    }


    public ICommand PageBackwardCommand
    {
        get { return (ICommand)GetValue(PageBackwardCommandProperty); }
        set { SetValue(PageBackwardCommandProperty, value); }
    }
    public static readonly DependencyProperty PageBackwardCommandProperty =
        DependencyProperty.Register(
            "PageBackwardCommand",
            typeof(ICommand),
            typeof(PagingButtons),
            new PropertyMetadata(null));



    public ICommand PageForwardCommand
    {
        get { return (ICommand)GetValue(PageForwardCommandProperty); }
        set { SetValue(PageForwardCommandProperty, value); }
    }
    public static readonly DependencyProperty PageForwardCommandProperty =
        DependencyProperty.Register(
            "PageForwardCommand",
            typeof(ICommand),
            typeof(PagingButtons),
            new PropertyMetadata(null));



    public ICommand PageRefreshCommand
    {
        get { return (ICommand)GetValue(PageRefreshCommandProperty); }
        set { SetValue(PageRefreshCommandProperty, value); }
    }
    public static readonly DependencyProperty PageRefreshCommandProperty =
        DependencyProperty.Register(
            "PageRefreshCommand",
            typeof(ICommand),
            typeof(PagingButtons),
            new PropertyMetadata(null));



    public bool IsRefreshEnable
    {
        get { return (bool)GetValue(IsRefreshEnableProperty); }
        set { SetValue(IsRefreshEnableProperty, value); }
    }
    public static readonly DependencyProperty IsRefreshEnableProperty =
        DependencyProperty.Register(
            "IsRefreshEnable",
            typeof(bool),
            typeof(PagingButtons),
            new PropertyMetadata(null));


    public bool IsBackwardEnable
    {
        get { return (bool)GetValue(IsBackwardEnableProperty); }
        set { SetValue(IsBackwardEnableProperty, value); }
    }
    public static readonly DependencyProperty IsBackwardEnableProperty =
        DependencyProperty.Register(
            "IsBackwardEnable",
            typeof(bool),
            typeof(PagingButtons),
            new PropertyMetadata(null));


    public bool IsForwardEnable
    {
        get { return (bool)GetValue(IsForwardEnableProperty); }
        set { SetValue(IsForwardEnableProperty, value); }
    }
    public static readonly DependencyProperty IsForwardEnableProperty =
        DependencyProperty.Register(
            "IsForwardEnable",
            typeof(bool),
            typeof(PagingButtons),
            new PropertyMetadata(null));
}
