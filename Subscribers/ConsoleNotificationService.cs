/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090.Subscribers;
using Assignment8_MI4090.Events;
/// <summary>
/// Lớp dịch vụ thông báo trên console
/// </summary>
public class ConsoleNotificationService
{
  public void HandleAlert(object? sender, DeviceAlertEventArgs e)
  {
    Console.WriteLine($"[{e.Severity}] {e.DeviceId}: {e.Message}");
  }
}
