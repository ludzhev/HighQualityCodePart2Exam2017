using ProjectManager.Commands;
using ProjectManager.Common.Providers;
using ProjectManager.Core;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager
{
    public class Startup
    {
        public static void Main()
        {
            var validator = new Validator();
            
            var modelsFactory = new ModelsFactory(validator);
            var dataBase = new Database();

            var commandFactory = new CommandsFactory(dataBase, modelsFactory);

            var cmdCPU = new CommandProcessor(commandFactory);
            var logger = new FileLogger();
            var reader = new ConsoleReader();
            var writer = new ConsoleWriter();

            var engine = new Engine(logger, cmdCPU, reader, writer);

            engine.Start();
        }
    }
}
