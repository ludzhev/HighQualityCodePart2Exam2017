using ProjectManager.Models.Enums;

namespace ProjectManager.Models.Contracts
{
    public interface ITask
    {
        string Name { get; set; }
        
        User Owner { get; set; }

        TaskState State { get; set; }
    }
}
