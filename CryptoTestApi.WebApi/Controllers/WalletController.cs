using CryptoTestApi.Application.MediatR.Wallet;
using CryptoTestApi.DTOs.WalletDTO;
using CryptoTestApi.Request.WalletDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BalanceAction = CryptoTestApi.Request.WalletDTO;

namespace CryptoTestApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/crypto/api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
public class WalletController : Controller
{
    private readonly IMediator _mediator;

    public WalletController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BalanceActionDTO.Response), StatusCodes.Status200OK)]
    [SwaggerOperation("Изменить валютный счет. Enums: 0 - пополнить, 1 - снять")]
    public async Task<ActionResult<ChangeWalletBalance.CommandResult>> BalanceAction(
        [FromBody] BalanceActionDTO.Request request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeWalletBalance.Command(request.IdUser, request.IdCurrency, request.WalletBalanceAction,
            request.sum);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WalletExchangeByCurrencyDTO.Response), StatusCodes.Status200OK)]
    [SwaggerOperation("Обмен валют")]
    public async Task<ActionResult<ChangeWalletBalance.CommandResult>> WalletExchangeByCurrency(
        [FromBody] WalletExchangeByCurrencyDTO.Request request,
        CancellationToken cancellationToken)
    {
        var command = new WalletExchangeByCurrency.Command(request.IdUser, request.IdCurrencyWithdraw,
            request.IdCurrencyReplenishment, request.ExchangeRate, request.Sum, request.CommissionPercentage);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok("Обмен валют прошел успешно!");
    }
}