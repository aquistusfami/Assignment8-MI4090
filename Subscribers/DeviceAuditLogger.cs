/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using Assignment8_MI4090.Enums;
using Assignment8_MI4090.Events;
namespace Assignment8_MI4090.Subscribers;
/// <summary>
/// Lớp ghi sự thay đổi của thiết bị
/// </summary>
public class DeviceAuditLogger
{
  private readonly List<string> entries = new();

  public IReadOnlyList<string> Entries => entries.AsReadOnly();

  /// <summary>
  /// Phương thức xử lý trạng thái thay đổi
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void HandleStatusChanged(object? sender, DeviceStatusChangedEventArgs e)
  {
    entries.Add(
      $"{e.ChangedAt}: {e.DeviceId}, " +
      $"{e.OldStatus} -> {e.NewStatus}"
    );
  }
}
