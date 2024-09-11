using System.Runtime.InteropServices;
using MotoRental.Borders.Models;
using MotoRental.Repositories.Base;
using MotoRental.UseCases.Base;

namespace MotoRental.UseCases.Motocycles;

public class MotorcycleUseCase(IBaseRepository<Motorcycle> repository) : BaseUseCase<Motorcycle>(repository), IMotorcycleUseCase
{
  
}