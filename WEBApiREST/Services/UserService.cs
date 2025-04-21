using Microsoft.AspNetCore.Identity;
using WEBApiREST.Interfaces;
using WEBApiREST.Interfaces.Interfaces;

namespace WEBApiREST.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _provider;
        private readonly IUsersRepository _usersRepository;
        public UserService(IUsersRepository usersRepository,IPasswordHasher passwordHasher, IJwtProvider provider) {
            _passwordHasher = passwordHasher;
            _provider = provider;
            _usersRepository = usersRepository;
        }

        public async Task Register(string username, string password)
        {
            var  hashedPassword = _passwordHasher.Generate(password);
            await _usersRepository.Add(Guid.NewGuid(), username: username, isActive: false, passwordHash: hashedPassword);

        }
        public async Task<string> Login(string username, string password)
        {

            var user = await _usersRepository.GetByUsername(username);

            var result = _passwordHasher.Verify(password, user.passwordHash);

            if (result == false)
            {
                throw new Exception("Failed To Login"); 
            }
            var token = _provider.GenerateToken(user);
            return token;
        }
    }
}
