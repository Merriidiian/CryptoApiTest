using CryptoTestApi.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using MediatR;

namespace CryptoTestApi.Application.MediatR.Currency;

public static class CreateNewCurrency
{
    public record Command(string? name) : IRequest<CommandResult>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .WithMessage("Не введено название валюты");
            RuleFor(x => x.name)
                .Length(3, 50)
                .WithMessage("Некорректное название валюты");
        }
    }

    public record CommandResult(Guid id);

    public class Handler : IRequestHandler<Command, CommandResult>
    {
        private readonly ICurrencyRepository _repository;

        public Handler(ICurrencyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var newCurrency = new Domain.Models.Currency()
            {
                Id = new Guid(),
                DateTimeCreated = DateTimeOffset.Now,
                Name = request.name
            };
            await _repository.InsertAsync(newCurrency, cancellationToken);
            return new CommandResult(newCurrency.Id);
        }
    }
}