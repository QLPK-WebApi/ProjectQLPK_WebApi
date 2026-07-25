using System.ComponentModel.DataAnnotations;


namespace BlazorServer.Models;

public class ApiResponse<T>
{
    public string? Message { get; set; }
    public T? Data { get; set; }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new()!;
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
}


public class LoginRequest
{
    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    public string Password { get; set; } = "";
}

public class LoginResponse
{
    public string Token { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
    public UserInfo User { get; set; } = new();
}


public class UserInfo
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Role { get; set; } = "";
}


public class AppointmentModel
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = "";
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = "";
    public DateTime AppointmentTime { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = "";
    public DateTime? CheckInTime { get; set; }
    public DateTime? CompleteAt { get; set; }
    public string? CancelReason { get; set; }
    public DateTime CreatedAt { get; set; }
}


public class CreateAppointmentModel
{
    [Required]  
    [Range(1, int.MaxValue, ErrorMessage = "PatientId phải > 0")]
    public int PatientId { get; set; }


    [Required]  
    [Range(1, int.MaxValue, ErrorMessage = "DoctorId phải > 0")]
    public int DoctorId { get; set; }

    [Required] 
    public DateTime AppointmentTime { get; set; } = DateTime.Now.AddHours(1);

    [MaxLength(255)] 
    public string? Reason { get; set; }

    [MaxLength(255)] 
    public string? Notes { get; set; }
}