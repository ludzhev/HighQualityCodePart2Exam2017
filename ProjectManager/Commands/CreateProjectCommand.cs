using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;
using ProjectManager.Commands.Contracts;
using ProjectManager.Common.Exceptions;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager.Commands
{
    public class CreateProjectCommand : ICommand
    {
        private readonly Database dataBase;
        private readonly ModelsFactory modelsFactory;

        public CreateProjectCommand(Database database, ModelsFactory factory)
        {
            Guard.WhenArgument(database, "CreateProjectCommand Database").IsNull().Throw();
            Guard.WhenArgument(factory, "CreateProjectCommand ModelsFactory").IsNull().Throw();

            this.dataBase = database;
            this.modelsFactory = factory;
        }

        public string Execute(List<string> parameters)
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

            var project = this.modelsFactory.CreateProject(parameters[0], parameters[1], parameters[2], parameters[3]);
            this.dataBase.Projects.Add(project);

            return "Successfully created a new project!";
        }
    }
}