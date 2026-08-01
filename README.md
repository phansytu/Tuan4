# GlobalMiddlewear - MiniBankAPI

## 🚀 Công nghệ sử dụng

* C#
* .NET 8
* ASP.NET Core Web API
* FluentValidation
* ProblemDetails
* Global Exception Handler
* Dependency Injection (DI)
* Swagger / OpenAPI
* In-Memory State

## 📂 Cấu trúc dự án

```text
GlobalMiddlewear/
├── Controllers/
│   └── TransfersController.cs
├── Dto/
│   └── TransferRequest.cs
├── Exceptions/
│   └── BankException.cs
├── Handler/
│   └── GlobalExceptionHandler.cs
├── Models/
│   └── Account.cs
├── Service/
│   └── BankService.cs
├── Validators/
│   └── TransferRequestValidator.cs
├── Program.cs
└── README.md
```

## ⚙️ Cài đặt
### 1. Clone project
```bash
git clone https://github.com/your-repo/Middlewear.git
```
### 2. Di chuyển vào thư mục project
```bash
cd GlobalMiddlewear
```
### 3. Khôi phục package

```bash
dotnet restore
```

### 4. Build project

```bash
dotnet build
```

### 5. Chạy project

```bash
dotnet run
```

## 🔗 API Endpoint

### Chuyển tiền

```http
POST https://localhost:5177/swagger
```

### Request Body

```json
{
  "tuTaikhoan": "9999",
  "DenTaiKhoan": "9998",
  "tienChuyen": 500000,
  "note": "Chuyen tien mung sinh nhat"
}
```

## Dữ liệu tài khoản

Dữ liệu tài khoản được khởi tạo sẵn trong bộ nhớ (**In-Memory State**).

| Số tài khoản | Tên chủ tài khoản |   Số dư ban đầu |
| ------------ | ----------------- | --------------: |
| 9999         | Phan Sy Tu        | 100,000,000 VNĐ |
| 9998         | Anh La Tu         |   1,500,000 VNĐ |

>  Dữ liệu được lưu trong bộ nhớ nên sẽ được khởi tạo lại khi ứng dụng khởi động lại.

## Chức năng

* [x] Chuyển tiền giữa hai tài khoản
* [x] Kiểm tra tài khoản nguồn tồn tại
* [x] Kiểm tra tài khoản đích tồn tại
* [x] Kiểm tra số tiền chuyển lớn hơn 0
* [x] Kiểm tra tài khoản nguồn và tài khoản đích không trùng nhau
* [x] Kiểm tra số dư tài khoản
* [x] Custom Exception
* [x] Global Exception Handler
* [x] Validation với FluentValidation
* [x] Chuẩn hóa lỗi với ProblemDetails
* [x] Dependency Injection
* [x] In-Memory State

## Các trường hợp kiểm thử
### 1. Chuyển tiền thành công
**HTTP Status:**
```text
200 OK
```
**Response:**
```json
{
  "success": true,
  "message": "Chuyển thành công 500,000 VNĐ từ tài khoản 9999 sang 9998."
}
```
### 2. Dữ liệu không hợp lệ
**HTTP Status:**
```text
400 Bad Request
```
**Các lỗi kiểm tra:**
* Số tiền chuyển phải lớn hơn 0.
* Tài khoản nguồn và tài khoản đích không được trùng nhau.
### 3. Số dư không đủ
**HTTP Status:**
```text
400 Bad Request
```
**Response:**
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Số dư không đủ",
  "status": 400,
  "detail": "Tài khoản 9998 không đủ số dư. Số dư hiện tại: 1,500,000 VNĐ, số tiền cần chuyển: 10,000,000 VNĐ.",
  "instance": "/api/Transfers"
}
```
### 4. Tài khoản không tồn tại
**HTTP Status:**

```text
404 Not Found
```

**Response:**

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Tài khoản không tồn tại",
  "status": 404,
  "detail": "Không tìm thấy tài khoản số: 1234",
  "instance": "/api/Transfers"
}
```
## Luồng xử lý
```text
Client
   │
   │ POST /api/Transfers
   ▼
TransfersController
   │
   ▼
FluentValidation
   │
   ├── Validation Error ──► 400 Bad Request
   │
   ▼
BankService
   │
   ├── Account Not Found ──► 404 Not Found
   │
   ├── Insufficient Balance ──► 400 Bad Request
   │
   ▼
Transfer Success
   │
   ▼
200 OK
```
#  BayBay Middleware Pipeline
Dự án giúp minh họa cách hoạt động của:
* Request Pipeline
* Custom Middleware
* `RequestDelegate`
* `_next(context)`
* Middleware Short-Circuiting
* Logging Request
* HTTP Status Code
* `UseMiddleware<T>()`
* `MapGet()`

