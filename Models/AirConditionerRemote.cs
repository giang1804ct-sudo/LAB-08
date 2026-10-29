/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

namespace TeachingDeviceManager.Models;

public class AirConditionerRemote : TeachingDevice
{
    public string AssignedRoom { get; }
    public bool IsResponsive { get; private set; }

    public AirConditionerRemote(string id, string name, string assignedRoom, bool isResponsive = true)
        : base(id, name)
    {
        AssignedRoom = assignedRoom;
        IsResponsive = isResponsive;
    }

    public void UpdateResponseState(bool isResponsive)
    {
        // TODO: Cứ truyền false là báo lỗi (chưa chặn lặp nếu đã false trước đó)
        if (!isResponsive)
        {
            RaiseAlert($"Dieu hoa phong {AssignedRoom} khong phan hoi tin hieu!", AlertSeverity.Critical);
        }
        IsResponsive = isResponsive;
    }
}