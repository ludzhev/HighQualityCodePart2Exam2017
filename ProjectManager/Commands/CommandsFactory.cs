using Bytes2you.Validation;
using ProjectManager.Commands.Contracts;
using ProjectManager.Common.Exceptions;
using ProjectManager.Data;
using ProjectManager.Models.Contracts;

namespace ProjectManager.Commands
{
    public class CommandsFactory : ICommandFactory
    {
        private readonly IDatabase dataBase;
        private readonly IModelsFactory modelsFactory;

        public CommandsFactory(IDatabase dataBase, IModelsFactory modelsFactory)
        {
            Guard.WhenArgument(dataBase, "CommandsFactory Database").IsNull().Throw();
            Guard.WhenArgument(modelsFactory, "CommandsFactory ModelsFactory").IsNull().Throw();

            this.dataBase = dataBase;
            this.modelsFactory = modelsFactory;
        }

        public ICommand CreateCommandFromString(string commandName)
        {
            var currentCommand = commandName.ToLower();

            switch (currentCommand)
            {
                case "createproject": return new CreateProjectCommand(this.dataBase, this.modelsFactory);
                case "createuser": return new CreateUserCommand(this.dataBase, this.modelsFactory);
                case "createtask": return new CreateTaskCommand(this.dataBase, this.modelsFactory);
                case "listprojects": return new ListProjectsCommand(this.dataBase);
                case "listprojectdetails": return new ListProjectDetails(this.dataBase);
                default: throw new UserValidationException("The passed command is not valid!");
            }
        }
    }
}
