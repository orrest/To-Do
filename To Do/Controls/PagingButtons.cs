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


    public ICommand PageBackCommand
    {
        get { return (ICommand)GetValue(PageBackCommandProperty); }
        set { SetValue(PageBackCommandProperty, value); }
    }
    public static readonly DependencyProperty PageBackCommandProperty =
        DependencyProperty.Register(
            "PageBackCommand",
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


}
