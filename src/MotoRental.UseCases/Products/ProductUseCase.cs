using MotoRental.Borders.Models;
using MotoRental.Repositories.Products;
using MotoRental.UseCases.Base;

namespace MotoRental.UseCases.Products;

public class ProductUseCase(IProductRepository repository) : BaseUseCase<Product>(repository), IProductUseCase
{
    public override async Task<IEnumerable<Product>?> FindAll()
    {
        return new List<Product>
        {
            new()
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                Description = "7 dias com um custo de R$30,00 por dia",
                IsLateFee = true,
                LateFee = 20,
                IsPercent = true,
                NumberOfDays = 7,
                PricePerDay = 30M,
                Active = true
            },
            new()
            {
                Id = 2,
                CreatedAt = DateTime.Now,
                Description = "15 dias com um custo de R$28,00 por dia",
                IsLateFee = true,
                LateFee = 40,
                IsPercent = true,
                NumberOfDays = 15,
                PricePerDay = 28M,
                Active = true
            },
            new()
            {
                Id = 3,
                CreatedAt = DateTime.Now,
                Description = "30 dias com um custo de R$22,00 por dia",
                IsLateFee = true,
                LateFee = 50,
                IsPercent = false,
                NumberOfDays = 30,
                PricePerDay = 22M,
                Active = true
            }
        };
    }
}