using LIUMarketplace.Models;
using LIUMarketplace.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LIUMarketPlace.Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task<UserDto> GetUserByProductIdAsync(string id);
        Task DeleterUserAsync(string id);
        Task<UserDto> UpdateUserAsync(UserDto user);
    }

    public class UserService : IUserService
    {
        private ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task DeleterUserAsync(string id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = _db.Users.OrderByDescending(u => u.FirstName);

           
            return await users.Select(u => new UserDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Campus = u.Campus,
                Major = u.Major,
                Id = u.Id,
            }).ToListAsync();

        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user =await _db.Users.FindAsync(id);
            if(user != null)
            {
                return new UserDto
                {
                    FirstName = user.FirstName,
                    LastName= user.LastName,
                    Campus = user.Campus,
                    Id = user.Id,
                    Major = user.Major,
                    Email = user.Email,
                };
            }

            return null;
        }

        public async Task<UserDto> GetUserByProductIdAsync(string id)
        {
            var user = await (from p in _db.Users
                                  join ci in _db.Products
                                  on p.Id equals ci.CreatedByUserId
                                  where ci.Id == id

                                  select p).FirstOrDefaultAsync();
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Campus = user.Campus,
                Id = user.Id,
                Major = user.Major,
                Email = user.Email,
            };
           
        }

        public Task<UserDto> UpdateUserAsync(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
