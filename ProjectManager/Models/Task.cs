using System.ComponentModel.DataAnnotations;
using System.Text;

using ProjectManager.Models.Contracts;
using ProjectManager.Models.Enums;

namespace ProjectManager.Models
{
    public class Task : ITask
    {
        public Task(string name, User owner, TaskState state)
        {
            this.Name = name;
            this.Owner = owner;
            this.State = state;
        }

        [Required(ErrorMessage = "Task Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Task Owner is required")]
        public User Owner { get; set; }

        [Range(typeof(TaskState), "0", "1", ErrorMessage = "Task State can be either Pending, InProgress or Done!")]
        public TaskState State { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine("    Name: " + this.Name);
            builder.Append("    State: " + this.State);

            return builder.ToString();
        }
    }
}
