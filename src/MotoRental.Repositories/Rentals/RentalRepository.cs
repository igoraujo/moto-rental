using Dapper;
using Microsoft.Extensions.Configuration;
using MotoRental.Borders.Models;
using MotoRental.Repositories.Base;
using Npgsql;

namespace MotoRental.Repositories.Rentals;

public class RentalRepository(IConfiguration configuration) : BaseRepository<Rental>(configuration), IRentalRepository
{
    public async Task<IEnumerable<Rental>?> FindByDeliveryPersonId(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            return await connection.QueryAsync<Rental>("select * from rental where deliveryPersonId = @id", new { id });
        }
    }
}