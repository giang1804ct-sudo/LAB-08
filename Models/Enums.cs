/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

namespace TeachingDeviceManager.Models;

// Trạng thái thiết bị
public enum DeviceStatus
{
    Available,
    Borrowed,
    UnderMaintenance
}

// Mức độ cảnh báo
public enum AlertSeverity
{
    Information,
    Warning,
    Critical
}