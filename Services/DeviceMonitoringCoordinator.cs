/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

using TeachingDeviceManager.Models;

namespace TeachingDeviceManager.Services;

// Lop dieu phoi dang ky su kien (v3 - Hoan thien chan dang ky trung va don dep su kien)
public class DeviceMonitoringCoordinator : IDisposable
{
    private readonly List<TeachingDevice> devices;
    private readonly ConsoleNotificationService notificationService;
    private readonly DeviceAuditLogger auditLogger;
    private readonly MaintenanceService maintenanceService;

    // Co danh dau trang thai dang ky de tranh dang ky trung lap nhieu lan
    private bool isSubscribed = false;

    public DeviceMonitoringCoordinator(
        IEnumerable<TeachingDevice> devices,
        ConsoleNotificationService notificationService,
        DeviceAuditLogger auditLogger,
        MaintenanceService maintenanceService)
    {
        this.devices = devices.ToList();
        this.notificationService = notificationService;
        this.auditLogger = auditLogger;
        this.maintenanceService = maintenanceService;
    }

    public void Subscribe()
    {
        // Fix o ban v3: Neu da subscribe roi thi bo qua, khong dang ky trung cung 1 handler
        if (isSubscribed)
        {
            return;
        }

        foreach (var d in devices)
        {
            d.AlertRaised += notificationService.HandleAlert;
            d.AlertRaised += maintenanceService.HandleCriticalAlert;
            d.StatusChanged += auditLogger.HandleStatusChanged;
        }

        isSubscribed = true;
    }

    public void Unsubscribe()
    {
        // Neu chua tung subscribe thi khong can unsubscribe
        if (!isSubscribed)
        {
            return;
        }

        foreach (var d in devices)
        {
            d.AlertRaised -= notificationService.HandleAlert;
            d.AlertRaised -= maintenanceService.HandleCriticalAlert;
            d.StatusChanged -= auditLogger.HandleStatusChanged;
        }

        isSubscribed = false;
    }

    public void Dispose()
    {
        Unsubscribe();
    }
}
