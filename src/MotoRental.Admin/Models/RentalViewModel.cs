using MotoRental.Borders.Models;

namespace MotoRental.Admin.Models;

public class RentalViewModel
{
    public IEnumerable<Product>? Products { get; set; }
    public Rental? Rental { get; set; }
}