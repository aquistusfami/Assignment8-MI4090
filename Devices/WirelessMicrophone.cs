/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090.Devices;
using Assignment8_MI4090.Exceptions;
using Assignment8_MI4090.Enums;

public class WirelessMicrophone : TeachingDevice
{
  public int BatteryLevel { get; private set; } = 100;
  public int SignalQuality { get; private set; } = 100;

  public WirelessMicrophone(string id, string name) : base(id, name)
  {}
  public void UpdateBatteryLevel(int newLevel)
  {
    if(newLevel < 0 || newLevel > 100) throw new InvalidDeviceReadingException(Id, newLevel);

    int oldBattery = BatteryLevel;
    BatteryLevel = newLevel;

    if(newLevel == 0 && oldBattery != 0)
    {
      RaiseAlert("<!> Micro không dây hết pin", AlertSeverity.Critical);
    }
    else if(newLevel > 0 && newLevel < 30 && (oldBattery >= 30 || oldBattery == 0))
    {
      RaiseAlert("<!> Micro không dây pin dưới 30%", AlertSeverity.Warning);
    }
  }

  public void UpdateSignalQuality(int newQuality)
  {
    if(newQuality < 0 || newQuality > 100) throw new InvalidDeviceReadingException(Id, newQuality);

    int oldSignal = SignalQuality;
    SignalQuality = newQuality;

    if(newQuality == 0 && oldSignal != 0)
    {
      RaiseAlert("<!> Micro không dây không thể phát tín hiệu", AlertSeverity.Critical);
    }
    else if(newQuality > 0 && newQuality < 40 && (oldSignal >= 40 || oldSignal == 0))
    {
      RaiseAlert("<!> Micro không dây có chất lượng tín hiệu dưới 40%", AlertSeverity.Warning);
    }
  }
}