---

## Công nghệ sử dụng

* C#
* .NET 8
* ASP.NET Core
* Custom Middleware
* RequestDelegate
* HttpContext
* ILogger
* Stopwatch
* Random
* Minimal API

---

## Cấu trúc dự án

```text
AirportSecurityMiddleware/
├── KiemTraVeMiddleware.cs
│   └── Kiểm tra vé máy bay
│
├── KiemTraNguoi.cs
│   └── Kiểm tra hành khách
│
├── RequestLoggingMiddle.cs
│   └── Ghi log Method, URL và thời gian xử lý
│
├── Program.cs
│   └── Cấu hình Request Pipeline
│
└── README.md
```

---

## Cài đặt

### 1. Clone project

```bash
git clone https://github.com/phansytu/Middlewear.git
```
### 2. Di chuyển vào thư mục project

```bash
cd KiemTraBayBay
```
### 3. Khôi phục package

```bash
dotnet restore
```

### 4. Build project

```bash
dotnet build
```
### 5. Chạy project

```bash
dotnet run
```

---

##  API Endpoint

Endpoint mô phỏng hành khách lên máy bay:

```http
GET /boing777
```

Nếu vượt qua tất cả các trạm kiểm tra:

```text
san may tren troi
```

---

##  Request Pipeline

Request được xử lý theo thứ tự:

```text
Client
   │
   │ GET /boing777
   ▼
RequestLoggingMiddle
   │
   │ Ghi nhận Method + URL
   ▼
KiemTraVeMiddleware
   │
   ├── Vé không hợp lệ
   │       │
   │       ▼
   │    401 Unauthorized
   │
   ▼
KiemTraNguoi
   │
   ├── Không vượt qua kiểm tra
   │       │
   │       ▼
   │    403 Forbidden
   │
   ▼
MapGet("/boing777")
   │
   ▼
"san may tren troi"
   │
   ▼
RequestLoggingMiddle
   │
   ▼
Ghi thời gian xử lý
```

---

##  Middleware 1: RequestLoggingMiddle

Middleware này có nhiệm vụ ghi lại thông tin của Request và đo thời gian xử lý.

Các thông tin được lấy:

* HTTP Method
* URL / Path
* Thời gian xử lý Request

Ví dụ:

```csharp
var method = context.Request.Method;
var url = context.Request.Path;

var timer = Stopwatch.StartNew();

try
{
    await _next(context);
}
finally
{
    timer.Stop();

    _logger.LogInformation(
        "Method: {method}, Path: {Path}, Time: {ElapsedMilliseconds}",
        method,
        url,
        timer.ElapsedMilliseconds
    );
}
```

Middleware sử dụng:

```csharp
await _next(context);
```

để chuyển Request sang Middleware tiếp theo.

Việc sử dụng `finally` đảm bảo thời gian xử lý vẫn được ghi nhận ngay cả khi Middleware phía sau xảy ra Exception.

---

##  Middleware 2: KiemTraVeMiddleware

Middleware này mô phỏng nhân viên kiểm tra vé máy bay.

Quy trình:

```text
Nhân viên kiểm tra vé
        │
        ▼
Chờ 2 giây
        │
        ▼
Kiểm tra kết quả
        │
        ▼
Random 1 → 100
        │
        ├── <= 70
        │      │
        │      ▼
        │   Vé hợp lệ
        │      │
        │      ▼
        │   _next(context)
        │
        └── > 70
               │
               ▼
        Vé không hợp lệ
               │
               ▼
        HTTP 401 Unauthorized
```

Logic kiểm tra:

```csharp
int randomNumber = Random.Shared.Next(1, 101);
bool ve = randomNumber <= 70;
```

Có khoảng **70% xác suất vé hợp lệ**.

Nếu vé không hợp lệ:

```csharp
context.Response.StatusCode = 401;
await context.Response.WriteAsync("Tram 1: false");
return;
```

Middleware sử dụng `return` để **dừng Request Pipeline**.

Nếu vé hợp lệ:

```csharp
await _next(context);
```

Request được chuyển sang Middleware tiếp theo.

---

## Middleware 3: KiemTraNguoi

Middleware này mô phỏng bảo vệ kiểm tra hành khách.

Quy trình:

```text
Bảo vệ kiểm tra hành khách
        │
        ▼
Chờ 1 giây
        │
        ▼
Đánh giá kết quả
        │
        ▼
Chờ 1.5 giây
        │
        ▼
Random 1 → 100
        │
        ├── <= 80
        │      │
        │      ▼
        │   An toàn
        │      │
        │      ▼
        │   _next(context)
        │
        └── > 80
               │
               ▼
        Không vượt qua kiểm tra
               │
               ▼
        HTTP 403 Forbidden
```

