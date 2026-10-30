/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using Assignment8_MI4090.Enums;
using Assignment8_MI4090.Subscribers;
using Assignment8_MI4090.Devices;
using Assignment8_MI4090.Exceptions;
using Assignment8_MI4090.Coordination;

namespace Assignment8_MI4090;

class Program
{
    static void Main(string[] args)
    {
        // TC1: Đổi trạng thái và ghi audit.
        try
        {
            var device = new ProjectorRemote("PR001", "Máy chiếu Casio");
            var logger = new DeviceAuditLogger();
            device.StatusChanged += logger.HandleStatusChanged;

            device.ChangeStatus(DeviceStatus.Borrowed);

            if (device.Status != DeviceStatus.Borrowed || logger.Entries.Count != 1)
                throw new Exception("Trạng thái hoặc audit không đúng.");

            Console.WriteLine("PASS TC1");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL TC1: {ex.Message}");
        }

        // TC2: Giữ nguyên trạng thái không phát event.
        try
        {
            var device = new ProjectorRemote("PR001", "Máy chiếu Casio");
            int count = 0;
            device.StatusChanged += (sender, e) => count++;

            device.ChangeStatus(DeviceStatus.Available);

            if (count != 0)
                throw new Exception("Phát event dù trạng thái không đổi.");

            Console.WriteLine("PASS TC2");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL TC2: {ex.Message}");
        }

        // TC3: Pin máy chiếu cảnh báo đúng, không lặp.
        try
        {
            var device = new ProjectorRemote("PR001", "Máy chiếu Casio");
            var alerts = new List<AlertSeverity>();
            device.AlertRaised += (sender, e) => alerts.Add(e.Severity);

            device.UpdateBatteryLevel(19);
            device.UpdateBatteryLevel(18);
            device.UpdateBatteryLevel(0);
            device.UpdateBatteryLevel(0);

            if (!alerts.SequenceEqual(new[]
                { AlertSeverity.Warning, AlertSeverity.Critical }))
                throw new Exception("Sai mức độ hoặc số lần cảnh báo.");

            Console.WriteLine("PASS TC3");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL TC3: {ex.Message}");
        }

        // TC4: Pin và tín hiệu micro.
        try
        {
            var device = new WirelessMicrophone("WM001", "Micro không dây");
            var alerts = new List<AlertSeverity>();
            device.AlertRaised += (sender, e) => alerts.Add(e.Severity);

            device.UpdateBatteryLevel(29);
            device.UpdateSignalQuality(39);
            device.UpdateSignalQuality(0);

            if (!alerts.SequenceEqual(new[]
                {
                    AlertSeverity.Warning,
                    AlertSeverity.Warning,
                    AlertSeverity.Critical
                }))
                throw new Exception("Cảnh báo micro không đúng.");

            Console.WriteLine("PASS TC4");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL TC4: {ex.Message}");
        }

        // TC5: Điều hòa không cảnh báo lặp khi vẫn mất phản hồi.
        try
        {
            var device = new AirConditionerRemote("AC001", "Điều hòa Daikin", "D5-102");
            var alerts = new List<AlertSeverity>();
            device.AlertRaised += (sender, e) => alerts.Add(e.Severity);

            device.UpdateResponseState(false);
            device.UpdateResponseState(false);
            device.UpdateResponseState(true);

            if (!device.IsResponsive
                || !alerts.SequenceEqual(new[] { AlertSeverity.Critical }))
                throw new Exception("Phải phục hồi và chỉ cảnh báo một lần.");

            Console.WriteLine("PASS TC5");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL TC5: {ex.Message}");
        }

        // TC6: Pin ngoài khoảng phải ném đúng ngoại lệ.
        try
        {
            var device = new ProjectorRemote("PR001", "Máy chiếu Casio");
            bool caught = false;

            try
            {
                device.UpdateBatteryLevel(-1);
            }
            catch (InvalidDeviceReadingException ex)
            {
                caught = ex.DeviceId == "PR001" && ex.InvalidValue == -1;
            }

            if (!caught || device.BatteryLevel != 100)
                throw new Exception("Thiếu ngoại lệ đúng hoặc pin đã bị đổi.");

            Console.WriteLine("PASS TC6");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL TC6: {ex.Message}");
        }

        // TC7: Đăng ký không trùng và hủy đúng handler.
        try
        {
            var device = new ProjectorRemote("PR001", "Máy chiếu Casio");
            var maintenance = new MaintenanceService();

            using var coordinator = new DeviceMonitoringCoordinator(
                new[] { device },
                new ConsoleNotificationService(),
                new DeviceAuditLogger(),
                maintenance);

            coordinator.Subscribe();
            coordinator.Subscribe();
            device.UpdateBatteryLevel(0);

            if (maintenance.Requests.Count != 1)
                throw new Exception("Đăng ký bị thiếu hoặc trùng.");

            coordinator.Unsubscribe();
            device.UpdateBatteryLevel(100);
            device.UpdateBatteryLevel(0);

            if (maintenance.Requests.Count != 1)
                throw new Exception("Vẫn nhận cảnh báo sau khi hủy.");

            Console.WriteLine("PASS TC7");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL TC7: {ex.Message}");
        }
        // TC8: Delegate chỉ xử lý Critical
        try
        {
            var device = new ProjectorRemote("PR001", "Máy chiếu Casio");
            int count = 0;

            var subscriber = new FilteredAlertSubscriber(
                alert => alert.Severity == AlertSeverity.Critical,
                alert => count++);

            device.AlertRaised += subscriber.HandleAlert;

            device.UpdateBatteryLevel(19); // Warning: bỏ qua
            if (count != 0)
                throw new Exception("Bộ lọc xử lý cả Warning.");

            device.UpdateBatteryLevel(0); // Critical: xử lý
            if (count != 1)
                throw new Exception("Bộ lọc không xử lý đúng Critical.");

            Console.WriteLine("PASS TC8");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL TC8: {ex.Message}");
        }

        // TC9: Dispose tự hủy đăng ký
        try
        {
            var device = new ProjectorRemote("PR001", "Máy chiếu Casio");
            var logger = new DeviceAuditLogger();
            var maintenance = new MaintenanceService();

            using (var coordinator = new DeviceMonitoringCoordinator(
                new[] { device },
                new ConsoleNotificationService(),
                logger,
                maintenance))
            {
                coordinator.Subscribe();
                device.ChangeStatus(DeviceStatus.Borrowed);
                device.UpdateBatteryLevel(0);

                if (logger.Entries.Count != 1 || maintenance.Requests.Count != 1)
                    throw new Exception("Đăng ký chưa hoạt động.");
            }

            device.ChangeStatus(DeviceStatus.Available);
            device.UpdateBatteryLevel(100);
            device.UpdateBatteryLevel(0);

            if (logger.Entries.Count != 1 || maintenance.Requests.Count != 1)
                throw new Exception("Sau Dispose vẫn ghi log hoặc tạo bảo trì.");

            Console.WriteLine("PASS TC9");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL TC9: {ex.Message}");
        }
    }
}
