/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090.Devices;
using Assignment8_MI4090.Exceptions;
using Assignment8_MI4090.Enums;

/// <summary>
/// Lớp mic không dây kế thừa TeachingDevice
/// </summary>
public class WirelessMicrophone : TeachingDevice
{
  public int BatteryLevel { get; private set; } = 100;
  public int SignalQuality { get; private set; } = 100;

  public WirelessMicrophone(string id, string name) : base(id, name)
  {}

  /// <summary>
  /// Phương thức cập nhật phần trăm pin
  /// </summary>
  /// <param name="newLevel"></param>
  public void UpdateBatteryLevel(int newLevel)
  {
    // ngưỡng không hợp lệ
    if(newLevel < 0 || newLevel > 100) throw new InvalidDeviceReadingException(Id, newLevel);

    int oldBattery = BatteryLevel;
    BatteryLevel = newLevel;

    // pin mới giảm về 0
    if(newLevel == 0 && oldBattery != 0)
    {
      RaiseAlert("<!> Micro không dây hết pin", AlertSeverity.Critical);
    }
    // Pin < 30% với điều kiện trước đó pin trên 30% hoặc đang hết pin
    else if(newLevel > 0 && newLevel < 30 && (oldBattery >= 30 || oldBattery == 0))
    {
      RaiseAlert("<!> Micro không dây pin dưới 30%", AlertSeverity.Warning);
    }
  }

  /// <summary>
  /// Phương thức cập nhật tín hiệu mic
  /// </summary>
  /// <param name="newQuality"></param>
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
