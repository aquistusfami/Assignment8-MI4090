/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090.Devices;
using Assignment8_MI4090.Enums;
using Assignment8_MI4090.Events;
/// <summary>
/// Lớp trừu tượng thiết bị dạy học
/// </summary>
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

  /// <summary>
  /// method đổi trạng thái thiết bị
  /// </summary>
  /// <param name="newStatus"></param>
  public void ChangeStatus(DeviceStatus newStatus)
  {
    if(newStatus == Status)
    {
      return;
    }

    DeviceStatus oldStatus = Status;
    Status = newStatus;

    OnStatusChanged(new DeviceStatusChangedEventArgs(Id, oldStatus, newStatus, DateTime.Now));
  }

  /// <summary>
  /// phát sinh sự kiện cảnh cáo của thiết bị
  /// </summary>
  /// <param name="message"></param>
  /// <param name="severity"></param>
  protected void RaiseAlert(string message, AlertSeverity severity)
  {
    OnAlertRaised(new DeviceAlertEventArgs(Id, message, severity, DateTime.Now));
  }
  protected virtual void OnStatusChanged(DeviceStatusChangedEventArgs e)
  {
    StatusChanged?.Invoke(this, e);
  }
  protected virtual void OnAlertRaised(DeviceAlertEventArgs e)
  {
    AlertRaised?.Invoke(this, e);
  }
}
