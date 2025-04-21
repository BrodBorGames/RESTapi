using WEBApiREST;

namespace WEBApiREST.Interfaces.Interfaces
{
    public interface IUsersRepository
    {
        Task Add(Guid id, string username, string? avatarUrl, int? subscribersAmount, string? firstName,
            string? lastName, bool isActive, string[]? stack, string? city, string? description, string? token, string passwordHash);
        Task Add(Guid id, string username, bool isActive, string passwordHash);
        Task Delete(Guid id);
        Task<List<UserEntity>> Get();
        Task<UserEntity?> GetById(Guid id);
        Task<List<UserEntity>> GetByPage(int page, int pageSize);
        Task<UserEntity?> GetByToken(string token);
        Task<UserEntity?> GetByUsername(string username);
        Task Update(Guid id, string username, string avatarUrl, int subscribersAmount, string firstName, string lastName, bool isActive, string[] stack, string? city, string? description, string? token, string? passwordHash);
    }
}