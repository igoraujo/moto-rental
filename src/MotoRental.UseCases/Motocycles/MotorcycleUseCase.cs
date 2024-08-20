using MotoRental.Borders.Models;
using MotoRental.Repositories.Base;
using MotoRental.UseCases.Base;

namespace MotoRental.UseCases.Motocycles;

public class MotorcycleUseCase(IBaseRepository<Motorcycle> repository) : BaseUseCase<Motorcycle>(repository), IMotorcycleUseCase
{
    public override async Task<IEnumerable<Motorcycle>?> FindAll()
    {
        var list = new List<Motorcycle>();
        for (int i = 0; i < 4; i++)
        {
            var moto = new Motorcycle
            {
                Id = i + 1,
                Model = $"CG 1{i}0",
                PlateNumber = $"ABC123H{i}"
            };
            
            list.Add(moto);
        }
        
        return list;
    }
}