using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Auth1.Classes;
using Auth1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace Auth1.Controllers;

[Route("login")]
[ApiController]
public class LoginController(IDbContextFactory<AuthDbContext> dbContextFactory, IConfiguration configuration) : ControllerBase
{
    //validate user properties and write jwt token
    
    //(jwt have been without expiration time)
    //(you also need to check page with explanation of DI in controllers)
    
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        await using var db = await dbContextFactory.CreateDbContextAsync();

        var user = await db.Users.FirstOrDefaultAsync(q => q.Email == email);
        
        if (user != null && user.Password == password)
        {
            //write jwt and send for this autist
            var jwt = new JwtSecurityToken
            (
                issuer: configuration["Issuer"], 
                audience: configuration["Audience"],
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, email)
                }, 
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMonths(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Secret"] ?? throw new Exception("Ты аутист и не находится у тебя Secret"))), SecurityAlgorithms.HmacSha256)
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtString = tokenHandler.WriteToken(jwt);
            
            HttpContext.Response.Cookies.Append("auth", jwtString, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true
            });
            
            return Ok();
        }
        else
        {
            //send notification about some problems
            return BadRequest("Something went wrong");
        }
    }
}