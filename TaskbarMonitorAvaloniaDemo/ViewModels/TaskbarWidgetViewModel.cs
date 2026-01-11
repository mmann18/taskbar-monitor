using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Threading;

namespace TaskbarMonitorAvaloniaDemo.ViewModels;

public sealed class TaskbarWidgetViewModel : INotifyPropertyChanged
{
    private readonly DispatcherTimer _timer;
    private int _clickCount;
    private string _statusText = string.Empty;

    public TaskbarWidgetViewModel()
    {
        UpdateStatusText();
        _timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (_, _) =>
        {
            UpdateStatusText();
        });
        _timer.Start();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string StatusText
    {
        get => _statusText;
        private set
        {
            if (_statusText != value)
            {
                _statusText = value;
                OnPropertyChanged();
            }
        }
    }

    public void RecordClick()
    {
        _clickCount++;
        UpdateStatusText();
    }

    private void UpdateStatusText()
    {
        StatusText = $"Avalonia @ {DateTime.Now:HH:mm:ss} (Clicks: {_clickCount})";
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
