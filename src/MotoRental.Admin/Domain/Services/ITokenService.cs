using MotoRental.Borders.Models;
namespace  MotoRental.Admin.Domain.Services;

public interface ITokenService
{
    string GenerateToken(Session user, string cookieValue);
}
