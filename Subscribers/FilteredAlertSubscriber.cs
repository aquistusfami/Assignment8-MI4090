/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090.Subscribers;
using Assignment8_MI4090.Enums;
using Assignment8_MI4090.Events;

public class FilteredAlertSubscriber
{
  private readonly AlertFilter filter;
  private readonly Action<DeviceAlertEventArgs> action;

  public FilteredAlertSubscriber(AlertFilter filter, Action<DeviceAlertEventArgs> action)
  {
    this.filter = filter;
    this.action = action;
  }

  public void HandleAlert(object? sender, DeviceAlertEventArgs e)
  {
    if(filter(e))
    {
      action(e);
    }
  }
}
