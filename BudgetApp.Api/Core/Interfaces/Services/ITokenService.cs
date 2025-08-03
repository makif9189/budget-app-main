using BudgetApp.Api.Core.Entities;

namespace BudgetApp.Api.Core.Interfaces.Services;

public interface ITokenService
{
    string CreateToken(User user);
}