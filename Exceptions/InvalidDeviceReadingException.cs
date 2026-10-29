/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

namespace TeachingDeviceManager.Exceptions;

public class InvalidDeviceReadingException : Exception
{
    public string DeviceId { get; }
    public int InvalidValue { get; }

    public InvalidDeviceReadingException(string deviceId, int invalidValue)
        : base($"Gia tri doc khong hop le cho thiet bi '{deviceId}': {invalidValue}. Gia tri phai nam trong khoang 0 - 100.")
    {
        DeviceId = deviceId;
        InvalidValue = invalidValue;
    }
}