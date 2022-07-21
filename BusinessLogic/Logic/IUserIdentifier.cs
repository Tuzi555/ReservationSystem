using Microsoft.Extensions.Configuration;

namespace Services.Logic;

public interface IUserIdentifier
{
    int GetUserIdFromToken(string accessToken, IConfiguration configuration);
}
