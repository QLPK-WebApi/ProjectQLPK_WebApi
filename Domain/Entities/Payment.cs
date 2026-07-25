using App_QLPK.Domain.Enums;

namespace App_QLPK.Domain.Entities;

public class Payment : BaseEntity
{
    public int InvoiceId { get; set;}
    
    public PaymentMethod Method { get; set;}
    public PaymentStatus Status { get; set;}

    public decimal Amount { get; set;}
    public DateTime PaymentTime { get; set;}
    

    public Invoice Invoice { get; set;} = null!;
}