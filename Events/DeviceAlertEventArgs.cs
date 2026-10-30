/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090;
using Assignment8_MI4090.Enums;
public class DeviceAlertEventArgs : EventArgs
{
  public string DeviceId { get; }
  public string Message { get; }
  public AlertSeverity Severity { get; }
  public DateTime OccurredAt { get; }

  public DeviceAlertEventArgs(string deviceId, string message, AlertSeverity severity, DateTime occurredAt)
  {
    DeviceId = deviceId;
    Message = message;
    Severity = severity;
    OccurredAt = occurredAt;
  }
}
