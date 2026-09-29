using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.User.Register;
using RecipeBook.Communication;
using RecipeBook.Communication.Responses;

namespace RecipeBook.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RequestRegisterUser request, [FromServices] IRegisterUserUseCase useCase)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }
}

