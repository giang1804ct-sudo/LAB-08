/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

using TeachingDeviceManager.Models;

namespace TeachingDeviceManager.Services;

public class ConsoleNotificationService
{
    public void HandleAlert(object? sender, DeviceAlertEventArgs e)
    {
        Console.WriteLine($"[{e.Severity}] {e.DeviceId}: {e.Message}");
    }
}