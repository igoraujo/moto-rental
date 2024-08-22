using Dapper.Contrib.Extensions;

namespace MotoRental.Borders.Models;

public class Rental
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public string LicenseType { get; set; }
    public decimal TotalValue { get; set; }
    [ExplicitKey]
    public int ProductId { get; set; }
    [ExplicitKey]
    public int DeliveryPersonId { get; set; }
    
}