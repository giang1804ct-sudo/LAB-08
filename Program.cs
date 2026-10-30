/****************/
// Mã sinh viên: 22001234
// Họ tên: Nguyễn Văn An
/****************/

using TeachingDeviceManager.Models;
using TeachingDeviceManager.Services;
using TeachingDeviceManager.Exceptions;

Console.WriteLine("=== CHUONG TRINH QUAN LY THIET BI GIANG DUONG (v1 - Draft) ===");

// 1. Khoi tao cac thiet bi
var projector = new ProjectorRemote("PRJ-01", "Dieu khien may chieu phong A101");
var ac = new AirConditionerRemote("AC-01", "Dieu khien dieu hoa phong A101", "A101");
var mic = new WirelessMicrophone("MIC-01", "Micro khong day phong A101");

var devices = new List<TeachingDevice> { projector, ac, mic };

// 2. Khoi tao cac subscriber
var consoleNotification = new ConsoleNotificationService();
var auditLogger = new DeviceAuditLogger();
var maintenanceService = new MaintenanceService();

// 3. Su dung Coordinator de dang ky su kien
var coordinator = new DeviceMonitoringCoordinator(devices, consoleNotification, auditLogger, maintenanceService);
coordinator.Subscribe();

Console.WriteLine("\n--- TEST 1: Thay doi trang thai thiet bi ---");
projector.ChangeStatus(DeviceStatus.Borrowed);
projector.ChangeStatus(DeviceStatus.Borrowed); // Trạng thái không đổi, không phát event
projector.ChangeStatus(DeviceStatus.UnderMaintenance);

Console.WriteLine("\n--- TEST 2: Kiem tra pin may chieu giam xuong 0% ---");
projector.UpdateBatteryLevel(15); // Xuong 15% -> hien Warning
projector.UpdateBatteryLevel(0);  // Xuong 0% -> SV mong doi Critical nhung bi nham sang Warning vi 0 < 20!

Console.WriteLine("\n--- TEST 3: Kiem tra dieu hoa mat ket noi 2 lan lien tiep ---");
ac.UpdateResponseState(false); // Mat ket noi -> bao Critical
ac.UpdateResponseState(false); // Van mat ket noi -> bi spam tiep canh bao (loi chua chan lap)

Console.WriteLine("\n--- TEST 4: Micro nhan gia tri am (-10%) ---");
// TODO: Cho nay le ra phai nem ngoai le InvalidDeviceReadingException, nhung hien tai chua validate
mic.UpdateBatteryLevel(-10);
Console.WriteLine($"Pin hien tai cua micro: {mic.BatteryLevel}% (bi am ma khong bao loi!)");

Console.WriteLine("\n--- TEST 5: Kiem tra delegate FilteredAlertSubscriber ---");
var criticalOnlySubscriber = new FilteredAlertSubscriber(
    alert => alert.Severity == AlertSeverity.Critical,
    alert => Console.WriteLine($"[CUSTOM CALLBACK CRITICAL] {alert.DeviceId}: {alert.Message}")
);
// Thu dang ky truc tiep vao micro
mic.AlertRaised += criticalOnlySubscriber.HandleAlert;
mic.UpdateSignalQuality(0); // Mat tin hieu -> Critical

Console.WriteLine("\n--- KET QUA GHI LOG VA BAO TRI ---");
Console.WriteLine($"Tong so nhat ky thay doi trang thai (Audit Logger): {auditLogger.Entries.Count}");
foreach (var entry in auditLogger.Entries)
{
    Console.WriteLine($" + {entry}");
}

Console.WriteLine($"\nTong so yeu cau bao tri (Maintenance Service): {maintenanceService.Requests.Count}");
foreach (var req in maintenanceService.Requests)
{
    Console.WriteLine($" + {req}");
}

coordinator.Dispose();
Console.WriteLine("\n=== HOAN THANH CHAY BAN v1 ===");
