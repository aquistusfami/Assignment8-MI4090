/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090.Subscribers;
using Assignment8_MI4090.Enums;
using Assignment8_MI4090.Events;

public class MaintenanceService
{
  private readonly List<string> requests = new();
  public IReadOnlyList<string> Requests => requests.AsReadOnly();

  public void HandleCriticalAlert(object? sender, DeviceAlertEventArgs e)
  {
    if (e.Severity != AlertSeverity.Critical)
    {
      return;
    }

    requests.Add(
      $"<!> Yêu cầu bảo trì cho {e.DeviceId}: {e.Message}"
    );
  }
}
