/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
namespace Assignment8_MI4090.Events;
/// <summary>
/// delegate lọc các sự kiện cảnh cáo 
/// </summary>
/// <param name="alert"></param>
/// <returns></returns>
public delegate bool AlertFilter(DeviceAlertEventArgs alert);
