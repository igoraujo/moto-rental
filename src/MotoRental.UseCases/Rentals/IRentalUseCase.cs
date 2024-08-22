using MotoRental.Borders.Models;
using MotoRental.Repositories.Base;
using MotoRental.UseCases.Base;

namespace MotoRental.UseCases.Rentals;

public interface IRentalUseCase : IBaseUseCase<Rental>
{
    Task<IEnumerable<Rental>?> FindByDeliveryPersonId(int id);
    Task<int> Save(Product product, DeliveryPerson deliveryPerson);
}