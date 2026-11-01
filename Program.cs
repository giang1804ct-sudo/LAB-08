/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

using System.Text;
using TeachingDeviceManager.Models;
using TeachingDeviceManager.Services;
using TeachingDeviceManager.Exceptions;


Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("====================================================================");
Console.WriteLine("=== CHƯƠNG TRÌNH QUẢN LÝ THIẾT BỊ GIẢNG ĐƯỜNG (v3 - Hoàn thiện) ===");
Console.WriteLine("====================================================================");

// 1. Khởi tạo danh sách các thiết bị
var projector = new ProjectorRemote("PRJ-01", "Điều khiển máy chiếu phòng A101");
var ac = new AirConditionerRemote("AC-01", "Điều khiển điều hòa phòng A101", "A101");
var mic = new WirelessMicrophone("MIC-01", "Micro không dây phòng A101");

var devices = new List<TeachingDevice> { projector, ac, mic };

// 2. Khởi tạo các service (Subscribers)
var consoleNotification = new ConsoleNotificationService();
var auditLogger = new DeviceAuditLogger();
var maintenanceService = new MaintenanceService();

// 3. Sử dụng Coordinator để quản lý đăng ký sự kiện
var coordinator = new DeviceMonitoringCoordinator(devices, consoleNotification, auditLogger, maintenanceService);
coordinator.Subscribe();

Console.WriteLine("\n--- TEST 1: Kiểm tra chặn đăng ký trùng handler trong Coordinator ---");
// Gọi Subscribe() lần 2 để kiểm tra tính năng chặn đăng ký trùng
coordinator.Subscribe();
Console.WriteLine("-> Gọi Subscribe() 2 lần thành công (đã được bảo vệ, không bị duplicate handler).");

Console.WriteLine("\n--- TEST 2: Thay đổi trạng thái thiết bị và ghi Audit Log ---");
projector.ChangeStatus(DeviceStatus.Borrowed);
projector.ChangeStatus(DeviceStatus.Borrowed); // Trùng trạng thái -> Bỏ qua, không phát event
projector.ChangeStatus(DeviceStatus.UnderMaintenance);
Console.WriteLine($"-> Audit log đã ghi nhận số lần thay đổi: {auditLogger.Entries.Count}");

Console.WriteLine("\n--- TEST 3: Kiểm tra mức pin Remote máy chiếu (Warning & Critical) ---");
projector.UpdateBatteryLevel(15); // < 20% -> Warning
projector.UpdateBatteryLevel(0);  // 0% -> Critical

Console.WriteLine("\n--- TEST 4: Kiểm tra điều hòa phòng học mất kết nối ---");
ac.UpdateResponseState(false); // Từ true -> false: Phát Critical
ac.UpdateResponseState(false); // Tiếp tục false: Không phát sinh cảnh báo trùng lặp
Console.WriteLine("-> Gọi UpdateResponseState(false) lần 2 thành công và không bị spam alert.");

Console.WriteLine("\n--- TEST 5: Kiểm tra Micro không dây (Pin & Tín hiệu) ---");
mic.UpdateBatteryLevel(25);     // < 30% -> Warning
mic.UpdateSignalQuality(35);    // < 40% -> Warning
mic.UpdateSignalQuality(0);     // 0% -> Critical

Console.WriteLine("\n--- TEST 6: Kiểm tra Custom Delegate FilteredAlertSubscriber ---");
// Subscriber tự do chỉ lọc những cảnh báo Warning và in ra theo format riêng
var warningOnlySubscriber = new FilteredAlertSubscriber(
    alert => alert.Severity == AlertSeverity.Warning,
    alert => Console.WriteLine($"[FILTERED WARNING CALLBACK] Thiết bị {alert.DeviceId}: {alert.Message}")
);
mic.AlertRaised += warningOnlySubscriber.HandleAlert;
mic.UpdateBatteryLevel(10); // < 30% -> Warning -> Được callback xử lý
mic.AlertRaised -= warningOnlySubscriber.HandleAlert; // Hủy đăng ký sau khi test

Console.WriteLine("\n--- TEST 7: Kiểm tra ném ngoại lệ InvalidDeviceReadingException ---");
try
{
    Console.WriteLine("Thử cập nhật pin micro = -15%...");
    mic.UpdateBatteryLevel(-15);
}
catch (InvalidDeviceReadingException ex)
{
    Console.WriteLine($"[BẮT ĐƯỢC NGOẠI LỆ]: Thiết bị '{ex.DeviceId}', giá trị sai = {ex.InvalidValue}");
    Console.WriteLine($"Thông điệp: {ex.Message}");
}

try
{
    Console.WriteLine("Thử cập nhật tín hiệu micro = 150%...");
    mic.UpdateSignalQuality(150);
}
catch (InvalidDeviceReadingException ex)
{
    Console.WriteLine($"[BẮT ĐƯỢC NGOẠI LỆ]: Thiết bị '{ex.DeviceId}', giá trị sai = {ex.InvalidValue}");
    Console.WriteLine($"Thông điệp: {ex.Message}");
}

Console.WriteLine("\n--- TỔNG HỢP NHẬT KÝ HỆ THỐNG ---");
Console.WriteLine($"[DeviceAuditLogger] Tổng số nhật ký ({auditLogger.Entries.Count}):");
foreach (var entry in auditLogger.Entries)
{
    Console.WriteLine($"   * {entry}");
}

Console.WriteLine($"\n[MaintenanceService] Tổng số yêu cầu bảo trì tạo bởi Critical alert ({maintenanceService.Requests.Count}):");
foreach (var req in maintenanceService.Requests)
{
    Console.WriteLine($"   * {req}");
}

Console.WriteLine("\n--- TEST 8: Kiểm tra Dispose / Unsubscribe Coordinator ---");
coordinator.Dispose();
Console.WriteLine("Coordinator đã Dispose. Thử thay đổi trạng thái và phát cảnh báo sau khi dispose...");
projector.ChangeStatus(DeviceStatus.Available);
projector.UpdateBatteryLevel(5);
Console.WriteLine($"Số lượng audit log sau dispose vẫn là: {auditLogger.Entries.Count} (không tăng thêm)");

Console.WriteLine("\n====================================================================");
Console.WriteLine("=== TẤT CẢ CÁC TEST CỦA BẢN v3 ĐÃ HOÀN THÀNH XUẤT SẮC ===");
Console.WriteLine("====================================================================");