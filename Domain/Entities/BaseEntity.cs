
namespace App_QLPK.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; set;}
    public DateTime CreatedAt { get; set;}
    public DateTime? UpdatedAt { get; set;}
}