/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using Assignment8_MI4090.Devices;
using Assignment8_MI4090.Subscribers;

namespace Assignment8_MI4090.Coordination;

/// <summary>
/// lớp quản lý xử lý các phát sinh sự kiện thiết bị
/// </summary>
public class DeviceMonitoringCoordinator : IDisposable
{
  private readonly List<TeachingDevice> devices;
  private readonly ConsoleNotificationService notificationService;
  private readonly DeviceAuditLogger auditLogger;
  private readonly MaintenanceService maintenanceService;
  private bool isSubcribed;

  public DeviceMonitoringCoordinator(
      IEnumerable<TeachingDevice> devices,
      ConsoleNotificationService notificationService,
      DeviceAuditLogger auditLogger,
      MaintenanceService maintenanceService
  )
  {
    this.devices = devices.Distinct().ToList();
    this.notificationService = notificationService;
    this.auditLogger = auditLogger;
    this.maintenanceService = maintenanceService;
  }

  public void Subscribe()
  {
    if(isSubcribed)
    {
      return;
    }

    foreach (TeachingDevice dev in devices)
    {
      dev.StatusChanged += auditLogger.HandleStatusChanged;
      dev.AlertRaised += notificationService.HandleAlert;
      dev.AlertRaised += maintenanceService.HandleCriticalAlert;
    }

    isSubcribed = true;
  }

  public void Unsubscribe()
  {
    if(!isSubcribed)
    {
      return;
    }

    foreach (TeachingDevice dev in devices)
    {
      dev.StatusChanged -= auditLogger.HandleStatusChanged;
      dev.AlertRaised -= notificationService.HandleAlert;
      dev.AlertRaised -= maintenanceService.HandleCriticalAlert;
    }

    isSubcribed = false;
  }

  public void Dispose()
  {
    Unsubscribe();
  }
}
