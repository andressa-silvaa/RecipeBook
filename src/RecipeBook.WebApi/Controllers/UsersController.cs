using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.User.Register;
using RecipeBook.Communication;

namespace RecipeBook.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterUser request)
    {
        var useCase = new RegisterUserUseCase();
        useCase.Execute(request);
        return Created();
    }
}

