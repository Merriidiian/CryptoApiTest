using CryptoTestApi.Application.Validations;
using CryptoTestApi.Domain.Models;
using CryptoTestApi.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using MediatR;

namespace CryptoTestApi.Application.MediatR.Wallet;

public class ChangeWalletBalance
{
    public record Command(Guid IdUser, Guid IdCurrency, BalanceAction WalletBalanceAction, double Sum) : IRequest<CommandResult>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.IdUser)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator())
                .WithMessage("Неподходящий формат Guid пользователя");
            RuleFor(x => x.IdCurrency)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator())
                .WithMessage("Неподходящий формат Guid валюты");
            RuleFor(x => x.Sum)
                .NotNull()
                .NotEmpty()
                .Custom((x, context) =>
                {
                    if (x < 0)
                    {
                        context.AddFailure("Введенное числовое значение не может быть меньше нуля");
                    }
                });
        }
    }

    public record CommandResult(Domain.Models.Wallet Wallet);

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
            var wallet = new Domain.Models.Wallet();
            try
            {
                var command = new FindWallet.Command(request.IdUser, request.IdCurrency);
                var result = await _mediator.Send(command, cancellationToken);
                wallet = result.Wallet;
            }
            catch (CryptoException e)
            {
                var createNewWalletCommand = new CreateNewWallet.Command(request.IdUser, request.IdCurrency);
                var result = await _mediator.Send(createNewWalletCommand, cancellationToken);
                wallet = result.Wallet;
            }

            if (request.WalletBalanceAction == BalanceAction.Top)
            {
                wallet.Balance += request.Sum;
            }
            else
            {
                if (wallet.Balance >= request.Sum)
                {
                    wallet.Balance -= request.Sum;
                }
                else
                {
                    throw new CryptoException("Недостаточно средств");
                }
            }
            await _repository.UpdateAsync(wallet, cancellationToken);
            return new CommandResult(wallet);
        }
        
        
    }
}