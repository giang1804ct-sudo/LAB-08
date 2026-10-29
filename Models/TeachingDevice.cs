/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

namespace TeachingDeviceManager.Models;

public abstract class TeachingDevice
{
    public string Id { get; }
    public string Name { get; }
    public DeviceStatus Status { get; private set; }

    // 2 Event chính của thiết bị
    public event EventHandler<DeviceStatusChangedEventArgs>? StatusChanged;
    public event EventHandler<DeviceAlertEventArgs>? AlertRaised;

    protected TeachingDevice(string id, string name)
    {
        Id = id;
        Name = name;
        Status = DeviceStatus.Available;
    }

    public void ChangeStatus(DeviceStatus newStatus)
    {
        // Trạng thái mới bằng trạng thái cũ thì bỏ qua
        if (newStatus == Status) return;

        DeviceStatus oldStatus = Status;
        Status = newStatus;

        OnStatusChanged(new DeviceStatusChangedEventArgs(Id, oldStatus, newStatus, DateTime.Now));
    }

    protected void RaiseAlert(string message, AlertSeverity severity)
    {
        OnAlertRaised(new DeviceAlertEventArgs(Id, message, severity, DateTime.Now));
    }

    protected virtual void OnStatusChanged(DeviceStatusChangedEventArgs e)
    {
        StatusChanged?.Invoke(this, e);
    }

    protected virtual void OnAlertRaised(DeviceAlertEventArgs e)
    {
        AlertRaised?.Invoke(this, e);
    }
}