using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IUserData
    {
        Task<UserModel?> GetUser(string email);
        Task InsertUser(UserModel user);
    }
}
