namespace App_QLPK.Application.DTO;

public class CreateDoctorDTO
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;


    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? LicenseNumber { get; set; }
    public string? Biography { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<string> SpecialtyNames { get; set; } = new();
    public string? SpecialtyDescription { get; set; }


}