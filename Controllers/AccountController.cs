using EmployeeManagementApi.Models;
using EmployeeManagementApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AccountController : ControllerBase
   {
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly SignInManager<ApplicationUser> _signInManager;
      private readonly IConfiguration _configuration;

      public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
      {
         _userManager = userManager;
         _signInManager = signInManager;
         _configuration = configuration;
      }

      [HttpPost("register")]
      public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
      {
         var user = new ApplicationUser { UserName = model.Username };
         var result = await _userManager.CreateAsync(user, model.Password);
         if (result.Succeeded)
         {
            return Ok(new { Result = "User created successfully" });
         }
         return BadRequest(result.Errors);
      }

      [HttpPost("login")]
      public async Task<IActionResult> Login([FromBody] LoginViewModel model)
      {
         var user = await _userManager.FindByNameAsync(model.Username);
         if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
         {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine($"Token: {tokenString}");

            return Ok(new
            {
               token = tokenString,
               expiration = token.ValidTo
            });
         }
         Console.WriteLine("Unauthorized login attempt.");
         return Unauthorized();
      }
   }
}
