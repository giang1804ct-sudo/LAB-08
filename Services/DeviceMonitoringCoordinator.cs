/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

using TeachingDeviceManager.Models;

namespace TeachingDeviceManager.Services;

public class DeviceMonitoringCoordinator : IDisposable
{
    private readonly List<TeachingDevice> devices;
    private readonly ConsoleNotificationService notificationService;
    private readonly DeviceAuditLogger auditLogger;
    private readonly MaintenanceService maintenanceService;

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
        // TODO: Chua co co chan dang ky trung neu goi Subscribe() 2 lan
        foreach (var d in devices)
        {
            d.AlertRaised += notificationService.HandleAlert;
            d.AlertRaised += maintenanceService.HandleCriticalAlert;
            d.StatusChanged += auditLogger.HandleStatusChanged;
        }
    }

    public void Unsubscribe()
    {
        foreach (var d in devices)
        {
            d.AlertRaised -= notificationService.HandleAlert;
            d.AlertRaised -= maintenanceService.HandleCriticalAlert;
            d.StatusChanged -= auditLogger.HandleStatusChanged;
        }
    }

    public void Dispose()
    {
        Unsubscribe();
    }
}