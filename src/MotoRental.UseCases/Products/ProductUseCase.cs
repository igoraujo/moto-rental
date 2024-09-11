using MotoRental.Borders.Models;
using MotoRental.Repositories.Products;
using MotoRental.UseCases.Base;

namespace MotoRental.UseCases.Products;

public class ProductUseCase(IProductRepository repository) : BaseUseCase<Product>(repository), IProductUseCase
{
   
}