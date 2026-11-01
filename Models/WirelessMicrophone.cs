/****************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/****************/

using TeachingDeviceManager.Exceptions;

namespace TeachingDeviceManager.Models;

// Thiet bi micro khong day (v3 - Hoan thien validate ngoai le va phan loai canh bao day du)
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
        // Fix o ban v3: Validate gia tri pin phai nam trong khoang 0 - 100
        if (newLevel < 0 || newLevel > 100)
        {
            throw new InvalidDeviceReadingException(Id, newLevel);
        }

        BatteryLevel = newLevel;

        // Quy tac: Pin bang 0 la Critical, duoi 30% la Warning
        if (newLevel == 0)
        {
            RaiseAlert($"Micro '{Name}' da can sach pin!", AlertSeverity.Critical);
        }
        else if (newLevel < 30)
        {
            RaiseAlert($"Micro '{Name}' pin yeu ({newLevel}%)", AlertSeverity.Warning);
        }
    }

    public void UpdateSignalQuality(int newQuality)
    {
        // Fix o ban v3: Validate gia tri tin hieu phai nam trong khoang 0 - 100
        if (newQuality < 0 || newQuality > 100)
        {
            throw new InvalidDeviceReadingException(Id, newQuality);
        }

        SignalQuality = newQuality;

        // Quy tac: Tin hieu bang 0 la Critical, duoi 40% la Warning
        if (newQuality == 0)
        {
            RaiseAlert($"Micro '{Name}' mat hoan toan tin hieu!", AlertSeverity.Critical);
        }
        else if (newQuality < 40)
        {
            RaiseAlert($"Micro '{Name}' tin hieu kem ({newQuality}%)", AlertSeverity.Warning);
        }
    }
}
