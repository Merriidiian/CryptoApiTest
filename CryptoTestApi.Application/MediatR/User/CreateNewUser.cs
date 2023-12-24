using AutoMapper;
using CryptoTestApi.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using MediatR;

namespace CryptoTestApi.Application.MediatR.User;

public static class CreateNewUser
{
    public record Command(string? fullName) : IRequest<CommandResult>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.fullName)
                .NotEmpty()
                .WithMessage("Не введено имя пользователя");
            RuleFor(x => x.fullName)
                .Length(3, 50)
                .WithMessage("Некорректное имя пользователя");
        }
    }

    public record CommandResult(string fullName, Guid id);

    public class Handler : IRequestHandler<Command, CommandResult>
    {
        private readonly IUserRepository _repository;

        public Handler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var newUser = new Domain.Models.User
            {
                Id = new Guid(),
                DateTimeCreated = DateTimeOffset.Now,
                FullName = request.fullName
            };
            await _repository.InsertAsync(newUser, cancellationToken);
            return new CommandResult(newUser.FullName, newUser.Id);
        }
    }
}