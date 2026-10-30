/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

using TeachingDeviceManager.Models;

namespace TeachingDeviceManager.Services;

public delegate bool AlertFilter(DeviceAlertEventArgs alert);

public class FilteredAlertSubscriber
{
    private readonly AlertFilter filter;
    private readonly Action<DeviceAlertEventArgs> action;

    public FilteredAlertSubscriber(AlertFilter filter, Action<DeviceAlertEventArgs> action)
    {
        this.filter = filter;
        this.action = action;
    }

    public void HandleAlert(object? sender, DeviceAlertEventArgs e)
    {
        if (filter(e)) action(e);
    }
}