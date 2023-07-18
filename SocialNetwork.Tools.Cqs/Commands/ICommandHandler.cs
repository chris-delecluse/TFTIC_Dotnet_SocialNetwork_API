using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Tools.Cqs.Commands;

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    ICommandResult Execute(TCommand command);
}

public interface ICommandHandler<TCommand, TResult>
    where TCommand : ICommand
    where TResult : struct
{
    ICommandResult<TResult> Execute(TCommand command);
}
