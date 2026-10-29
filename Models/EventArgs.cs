/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

namespace TeachingDeviceManager.Models;

// Sự kiện thay đổi trạng thái
public class DeviceStatusChangedEventArgs : EventArgs
{
    public string DeviceId { get; }
    public DeviceStatus OldStatus { get; }
    public DeviceStatus NewStatus { get; }
    public DateTime ChangedAt { get; }

    public DeviceStatusChangedEventArgs(string deviceId, DeviceStatus oldStatus, DeviceStatus newStatus, DateTime changedAt)
    {
        DeviceId = deviceId;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedAt = changedAt;
    }
}

// Sự kiện cảnh báo thiết bị
public class DeviceAlertEventArgs : EventArgs
{
    public string DeviceId { get; }
    public string Message { get; }
    public AlertSeverity Severity { get; }
    public DateTime OccurredAt { get; }

    public DeviceAlertEventArgs(string deviceId, string message, AlertSeverity severity, DateTime occurredAt)
    {
        DeviceId = deviceId;
        Message = message;
        Severity = severity;
        OccurredAt = occurredAt;
    }
}