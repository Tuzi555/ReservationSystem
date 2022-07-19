namespace ReservationSystemAPI
{
    public interface IAuthTokenCreator
    {
        string CreateToken(UserModel user);
    }
}