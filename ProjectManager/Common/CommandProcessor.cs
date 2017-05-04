using Bytes2you.Validation;
using ProjectManager.Commands;
using System;
using System.Linq;

namespace ProjectManager.Common
{
    public class CmdCPU
    {
        private CommandsFactory fac;
        
        public CmdCPU(CommandsFactory fac)
        {
            this.fac = fac;
        }

        public string Process(string cl)
        {
            if (string.IsNullOrWhiteSpace(cl))
            {
                throw new Exceptions.UserValidationException("No command has been provided!");
            }

            var command = this.fac.CreateCommandFromString(cl.Split(' ')[0]);
            return command.Execute(cl.Split(' ').Skip(1).ToList());
        }
    }
}
