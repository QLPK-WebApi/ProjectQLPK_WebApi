namespace App_QLPK.Domain.Entities;

public class Specialty : BaseEntity
{
    public string Name { get; set;} = null!;
    public string? Description { get; set;}

    public ICollection<DoctorSpecialty> DoctorSpecialties { get; set;} = new List<DoctorSpecialty>();

}