/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090;

public abstract class TeachingDevice
{
  public string Id { get; }
  public string Name { get; }
  public DeviceStatus Status { get; private set; }
  public event EventHandler<DeviceStatusChangedEventArgs>? StatusChanged;
  public event EventHandler<DeviceAlertEventArgs>? AlertRaised;

  protected TeachingDevice(string id, string name)
  {
    Id = id;
    Name = name;
    Status = DeviceStatus.Available;
  }
  public void ChangeStatus(DeviceStatus newStatus)
  {
    
  }
}
