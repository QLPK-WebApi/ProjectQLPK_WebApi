namespace App_QLPK.Domain.Entities;

/// <summary>
/// Danh sách DV khám bệnh
/// </summary> 
public class Service : BaseEntity
{
    public string Name { get; set;} = null!; // bắt buộc
    public string Code { get; set;} = null!; // không được trùng

    public decimal Price { get; set;}
    public string? Description { get; set;}

    public ICollection<ServiceOrder> ServiceOrders = new List<ServiceOrder>();
}