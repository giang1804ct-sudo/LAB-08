/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

using TeachingDeviceManager.Models;
using TeachingDeviceManager.Services;
using TeachingDeviceManager.Exceptions;

Console.WriteLine("=== CHUONG TRINH QUAN LY THIET BI GIANG DUONG (v2 - Fixed 1 phan) ===");

// 1. Khoi tao cac thiet bi
var projector = new ProjectorRemote("PRJ-01", "Dieu khien may chieu phong A101");
var ac = new AirConditionerRemote("AC-01", "Dieu khien dieu hoa phong A101", "A101");
var mic = new WirelessMicrophone("MIC-01", "Micro khong day phong A101");

var devices = new List<TeachingDevice> { projector, ac, mic };

var consoleNotification = new ConsoleNotificationService();
var auditLogger = new DeviceAuditLogger();
var maintenanceService = new MaintenanceService();

var coordinator = new DeviceMonitoringCoordinator(devices, consoleNotification, auditLogger, maintenanceService);
coordinator.Subscribe();

Console.WriteLine("\n--- TEST 1: Thay doi trang thai thiet bi ---");
projector.ChangeStatus(DeviceStatus.Borrowed);
projector.ChangeStatus(DeviceStatus.Borrowed); // Trạng thái cũ == mới -> không phát event
projector.ChangeStatus(DeviceStatus.UnderMaintenance);

Console.WriteLine("\n--- TEST 2: Kiem tra fix loi pin may chieu 0% ---");
projector.UpdateBatteryLevel(15); // Xuong 15% -> Warning
projector.UpdateBatteryLevel(0);  // Xuong 0% -> Da duoc sua thanh Critical dung yeu cau!

Console.WriteLine("\n--- TEST 3: Kiem tra fix loi spam dieu hoa mat ket noi ---");
ac.UpdateResponseState(false); // Tu true -> false: Phat Critical
ac.UpdateResponseState(false); // Van la false: Da khong con bi phat lai lan 2!
Console.WriteLine("-> Goi lai UpdateResponseState(false) thanh cong ma khong bi spam alert.");

Console.WriteLine("\n--- TEST 4: Kiem tra loi con sot: Chua chan dang ky trung handler ---");
// Sinh vien goi tiep Subscribe lan 2 ma khong biet Coordinator chua co bien chan
coordinator.Subscribe();
Console.WriteLine("-> Da vo tinh goi Subscribe() lan 2, neu co event phat sinh se bi in 2 lan!");
projector.ChangeStatus(DeviceStatus.Available); // Se bi ghi log 2 lan do dang ky trung

Console.WriteLine("\n--- TEST 5: Kiem tra loi con sot: Chua nem ngoai le khi gia tri khong hop le ---");
// TODO: Micro van chua validate 0-100, can fix o ban v3
mic.UpdateBatteryLevel(120); // Vuot qua 100%
Console.WriteLine($"Pin hien tai cua micro: {mic.BatteryLevel}% (vuot qua 100% ma chua nem InvalidDeviceReadingException)");

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
Console.WriteLine("\n=== HOAN THANH CHAY BAN v2 ===");
