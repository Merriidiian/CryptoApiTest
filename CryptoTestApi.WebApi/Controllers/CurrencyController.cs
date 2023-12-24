using CryptoTestApi.Application.MediatR.Currency;
using CryptoTestApi.DTOs.CurrencyDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoTestApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/crypto/api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
public class CurrencyController : Controller
{
    private readonly IMediator _mediator;

    public CurrencyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(NewCurrencyDTO.Response), StatusCodes.Status200OK)]
    [SwaggerOperation("Добавить новую валюту")]
    public async Task<ActionResult<CreateNewCurrency.CommandResult>> CreateNewCurrency([FromQuery]string? name,
        CancellationToken cancellationToken)
    {
        var command = new CreateNewCurrency.Command(name);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}