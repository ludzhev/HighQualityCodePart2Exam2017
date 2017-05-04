using ProjectManager.Commands;
using ProjectManager.Common;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager
{

    public class Startup
    {
        public static void Main()
        {
            var dataBase = new Database();
            var modelsFactory = new ModelsFactory();

            var commandFactory = new CommandsFactory(dataBase, modelsFactory);

            var cmdCPU = new CmdCPU(commandFactory);
            var logger = new FileLogger();

            var engine = new Engine(logger, cmdCPU);

            engine.Start();
        }
    }
}
