using MotoRental.Borders.Models;

namespace MotoRental.Admin.Models;

public class RentalViewModel
{
    public IEnumerable<Product>? Products { get; set; }
    public Product Product { get; set; }
    public Rental? Rental { get; set; }
    public decimal Total { get; set; }
    public DateTime ExpectedEndDate { get; set; }
}