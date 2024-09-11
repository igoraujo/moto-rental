using System.ComponentModel.DataAnnotations;
using MotoRental.Borders.Models;
using MotoRental.Repositories.Rentals;
using MotoRental.UseCases.Base;

namespace MotoRental.UseCases.Rentals;

public class RentalUseCase(IRentalRepository repository) : BaseUseCase<Rental>(repository), IRentalUseCase
{
    public async Task<IEnumerable<Rental>?> FindByDeliveryPersonId(int id)
    {
        return await repository.FindByDeliveryPersonId(id);
    }

    public async Task<int> Save(Product product, DeliveryPerson deliveryPerson)
    {
        if (deliveryPerson.LicenseType != LicenseType.A)
        {
            throw new ValidationException("The driver's license is not type A, and therefore it is not possible to make the reservation");
        }
        
        const int startedAtOneDay = 1;
        var date = DateTime.Now.AddDays(startedAtOneDay);

        ///TODO isto tem que vir como uma parametro de decisao
        // if (product.IsLateFee)
        // {
        //     product.PricePerDay = product.IsPercent ? Multiply(product) : Sum(product);
        // }
        
        decimal totalPrice = product.NumberOfDays * product.PricePerDay;
        var endDate = date.AddDays(product.NumberOfDays);
        
        var rental = new Rental
        {
            CreatedAt = DateTime.Now,
            StartDate = date,
            EndDate = endDate,
            Total = totalPrice,
            LicenseType = nameof(deliveryPerson.LicenseType),
            ProductId = product.Id,
            DeliveryPersonId = deliveryPerson.Id,
            ExpectedEndDate = endDate
        };

        return await repository.Save(rental);
    }

    private static decimal Multiply(Product product)
    {
        return product.PricePerDay *= product.LateFee;
    }
    
    private static decimal Sum(Product product)
    {
        return product.PricePerDay += product.LateFee;
    }
}