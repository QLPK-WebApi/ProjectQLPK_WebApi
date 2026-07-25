namespace App_QLPK.Domain.Enums;

public enum AppointmentStatus
{
    Scheduled,  // Đã đặt lịch
    CheckedIn,    // Đã đến 
    Completed,   // Đã khám xong
    Cancelled,  // Đã hủy
}