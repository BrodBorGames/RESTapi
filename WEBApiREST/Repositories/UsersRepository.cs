using Microsoft.EntityFrameworkCore;
using WEBApiREST;
using WEBApiREST.Interfaces.Interfaces;

namespace WebAPI1.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationContext _dbcontext;

        public UsersRepository(ApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<UserEntity>> Get()
        {
            return await _dbcontext.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UserEntity?> GetById(Guid id)
        {

            return await _dbcontext.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.id == id);

        }
        public async Task<UserEntity?> GetByUsername(string username)
        {

            return await _dbcontext.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.username == username);

        }
        public async Task<UserEntity?> GetByToken(string token)
        {

            return await _dbcontext.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.token == token);
        }
        public async Task<List<UserEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbcontext.Users
                        .AsNoTracking()
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        public async Task Add(Guid id, string username, string? avatarUrl,
            int? subscribersAmount, string? firstName, string? lastName,
            bool isActive, string[]? stack, string? city, string? description,
            string? token, string passwordHash)
        {
            var userEntity = new UserEntity
            {
                id = id,
                username = username,
                avatarUrl = avatarUrl,
                subscribersAmount = 0,
                firstName = firstName,
                lastName = lastName,
                isActive = isActive,
                stack = stack,
                city = city,
                description = description,
                token = token,
                passwordHash = passwordHash

            };


            await _dbcontext.AddAsync(userEntity);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task Add(Guid id, string username, bool isActive, string passwordHash)
        {
            var userEntity = new UserEntity
            {
                id = id,
                username = username,
                isActive = isActive,
                passwordHash = passwordHash
            };


            await _dbcontext.AddAsync(userEntity);
            await _dbcontext.SaveChangesAsync();
        }


        public async Task Update(Guid id, string username, string avatarUrl,
            int subscribersAmount, string firstName, string lastName,
            bool isActive, string[] stack, string? city, string? description,
            string? token, string? passwordHash)
        {
            var userEntity = await _dbcontext.Users.FirstOrDefaultAsync(u => u.id == id)
                ?? throw new Exception();
            userEntity.username = username;
            userEntity.username = username;
            userEntity.avatarUrl = avatarUrl;
            userEntity.subscribersAmount = subscribersAmount;
            userEntity.firstName = firstName;
            userEntity.lastName = lastName;
            userEntity.isActive = isActive;
            userEntity.stack = stack;
            userEntity.city = city;
            userEntity.description = description;
            userEntity.passwordHash = passwordHash;
            await _dbcontext.SaveChangesAsync();

        }
        public async Task Delete(Guid id)
        {
            await _dbcontext.Users
                .Where(u => u.id == id)
                .ExecuteDeleteAsync();
        }

        
    }
}
