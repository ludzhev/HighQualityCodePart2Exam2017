using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using ProjectManager.Commands.Contracts;
using ProjectManager.Common.Exceptions;
using ProjectManager.Data;
using ProjectManager.Models.Contracts;
using ProjectManager.Models.Enums;

namespace ProjectManager.Commands
{
    public sealed class CreateTaskCommand : ICommand
    {
        private readonly IDatabase dataBase;
        private readonly IModelsFactory modelsFactory;

        public CreateTaskCommand(IDatabase dataBase, IModelsFactory modelsFactory)
        {
            Guard.WhenArgument(dataBase, "CreateTaskCommand Database").IsNull().Throw();
            Guard.WhenArgument(modelsFactory, "CreateTaskCommand ModelsFactory").IsNull().Throw();

            this.dataBase = dataBase;
            this.modelsFactory = modelsFactory;
        }

        public string Execute(IList<string> prms)
        {
            if (prms.Count != 4)
            { 
                throw new UserValidationException("Invalid command parameters count!");
            }

            if (prms.Any(x => x == string.Empty))
            {
                throw new UserValidationException("Some of the passed parameters are empty!");
            }

            var project = this.dataBase.Projects[int.Parse(prms[0])];

            var owner = project.Users[int.Parse(prms[1])];

            TaskState state;
            if (!Enum.TryParse<TaskState>(prms[3], out state))
            {
                throw new ArgumentException("Valid state is needed to create a task!");
            }

            var task = this.modelsFactory.CreateTask(owner, prms[2], state);
            project.Tasks.Add(task);

            return "Successfully created a new task!";
        }
    }
}