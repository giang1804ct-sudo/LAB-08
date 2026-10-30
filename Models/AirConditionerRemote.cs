/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

namespace TeachingDeviceManager.Models;

// Thiet bi dieu khien dieu hoa phong hoc (v2 - Da sua loi spam canh bao)
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
        // Fix o ban v2: Chi phat canh bao khi chuyen tu phan hoi sang khong phan hoi (true -> false)
        // Neu da khong phan hoi ma van false thi khong phat sinh lai
        if (IsResponsive && !isResponsive)
        {
            RaiseAlert($"Dieu hoa phong {AssignedRoom} khong phan hoi tin hieu!", AlertSeverity.Critical);
        }

        IsResponsive = isResponsive;
    }
}
