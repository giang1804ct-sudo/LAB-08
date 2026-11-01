/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

using TeachingDeviceManager.Exceptions;

namespace TeachingDeviceManager.Models;

// Thiet bi dieu khien may chieu (v3 - Hoan thien validate ngoai le va phan loai canh bao)
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
        // Fix o ban v3: Kiem tra pin hop le tu 0 den 100, neu sai thi nem ngoai le
        if (newLevel < 0 || newLevel > 100)
        {
            throw new InvalidDeviceReadingException(Id, newLevel);
        }

        int oldLevel = BatteryLevel;
        BatteryLevel = newLevel;

        // Kiem tra phat sinh canh bao
        if (newLevel == 0)
        {
            RaiseAlert("Remote may chieu da can kiet pin!", AlertSeverity.Critical);
        }
        else if (newLevel < 20)
        {
            // Chi phat canh bao neu truoc do chua o muc yeu, hoac muc pin tiep tuc giam xuong
            if (oldLevel >= 20 || newLevel < oldLevel)
            {
                RaiseAlert($"Remote may chieu pin yeu: {newLevel}%", AlertSeverity.Warning);
            }
        }
    }
}
