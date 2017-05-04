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
    public class CreateProjectCommand : ICommand
    {
        private readonly IDatabase dataBase;
        private readonly IModelsFactory modelsFactory;

        public CreateProjectCommand(IDatabase dataBase, IModelsFactory modelsFactory)
        {
            Guard.WhenArgument(dataBase, "CreateProjectCommand Database").IsNull().Throw();
            Guard.WhenArgument(modelsFactory, "CreateProjectCommand ModelsFactory").IsNull().Throw();

            this.dataBase = dataBase;
            this.modelsFactory = modelsFactory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 4)
            {
                throw new UserValidationException("Invalid command parameters count!");
            }

            if (parameters.Any(x => x == string.Empty))
            {
                throw new UserValidationException("Some of the passed parameters are empty!");
            }

            if (this.dataBase.Projects.Any(x => x.Name == parameters[0]))
            {
                throw new UserValidationException("A project with that name already exists!");
            }

            ProjectState state;
            if (!Enum.TryParse<ProjectState>(parameters[3], out state))
            {
                throw new ArgumentException("Valid state is needed to create a task!");
            }

            var project = this.modelsFactory.CreateProject(parameters[0], parameters[1], parameters[2], state);
            this.dataBase.Projects.Add(project);

            return "Successfully created a new project!";
        }
    }
}