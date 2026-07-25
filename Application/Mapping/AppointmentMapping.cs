
using App_QLPK.Application.DTO.Appointments;
using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Mapping;

internal static class AppointmentMapping
{
    public static AppointmentDTO ToDto(this Appointment a) => new()
    {
        Id = a.Id,
        PatientId = a.PatientId,
        PatientName = a.Patient?.User?.FullName!,
        DoctorId = a.DoctorId,
        DoctorName = a.Doctor?.User?.FullName!,
        AppointmentTime = a.AppointmentTime,
        Reason = a.Reason,
        Notes = a.Notes,
        Status = a.Status.ToString(),
        CheckInTime = a.CheckInTime,
        CompleteAt = a.CompleteAt,
        CancelReason = a.CancelReason,
        CreatedAt = a.CreatedAt,
    };
}

