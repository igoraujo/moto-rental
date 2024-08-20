using Dapper.Contrib.Extensions;

namespace MotoRental.Borders.Models;

public class Product
{
    private int _id;
    private string _description;
    private int _numberOfDays;
    private decimal _pricePerDay;
    private decimal _lateFee;
    private bool isLateFee;
    private bool isPercent;
    private DateTime _createdAt;
    private DateTime? _updatedAt;
    private bool _active;

    [Key]
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int NumberOfDays
    {
        get => _numberOfDays;
        set => _numberOfDays = value;
    }

    public decimal PricePerDay
    {
        get => _pricePerDay;
        set => _pricePerDay = value;
    }

    public decimal LateFee
    {
        get => _lateFee;
        set => _lateFee = value;
    }

    public bool IsLateFee
    {
        get => isLateFee;
        set => isLateFee = value;
    }

    public bool IsPercent
    {
        get => isPercent;
        set => isPercent = value;
    }

    public DateTime CreatedAt
    {
        get => _createdAt;
        set => _createdAt = value;
    }

    public DateTime? UpdatedAt
    {
        get => _updatedAt;
        set => _updatedAt = value;
    }

    public bool Active
    {
        get => _active;
        set => _active = value;
    }
}