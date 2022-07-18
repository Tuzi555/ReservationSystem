using DataAcces.Models;
using DataAccess.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetUsers()
    {
        return _db.LoadData<UserModel, dynamic>("reservation_system.spUser_GetAll", new { });
    }

    public async Task<UserModel?> GetUser(int id)
    {
        var results =
            await _db.LoadData<UserModel, dynamic>("reservation_system.spUser_Get_By_Id", new { id = id });
        return results.FirstOrDefault();
    }

    public Task InsertUser(UserModel user)
    {
        return _db.SaveData("reservation_system.spUser_Insert", new { user.FirstName, user.LastName, user.Email, user.Phone, user.PasswordHash, user.PasswordSalt });
    }

    public Task UpdateUser(UserModel user)
    {
        return _db.SaveData("reservation_system.spUser_Update", user);
    }

    public Task DeleteUser(int id)
    {
        return _db.SaveData("reservation_system.spUser_Delete", new { Id = id });
    }
}