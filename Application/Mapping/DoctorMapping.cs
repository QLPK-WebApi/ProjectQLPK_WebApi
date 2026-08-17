using System.Security.Cryptography;
using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Mapping;

internal static class DoctorMapping
{
    public static DoctorDTO ToDto(this Doctor d) => new()
    {
        Id = d.Id,
        Name = d.User?.FullName ?? string.Empty,
        Email = d.User?.Email,
        Phone = d.User?.Phone,
        Address = d.User?.Address,
        IsActive = d.IsActive,
        CreatedAt = DateTime.UtcNow,
        Specialties = d.DoctorSpecialties.Select(ds => ds.Specialty.Name).ToList(),
    };
}