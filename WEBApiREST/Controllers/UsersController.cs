using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WEBApiREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ApplicationContext db;
        public UsersController(ApplicationContext context)
        {
            db = context;
            if (db.Users.Any())
            {
                db.Users.Add(new UserEntity
                    {
                    id = Guid.NewGuid(),
                    username = "Иван",
                    avatarUrl = null,
                    subscribersAmount = 0,
                    firstName = "Иван",
                    lastName = "Черняков",
                    isActive = false,
                    stack = ["Java", "PHP"],
                    city = null,
                    description = null,
                    token = null


                });
            }
            
            
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEntity>>> Get()
        {
            return await  db.Users.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserEntity>> Get(Guid id)
        {
            UserEntity? user = await db.Users.FirstOrDefaultAsync(x => x.id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }
        [HttpPost]
        public async Task<ActionResult<UserEntity>> Put(UserEntity user)
        {
            if (user == null)
            { 
                return  BadRequest();
            }
            if (!db.Users.Any(x => x.id == user.id)) 
            {
                return NotFound();
            }
            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
