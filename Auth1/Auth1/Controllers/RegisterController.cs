using Auth1.Classes;
using Auth1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth1.Controllers;

[Route("register")]
[ApiController]
public class RegisterController(IDbContextFactory<AuthDbContext> dbContextFactory) : ControllerBase
{
    //Add user in db 
    [HttpPost]
    public async Task<IActionResult> AddUser(string email, string password)
    {
        await using var db = await dbContextFactory.CreateDbContextAsync();
        
        db.Add(new AppUser()
        {
            Email = email,
            Password = password
        });

        await db.SaveChangesAsync();

        return Ok();
    }
}