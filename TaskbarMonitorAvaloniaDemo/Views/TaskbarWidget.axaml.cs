using Avalonia.Controls;
using TaskbarMonitorAvaloniaDemo.ViewModels;

namespace TaskbarMonitorAvaloniaDemo.Views;

public partial class TaskbarWidget : UserControl
{
    private readonly TaskbarWidgetViewModel _viewModel = new();

    public TaskbarWidget()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }

    private void OnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _viewModel.RecordClick();
    }
}
