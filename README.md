# Mã nguồn bài tập tuần 8
### 1. Vì sao thiết bị không nên gọi trực tiếp MaintenanceService 
Vì bản chất là thiết bị tức TeachingDevice nó là 1 lớp trừu tượng chỉ phục vụ cho mục đích thiết kế, cần phải tách bạch rõ trách nhiệm là thiết bị chỉ phát hiện và thông báo sự cố đến `MaintenanceService` rồi nó mới tạo quyết định bảo trì. Các subscriber sẽ xử lý các công việc đó.

### 2. Khi nào cần một event riêng, khi nào có thể dùng chung một event? 
Cần event riêng khi các sự kiện khác nhau về ý nghĩa nghiệp vụ hoặc dữ liệu cần cung cấp. Có thể dùng chung event khi chúng cùng bản chất và cấu trúc dữ liệu, dù phát sinh ở các lớp khác nhau.

### 3. Thành phần nào có quyền phát sinh event?
`TeachingDevice` khai báo và gọi Invoke cho `StatusChanged`, `AlertRaised`. Các lớp dẫn xuất phát sinh event thông qua phương thức protected là `RaiseAlert` hay `OnAlertRaised`...
