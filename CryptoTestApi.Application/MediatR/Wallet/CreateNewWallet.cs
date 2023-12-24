using CryptoTestApi.Application.Validations;
using CryptoTestApi.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using MediatR;

namespace CryptoTestApi.Application.MediatR.Wallet;

public static class CreateNewWallet
{
    public record Command(Guid IdUser, Guid IdCurrency) : IRequest<CommandResult>;

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
        }
    }

    public record CommandResult(Domain.Models.Wallet Wallet);

    public class Handler : IRequestHandler<Command, CommandResult>
    {
        private readonly IWalletRepository _repository;

        public Handler(IWalletRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var newWallet = new Domain.Models.Wallet()
            {
                Id = new Guid(),
                Balance = 0,
                CurrencyId = request.IdCurrency,
                UserId = request.IdUser,
                DateTimeCreated = DateTimeOffset.Now
            };
            await _repository.InsertAsync(newWallet, cancellationToken);
            return new CommandResult(newWallet);
        }
    }
}