Logic kiểm tra:

```csharp
int randomNumber = Random.Shared.Next(1, 101);
var soichieu = randomNumber <= 80;
```

Có khoảng **80% xác suất hành khách vượt qua kiểm tra**.

Nếu không vượt qua:

```csharp
context.Response.StatusCode = 403;
return;
```

Request sẽ bị dừng tại Middleware này.

Nếu vượt qua:

```csharp
await _next(context);
```

Request được chuyển đến Endpoint `/boing777`.

---

## RequestDelegate và `_next`

Trong Middleware:

```csharp
private readonly RequestDelegate _next;
```

`RequestDelegate` có thể hiểu đơn giản là một đại diện cho Middleware tiếp theo trong Request Pipeline.

Khi Constructor nhận:

```csharp
public KiemTraVeMiddleware(RequestDelegate next)
{
    _next = next;
}
```

ASP.NET Core sẽ truyền Middleware tiếp theo vào biến `_next`.

Khi gọi:

```csharp
await _next(context);
```

Request được chuyển tiếp sang Middleware tiếp theo.

Ngược lại, nếu Middleware không gọi:

```csharp
await _next(context);
```

thì Request Pipeline sẽ dừng tại Middleware hiện tại.

Ví dụ:

```csharp
if (!ve)
{
    context.Response.StatusCode = 401;
    return;
}
```

Đây được gọi là **Short-Circuit Middleware**.

---

##  Middleware Short-Circuit

Pipeline có thể bị dừng ở bất kỳ Middleware nào.

Ví dụ:

```text
Request
   │
   ▼
Logging
   │
   ▼
Kiểm tra vé
   │
   ├── Không hợp lệ
   │      │
   │      ▼
   │   HTTP 401
   │      │
   │      ▼
   │   STOP ❌
   │
   ▼
Kiểm tra người
   │
   ▼
Endpoint
```

Nếu vé không hợp lệ, Middleware `KiemTraNguoi` và Endpoint sẽ **không được thực thi**.

---

## ⚙️ Cấu hình Middleware Pipeline

Trong `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePages();

app.UseMiddleware<RequestLoggingMiddle>();

app.UseMiddleware<KiemTraVeMiddleware>();

app.UseMiddleware<KiemTraNguoi>();

app.MapGet("/boing777", () => "san may tren troi");

app.Run();
```

Thứ tự đăng ký Middleware rất quan trọng.

Pipeline được thực thi theo thứ tự:

```text
RequestLoggingMiddle
        ↓
KiemTraVeMiddleware
        ↓
KiemTraNguoi
        ↓
/boing777
```

Các Middleware được đăng ký trước sẽ chạy trước khi Request đi vào Endpoint.

Sau khi Endpoint xử lý xong, Request có thể quay ngược trở lại các Middleware phía trước.

---

## 📊 Các trường hợp xử lý

| Trường hợp                          | Kết quả                        |           HTTP Status |
| ----------------------------------- | ------------------------------ | --------------------: |
| Vé không hợp lệ                     | Dừng tại `KiemTraVeMiddleware` |                   401 |
| Vé hợp lệ, kiểm tra người không đạt | Dừng tại `KiemTraNguoi`        |                   403 |
| Vé hợp lệ, kiểm tra người đạt       | Cho phép vào máy bay           |                   200 |
| Có Exception                        | Xử lý bởi Exception Handler    | 500 hoặc mã tương ứng |

---

## ✨ Chức năng

* [x] Custom Middleware
* [x] Request Pipeline
* [x] Kiểm tra vé máy bay
* [x] Kiểm tra hành khách
* [x] Random kết quả kiểm tra
* [x] Middleware Short-Circuit
* [x] Sử dụng `RequestDelegate`
* [x] Sử dụng `_next(context)`
* [x] Ghi log HTTP Method
* [x] Ghi log Request Path
* [x] Đo thời gian xử lý Request
* [x] Sử dụng `ILogger`
* [x] Sử dụng `Stopwatch`
* [x] Xử lý HTTP Status Code

---
Đây là dự án mô phỏng nhằm mục đích học tập về **ASP.NET Core Middleware và Request Pipeline**.

Kết quả kiểm tra vé và hành khách được tạo ngẫu nhiên bằng:

```csharp
Random.Shared.Next(1, 101);
```

Do đó, mỗi lần gửi Request có thể nhận được kết quả khác nhau.

Dự án có thể được mở rộng để áp dụng Middleware vào các tình huống thực tế như:

* Logging Request/Response
* Authentication
* Authorization
* Kiểm tra API Key
* Kiểm tra quyền truy cập
* Global Exception Handling
* Đo hiệu năng API
* Rate Limiting
* Request Validation

---


