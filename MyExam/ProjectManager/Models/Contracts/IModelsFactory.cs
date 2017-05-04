using ProjectManager.Models.Enums;

namespace ProjectManager.Models.Contracts
{
    public interface IModelsFactory
    {
        Project CreateProject(string name, string startingDate, string endingDate, ProjectState state);

        Task CreateTask(User owner, string name, TaskState state);

        User CreateUser(string username, string email);
    }
}
