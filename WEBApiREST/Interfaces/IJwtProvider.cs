namespace WEBApiREST.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(UserEntity user);
        string GetUserFromToken(string accessToken);
    }
}