using Dapper.Contrib.Extensions;

namespace MotoRental.Borders.Models;

[Table("motorcycle")]
public class Motorcycle
{
    [Key]
    public int Id { get; set; }
    public string PlateNumber { get; set; }
    public int Year { get; set; }
    public string Model { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}