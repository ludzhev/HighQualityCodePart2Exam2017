using ProjectManager.Commands.Contracts;
using ProjectManager.Common.Exceptions;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager.Commands
{
    public class CommandsFactory
    {
        private readonly Database dataBase;
        private readonly ModelsFactory modelsFactory;

        public CommandsFactory(Database dataBase, ModelsFactory modelsFactory)
        {
            this.dataBase = dataBase;
            this.modelsFactory = modelsFactory;
        }

        public ICommand CreateCommandFromString(string commandName)
        {
            var currentCommand = commandName.ToLower();

            switch (currentCommand)
            {
                case "createproject": return new CreateProjectCommand(this.dataBase, this.modelsFactory);
                case "createtask": return new CreateTaskCommand();
                case "listprojects": return new ListProjectsCommand(this.dataBase);
                default: throw new UserValidationException("The passed command is not valid!");
            }
        }
    }
}
