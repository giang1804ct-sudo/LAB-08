/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

using TeachingDeviceManager.Models;

namespace TeachingDeviceManager.Services;

public class DeviceAuditLogger
{
    private readonly List<string> entries = new();
    public IReadOnlyList<string> Entries => entries.AsReadOnly();

    public void HandleStatusChanged(object? sender, DeviceStatusChangedEventArgs e)
    {
        entries.Add($"{e.ChangedAt}: {e.DeviceId}, {e.OldStatus} -> {e.NewStatus}");
    }
}