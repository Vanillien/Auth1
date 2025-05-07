using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth1.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<string> Test()
    {
        return new string("wawa");
    }
}