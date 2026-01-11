# TaskbarMonitor Avalonia Demo

这是一个完整的 Avalonia Demo，用于演示如何把 Avalonia 的 `UserControl` 作为子窗口插入到任务栏，并支持点击事件与动态更新显示内容。

## 运行步骤

1. 确保已安装 .NET 8 SDK。
2. 在仓库根目录执行：

```bash
cd TaskbarMonitorAvaloniaDemo
```

3. 运行 Demo：

```bash
dotnet run
```

4. 运行后会在任务栏右侧看到一个小控件：
   - 显示当前时间与点击次数。
   - 点击 **Click** 按钮会立即更新点击次数（演示点击事件）。
   - 时间每秒更新一次（演示动态更新）。

## 关键点

- `MainWindow` 是一个无边框窗口，启动后会调用 `TaskbarHost.AttachToTaskbar`。
- `TaskbarHost` 使用 `FindWindow("Shell_TrayWnd")` + `SetParent` 把 Avalonia 窗口设置为任务栏子窗口。
- `TaskbarWidget` 是普通 Avalonia `UserControl`，包含点击事件与定时更新逻辑。
