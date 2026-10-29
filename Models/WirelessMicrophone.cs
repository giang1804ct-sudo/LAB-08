/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

namespace TeachingDeviceManager.Models;

public class WirelessMicrophone : TeachingDevice
{
    public int BatteryLevel { get; private set; }
    public int SignalQuality { get; private set; }

    public WirelessMicrophone(string id, string name, int initialBattery = 100, int initialSignal = 100)
        : base(id, name)
    {
        BatteryLevel = initialBattery;
        SignalQuality = initialSignal;
    }

    public void UpdateBatteryLevel(int newLevel)
    {
        // TODO: Chua kiem tra hop le 0-100
        BatteryLevel = newLevel;
        if (newLevel == 0)
            RaiseAlert($"Micro {Name} da can sach pin!", AlertSeverity.Critical);
        else if (newLevel < 30)
            RaiseAlert($"Micro {Name} pin yeu ({newLevel}%)", AlertSeverity.Warning);
    }

    public void UpdateSignalQuality(int newQuality)
    {
        // TODO: Chua kiem tra hop le 0-100
        SignalQuality = newQuality;
        if (newQuality == 0)
            RaiseAlert($"Micro {Name} mat hoan toan tin hieu!", AlertSeverity.Critical);
        else if (newQuality < 40)
            RaiseAlert($"Micro {Name} tin hieu kem ({newQuality}%)", AlertSeverity.Warning);
    }
}