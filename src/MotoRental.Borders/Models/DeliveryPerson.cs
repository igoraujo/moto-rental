using Dapper.Contrib.Extensions;

namespace MotoRental.Borders.Models;

[Table ("deliveryPerson")]
public class DeliveryPerson
{
    private int _id;
    private string _name;
    private string _cnpj;
    private DateTime _dateOfBirth;
    private string _driverLicense;
    private LicenseType _licenseType;
    private DateTime _ceatedAt;
    private DateTime? _updatedAt;
    private bool _active;
    
    [Key]
    public int Id { get => _id;set => _id = value; }
    public string Name {get => _name; set => _name = value ?? throw new ArgumentNullException(nameof(value)); }
    public string Cnpj { get => _cnpj; set => _cnpj = value ?? throw new ArgumentNullException(nameof(value)); }
    public DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
    public string DriverLicense { get => _driverLicense; set => _driverLicense = value ?? throw new ArgumentNullException(nameof(value)); }
    public LicenseType LicenseType { get => _licenseType; set => _licenseType = value; }
    public DateTime CeatedAt { get => _ceatedAt; set => _ceatedAt = value; }
    public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
    public bool Active { get => _active; set => _active = value; }
}