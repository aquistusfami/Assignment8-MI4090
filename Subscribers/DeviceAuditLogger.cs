/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using Assignment8_MI4090.Enums;
using Assignment8_MI4090.Events;
namespace Assignment8_MI4090.Subscribers;

public class DeviceAuditLogger
{
  private readonly List<string> entries = new();

  private IReadOnlyList<string> Entries => entries.AsReadOnly();

  public void HandleStatusChanged(object? sender, DeviceStatusChangedEventArgs e)
  {
    entries.Add(
      $"{e.ChangedAt}: {e.DeviceId}, " +
      $"{e.OldStatus} -> {e.NewStatus}"
    );
  }
}
