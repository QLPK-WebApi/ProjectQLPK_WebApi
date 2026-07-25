namespace App_QLPK.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set;} = null!;
    public string FullName { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Alias { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string PasswordHash { get; set; } = null!;
    public bool IsActive { get; set; }
    // dùng để xác định Bác sĩ , nhân viên có còn làm việc hay đã nghỉ 
    public bool IsDeleted { get; set; }
    // đã bị xóa hay chưa? ( không xóa thật trong Database )

    public DateTime? LastLoginAt { get; set; } //  đăng nhập gần nhất
    

    public string? AdditionalData { get; set; }
    

    public Doctor? Doctor { get; set; } 
    public Patient? Patient { get; set;} 
    public Role Role { get; set; } = null!;
}