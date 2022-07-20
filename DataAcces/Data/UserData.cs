using DataAccess.Models;
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

    public async Task<UserModel?> GetUser(string email)
    {
        var results =
            await _db.LoadData<UserModel, dynamic>("reservation_system.spUsers_Get_By_Email", new { Email = email });
        return results.FirstOrDefault();
    }

    public Task InsertUser(UserModel user)
    {
        return _db.SaveData("reservation_system.spUsers_Insert", new { user.FirstName, user.LastName, user.Email, user.Phone, user.PasswordHash, user.PasswordSalt });
    }
}
