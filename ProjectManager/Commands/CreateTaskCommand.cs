using System.Collections.Generic;
using System.Linq;
using ProjectManager.Commands.Contracts;
using ProjectManager.Common.Exceptions;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager.Commands
{
    public sealed class CreateTaskCommand : ICommand
    {
        public string Execute(List<string> prms)
        {
            var db = new Database();

            var zavoda = new ModelsFactory();

            if (prms.Count != 4)
            { 
                throw new UserValidationException("Invalid command parameters count!");
            }

            if (prms.Any(x => x == string.Empty))
            {
                throw new UserValidationException("Some of the passed parameters are empty!");
            }
            
            var pj = db.Projects[int.Parse(prms[0])];

            var owner = pj.Users[int.Parse(prms[1])];

            var task = zavoda.CreateTask(owner, prms[2], prms[3]);
            pj.Tasks.Add(task);

            return "Successfully created a new task!";
        }
    }
}