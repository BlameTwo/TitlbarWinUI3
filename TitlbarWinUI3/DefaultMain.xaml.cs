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
        //1. ��ȡDPI����
        var dpivalue = Win32.GetScaleAdjustment(App.Window);
        // ����ϵͳԤ������
        var leftsystemPadding = App.Window.AppWindow.TitleBar.LeftInset;
        var rightsystemPadding = App.Window.AppWindow.TitleBar.RightInset;
        //2. ��ֵϵͳԤ�����϶�
        LeftPaddingColumn.Width = new GridLength(pixels: leftsystemPadding / dpivalue);
        RightPaddingColumn.Width = new GridLength(pixels: rightsystemPadding / dpivalue);
        //�߼����ش�С���������ش�С/DPI����
        //�������ش�С
        List<RectInt32> rects = new();
        RectInt32 rectLeft = new(); //���϶�����
        RectInt32 rectRight = new(); //���϶�����
        //��ά�ľ��η�Χ
        //�����X�����ʼλ��
        rectLeft.X = (int)((LeftPaddingColumn.ActualWidth + HeaderColumn.ActualWidth) * dpivalue);
        //�����Y����ʼλ�ã�����0
        rectLeft.Y = 0;
        //������εĸ߶�
        rectLeft.Height = (int)(mygrid.ActualHeight * dpivalue);
        //��������εĿ��
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
        //���ñ������϶�����
        App.Window.AppWindow.TitleBar.SetDragRectangles(rects.ToArray());
    }
}
