/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment8_MI4090.Exceptions;

public class InvalidDeviceReadingException : Exception
{
  public string DeviceId { get; }
  public int InvalidValue { get; }

  public InvalidDeviceReadingException(string deviceId, int invalidValue) : base($"<!> Thiết bị {deviceId} nhập thời lượng pin {invalidValue} không hợp lệ")
  {
    DeviceId = deviceId;
    InvalidValue = invalidValue;
  }
}
