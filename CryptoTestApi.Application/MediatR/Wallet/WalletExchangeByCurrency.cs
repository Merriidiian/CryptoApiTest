using CryptoTestApi.Application.Validations;
using CryptoTestApi.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using MediatR;

namespace CryptoTestApi.Application.MediatR.Wallet;

public static class WalletExchangeByCurrency
{
    public record Command(Guid IdUser, Guid IdCurrencyWithdraw, Guid IdCurrencyReplenishment, double ExchangeRate,
        double Sum, double CommissionPercentage) : IRequest<CommandResult>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.IdUser)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator())
                .WithMessage("Неподходящий формат Guid пользователя");
            RuleFor(x => x.IdCurrencyWithdraw)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator())
                .WithMessage("Неподходящий формат Guid валюты");
            RuleFor(x => x.IdCurrencyReplenishment)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator())
                .WithMessage("Неподходящий формат Guid валюты");
            RuleFor(x => x.ExchangeRate)
                .NotNull()
                .NotEmpty()
                .Custom((x, context) =>
                {
                    if (x <= 0)
                    {
                        context.AddFailure("Курс не может быть меньше или равен нулю");
                    }
                });
            RuleFor(x => x.Sum)
                .NotNull()
                .NotEmpty()
                .Custom((x, context) =>
                {
                    if (x < 0)
                    {
                        context.AddFailure("Перевод не может быть меньше нуля");
                    }
                });
        }
    }

    public record CommandResult(bool result);

    public class Handler : IRequestHandler<Command, CommandResult>
    {
        private readonly IWalletRepository _repository;
        private readonly IMediator _mediator;

        public Handler(IWalletRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var walletWithdraw = new Domain.Models.Wallet();
            var walletReplenishment = new Domain.Models.Wallet();
            try
            {
                var command = new FindWallet.Command(request.IdUser, request.IdCurrencyWithdraw);
                var result = await _mediator.Send(command, cancellationToken);
                walletWithdraw = result.Wallet;
            }
            catch (CryptoException e)
            {
                var createNewWalletCommand = new CreateNewWallet.Command(request.IdUser, request.IdCurrencyWithdraw);
                var result = await _mediator.Send(createNewWalletCommand, cancellationToken);
                walletWithdraw = result.Wallet;
                throw new CryptoException("Кошелек для снятия средств пуст. Перед обменом валют пополните кошелёк.");
            }
            try
            {
                var command = new FindWallet.Command(request.IdUser, request.IdCurrencyReplenishment);
                var result = await _mediator.Send(command, cancellationToken);
                walletReplenishment = result.Wallet;
            }
            catch (CryptoException e)
            {
                var createNewWalletCommand = new CreateNewWallet.Command(request.IdUser, request.IdCurrencyReplenishment);
                var result = await _mediator.Send(createNewWalletCommand, cancellationToken);
                walletReplenishment = result.Wallet;
            }

            if (!(walletWithdraw.Balance >= request.Sum)) throw new CryptoException("Недостаточно средств!");
            walletWithdraw.Balance -= request.Sum;
            await _repository.UpdateAsync(walletWithdraw, cancellationToken);
            var commission = request.Sum * request.ExchangeRate * request.CommissionPercentage / 100;
            walletReplenishment.Balance +=
                request.Sum * request.ExchangeRate - commission;
            await _repository.UpdateAsync(walletReplenishment, cancellationToken);
            return new CommandResult(true);
        }
    }
}