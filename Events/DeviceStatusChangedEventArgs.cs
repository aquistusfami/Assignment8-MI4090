/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090;
using Assignment8_MI4090.Enums;
public class DeviceStatusChangedEventArgs : EventArgs
{
  public string DeviceId { get; }
  public DeviceStatus OldStatus { get; }
  public DeviceStatus NewStatus { get; }
  public DateTime ChangedAt { get; }

  public DeviceStatusChangedEventArgs(string deviceId, DeviceStatus oldStatus, DeviceStatus newStatus, DateTime changedAt)
  {
    DeviceId = deviceId;
    OldStatus = oldStatus;
    NewStatus = newStatus;
    ChangedAt = changedAt;
  }
}
