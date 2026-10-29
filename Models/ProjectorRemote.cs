/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

namespace TeachingDeviceManager.Models;

public class ProjectorRemote : TeachingDevice
{
    public int BatteryLevel { get; private set; }

    public ProjectorRemote(string id, string name, int initialBattery = 100) : base(id, name)
    {
        BatteryLevel = initialBattery;
    }

    public void UpdateBatteryLevel(int newLevel)
    {
        // TODO: can kiem tra pin hop le tu 0 den 100 khong? Tam thoi gan luon
        BatteryLevel = newLevel;

        // Lỗi sinh viên: Để < 20 trước nên 0% bị rơi vào đây luôn
        if (newLevel < 20)
        {
            RaiseAlert($"Remote may chieu pin yeu: {newLevel}%", AlertSeverity.Warning);
        }
        else if (newLevel == 0)
        {
            RaiseAlert("Remote may chieu da can kiet pin!", AlertSeverity.Critical);
        }
    }
}