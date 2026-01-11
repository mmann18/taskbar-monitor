using Avalonia.Controls;
using Avalonia.Threading;
using TaskbarMonitorAvaloniaDemo.Interop;

namespace TaskbarMonitorAvaloniaDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Opened += (_, _) =>
        {
            Dispatcher.UIThread.Post(() => TaskbarHost.AttachToTaskbar(this));
        };
    }
}
