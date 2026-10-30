/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

using TeachingDeviceManager.Models;

namespace TeachingDeviceManager.Services;

public class MaintenanceService
{
    private readonly List<string> requests = new();
    public IReadOnlyList<string> Requests => requests.AsReadOnly();

    public void HandleCriticalAlert(object? sender, DeviceAlertEventArgs e)
    {
        if (e.Severity != AlertSeverity.Critical) return;
        requests.Add($"Maintenance request for {e.DeviceId}: {e.Message}");
    }
}