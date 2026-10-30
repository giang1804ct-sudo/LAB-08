/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

namespace TeachingDeviceManager.Models;

// Thiet bi dieu khien may chieu (v2 - Da sua loi thu tu 0% va < 20%)
public class ProjectorRemote : TeachingDevice
{
    public int BatteryLevel { get; private set; }

    public ProjectorRemote(string id, string name, int initialBattery = 100)
        : base(id, name)
    {
        BatteryLevel = initialBattery;
    }

    public void UpdateBatteryLevel(int newLevel)
    {
        // TODO: van chua kiem tra ngoai le newLevel < 0 hoac > 100 o ban v2, dot sau sua tiep
        BatteryLevel = newLevel;

        // Fix o ban v2: Kiem tra 0% truoc de khong bi dinh vao nhanh < 20%
        if (newLevel == 0)
        {
            RaiseAlert("Remote may chieu da can kiet pin!", AlertSeverity.Critical);
        }
        else if (newLevel < 20)
        {
            RaiseAlert($"Remote may chieu pin yeu: {newLevel}%", AlertSeverity.Warning);
        }
    }
}