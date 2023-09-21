using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace TitlbarWinUI3;

public partial class App : Application
{
    public App()
    {
        this.InitializeComponent();
    }


    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        Window = new();
        Window.SystemBackdrop = new MicaBackdrop();
        Window.Content = new DefaultMain();
        //将内容区域扩展到标题栏，以隐藏系统默认标题栏
        Window.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        Window.Activate();
    }

    public static Window Window;
}
