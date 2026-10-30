/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090.Devices;
using Assignment8_MI4090.Exceptions;

public class ProjectorRemote : TeachingDevice
{
  public int BatteryLevel { get; private set; } = 100;

  public ProjectorRemote(string id, string name) : base(id, name)
  {}

  public void UpdateBatteryLevel(int newLevel)
  {
    if(newLevel < 0 || newLevel > 100) throw new InvalidDeviceReadingException(Id, newLevel);

    int oldBattery = BatteryLevel;
    BatteryLevel = newLevel;

    if(newLevel == 0 && oldBattery != 0)
    {
      RaiseAlert("<!> Điều khiển máy chiếu hết pin", AlertSeverity.Critical);
    }
    else if(newLevel > 0 && newLevel < 20 && (oldBattery >= 20 || oldBattery == 0))
    {
      RaiseAlert("<!> Điều khiển máy chiếu pin dưới 20%", AlertSeverity.Warning);
    }
  }
}
