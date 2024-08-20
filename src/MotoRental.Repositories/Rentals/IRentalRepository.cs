using MotoRental.Borders.Models;
using MotoRental.Repositories.Base;

namespace MotoRental.Repositories.Rentals;

public interface IRentalRepository : IBaseRepository<Rental>
{
    Task<IEnumerable<Rental>?> FindByDeliveryPersonId(int id);
}