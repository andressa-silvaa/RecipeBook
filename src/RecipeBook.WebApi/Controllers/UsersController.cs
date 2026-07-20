using Microsoft.AspNetCore.Mvc;
using RecipeBook.Communication;

namespace RecipeBook.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterUser request)
    {
        return Created(); //statusCode 201
    }
}

