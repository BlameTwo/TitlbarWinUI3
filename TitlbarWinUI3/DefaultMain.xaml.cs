using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Windows.Graphics;
using Windows.UI;

namespace TitlbarWinUI3;

public sealed partial class DefaultMain : Page
{
    public DefaultMain()
    {
        this.InitializeComponent();
        Loaded += DefaultMain_Loaded;
        SizeChanged += DefaultMain_SizeChanged;
    }

    private void DefaultMain_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpDateTitlebar();
    }

    private void DefaultMain_Loaded(object sender, RoutedEventArgs e)
    {
        App.Window.AppWindow.TitleBar.PreferredHeightOption = Microsoft.UI.Windowing.TitleBarHeightOption.Tall;
        App.Window.AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
        UpDateTitlebar();
    }

    private void UpDateTitlebar()
    {
        //1. 获取DPI缩放
        var dpivalue = Win32.GetScaleAdjustment(App.Window);
        // 计算系统预留区域
        var leftsystemPadding = App.Window.AppWindow.TitleBar.LeftInset;
        var rightsystemPadding = App.Window.AppWindow.TitleBar.RightInset;
        //2. 赋值系统预留右拖动
        LeftPaddingColumn.Width = new GridLength(pixels: leftsystemPadding / dpivalue);
        RightPaddingColumn.Width = new GridLength(pixels: rightsystemPadding / dpivalue);
        //逻辑像素大小，物理像素大小/DPI缩放
        //物理像素大小
        List<RectInt32> rects = new();
        RectInt32 rectLeft = new(); //左拖动区域
        RectInt32 rectRight = new(); //右拖动区域
        //二维的矩形范围
        //计算出X轴的起始位置
        rectLeft.X = (int)((LeftPaddingColumn.ActualWidth + HeaderColumn.ActualWidth) * dpivalue);
        //计算出Y轴起始位置，就是0
        rectLeft.Y = 0;
        //计算矩形的高度
        rectLeft.Height = (int)(mygrid.ActualHeight * dpivalue);
        //计算出矩形的宽度
        rectLeft.Width = (int)(
            (TitlebarColumn.ActualWidth + LeftDropColumn.ActualWidth) * dpivalue
        );
        rects.Add(rectLeft);


        rectRight.X = (int)(
            (
                LeftPaddingColumn.ActualWidth
                + HeaderColumn.ActualWidth
                + TitlebarColumn.ActualWidth
                + LeftDropColumn.ActualWidth
                + ContentColumn.ActualWidth
            ) * dpivalue
        );
        rectRight.Y = 0;
        rectRight.Height = (int)(mygrid.ActualHeight * dpivalue);
        rectRight.Width = (int)(RightDropColumn.ActualWidth * dpivalue);
        rects.Add(rectRight);
        //设置标题栏拖动区域
        App.Window.AppWindow.TitleBar.SetDragRectangles(rects.ToArray());
    }
}
