using Dapper;
using Microsoft.Extensions.Configuration;
using MotoRental.Borders.Models;
using MotoRental.Repositories.Base;
using MotoRental.Repositories.Products;
using Npgsql;

namespace MotoRental.Repositories.Rentals;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(IConfiguration configuration) : base(configuration)
    {
    }
    
}