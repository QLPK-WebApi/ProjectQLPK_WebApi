namespace App_QLPK.Domain.Entities;


/// <summary>
/// Danh mục thuốc
/// </summary> 
public class Medicine : BaseEntity
{
    
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } // thuốc còn được sử dụng hay không

    public int Stock { get; set; }
    public int MinStock { get; set; }

    public ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
}