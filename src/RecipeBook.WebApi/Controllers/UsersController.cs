using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.User.Register;
using RecipeBook.Communication;

namespace RecipeBook.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RequestRegisterUser request, [FromServices] IRegisterUserUseCase useCase)
    {
        await useCase.Execute(request);
        return Created();
    }
}

