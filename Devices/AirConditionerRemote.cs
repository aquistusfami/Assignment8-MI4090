/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090.Devices;
using Assignment8_MI4090.Enums;
public class AirConditionerRemote : TeachingDevice
{
  public string AssignedRoom { get; }
  public bool IsResponsive { get; private set; } = true;

  public AirConditionerRemote(string id, string name, string assignedRoom) : base(id, name)
  {
    AssignedRoom = assignedRoom;
  }

  public void UpdateResponseState(bool isResponsive)
  {
    if(IsResponsive == isResponsive)
    {
      return;
    }
    
    IsResponsive = isResponsive;

    if(!IsResponsive)
    {
      RaiseAlert("<!> Điều khiển đang không phản hồi",AlertSeverity.Critical);
    }
  }
}
