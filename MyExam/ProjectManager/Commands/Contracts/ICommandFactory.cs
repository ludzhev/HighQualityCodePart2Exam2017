namespace ProjectManager.Commands.Contracts
{
    public interface ICommandFactory
    {
        ICommand CreateCommandFromString(string commandName);
    }
}
