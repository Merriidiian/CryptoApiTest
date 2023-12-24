using CryptoTestApi.Application.MediatR.User;
using CryptoTestApi.DTOs.UserDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoTestApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("/crypto/api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(NewUserDTO.Response), StatusCodes.Status200OK)]
    [SwaggerOperation("Добавить нового пользователя")]
    public async Task<ActionResult<CreateNewUser.CommandResult>> CreateNewUser([FromQuery]string? fullName,
        CancellationToken cancellationToken)
    {
        var command = new CreateNewUser.Command(fullName);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}