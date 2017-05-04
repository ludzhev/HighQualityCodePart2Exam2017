namespace ProjectManager.Common.Constrants
{
    public interface ICommandProcessor
    {
        string Process(string commandAsString);
    }
}
