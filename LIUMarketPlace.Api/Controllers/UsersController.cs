using LIUMarketPlace.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIUMarketPlace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #region GetAllUsers
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {

            var users = await _userService.GetAllUserAsync();

            return Ok(users);

        }
        #endregion

        #region GetUserById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {

            var user = await _userService.GetUserByIdAsync(id);

            return Ok(user);

        }
        #endregion
        #region GetUserByProductId
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetUserByProductId(string id)
        {

            var user = await _userService.GetUserByProductIdAsync(id);

            return Ok(user);

        }
        #endregion

        #region DeleteUser
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.GetUserByIdAsync(id);

            if (result == null)
            {
                return BadRequest($"User with id {id} is not found");
            }
            else
            {
                await _userService.DeleterUserAsync(id);
                return Ok($"{result.FirstName} deleted");
            }


        }
        #endregion
    }
}
