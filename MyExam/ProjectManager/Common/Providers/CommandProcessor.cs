using System.Linq;
using Bytes2you.Validation;
using ProjectManager.Commands;
using ProjectManager.Common.Constrants;

namespace ProjectManager.Common.Providers
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly CommandsFactory commandsFactory;
        
        public CommandProcessor(CommandsFactory commandsFactory)
        {
            Guard.WhenArgument(commandsFactory, "CommandProcessor commandsFactory").IsNull().Throw();

            this.commandsFactory = commandsFactory;
        }

        public string Process(string commandAsString)
        {
            if (string.IsNullOrWhiteSpace(commandAsString))
            {
                throw new Exceptions.UserValidationException("No command has been provided!");
            }

            var command = this.commandsFactory.CreateCommandFromString(commandAsString.Split(' ')[0]);
            return command.Execute(commandAsString.Split(' ').Skip(1).ToList());
        }
    }
}
