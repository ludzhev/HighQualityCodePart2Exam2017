using System;
using System.Collections.Generic;
using ProjectManager.Models.Enums;

namespace ProjectManager.Models.Contracts
{
    public interface IProject
    {
        string Name { get; set; }

        DateTime StartingDate { get; set; }

        DateTime EndingDate { get; set; }

        ProjectState State { get; set; }

        List<User> Users { get; }

        List<Task> Tasks { get; }
    }
}
