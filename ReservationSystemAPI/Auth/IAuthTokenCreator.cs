namespace ReservationSystemAPI.Auth
{
    public interface IAuthTokenCreator
    {
        string CreateToken(UserModel user);
    }
}