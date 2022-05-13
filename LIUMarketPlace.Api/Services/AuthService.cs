using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LIUMarketPlace.Api.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;

        public AuthService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }
        }



        public async Task<AuthResponse> CreateUserAsync(RegisterDto model, string role)
        {
            if (model != null)
            {
                var checkUser = await _userManager.FindByEmailAsync(model.Email);

                if (checkUser != null)
                {
                    return new AuthResponse { Messages = "User already exist" };
                }
                else
                {
                    //check for university email 
                    var email = model.Email;
                    var domain = email.Split('@')[1];


                    if(domain != "students.liu.edu.lb")
                    {
                        return new AuthResponse { Messages = "Please use University email to regisger" };
                    }

                    if (model.Password != model.ConfirmPassword)
                    {
                        return new AuthResponse { Messages = "Confirm password should equal password" };
                    }
                    else
                    {
                        var user = new ApplicationUser
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Campus = model.Campus,
                            Major = model.Major

                        };

                        var result = await _userManager.CreateAsync(user, model.Password);

                        if (!result.Succeeded)
                        {
                            var errors = string.Empty;
                            foreach (var error in result.Errors)
                            {
                                errors += $"{error.Description}";
                            }
                            return new AuthResponse { Messages = errors };

                        }
                        await _userManager.AddToRoleAsync(user, role);


                        //adding jwt 
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.GivenName, user.FirstName),
                            new Claim(ClaimTypes.Surname, user.LastName)
                        };

                        // Get User roles and add them to claims
                        var roles = await _userManager.GetRolesAsync(user);
                        AddRolesToClaims(claims, roles);

                        


                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

                        var token = new JwtSecurityToken(
                            issuer: _configuration["AuthSettings:Issuer"],
                            audience: _configuration["AuthSettings:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                        string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

                        return new AuthResponse
                        {
                            Messages = "User Reisterd Successfully",
                            Email = model.Email,
                            UserName = model.FirstName,
                            IsAuthenticated = true,
                            ExpiresOn = token.ValidTo,
                            Token = tokenAsString

                        };
                    }


                }

            }
            return new AuthResponse
            {
                Messages = "Please fill all fields"
            };
        }

        public async Task<AuthResponse> LoginUserAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return new AuthResponse { Messages = "Email or password is invalid" };

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new AuthResponse { Messages = "Username or password is invalid" };


            //adding jwt 
            var claims = new List<Claim>
            {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.GivenName, user.FirstName),
                            new Claim(ClaimTypes.Surname, user.LastName)
                        };

            // Get User roles and add them to claims
            var roles = await _userManager.GetRolesAsync(user);
            AddRolesToClaims(claims, roles);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponse
            {
                Messages = "Logged In",
                Email = user.Email,
                UserName = user.FirstName,
                IsAuthenticated = true,
                ExpiresOn = token.ValidTo,
                Token = tokenAsString

            };


        }
    }
}
