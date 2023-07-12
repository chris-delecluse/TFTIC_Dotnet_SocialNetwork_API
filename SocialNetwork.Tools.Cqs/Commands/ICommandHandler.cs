using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Tools.Cqs.Commands;

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    CqsResult Execute(TCommand command);
}
