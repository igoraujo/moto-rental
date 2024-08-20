using Microsoft.Extensions.Configuration;
using MotoRental.Borders.Models;
using MotoRental.Repositories.Base;

namespace MotoRental.Repositories.Motocycles;

public class MotorcycleRepository(IConfiguration configuration) : BaseRepository<Motorcycle>(configuration), IMotorcycleRepository;