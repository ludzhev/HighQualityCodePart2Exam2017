using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using ProjectManager.Commands.Contracts;
using ProjectManager.Common.Exceptions;
using ProjectManager.Data;

namespace ProjectManager.Commands
{
    public sealed class ListProjectsCommand : ICommand
    {
        private readonly IDatabase dataBase;

        public ListProjectsCommand(IDatabase dataBase)
        {
            Guard.WhenArgument(dataBase, "ListProjectsCommand Database").IsNull().Throw();

            this.dataBase = dataBase;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 0)
            {
                throw new UserValidationException("Invalid command parameters count!");
            }

            if (parameters.Any(x => x == string.Empty))
            {
                throw new UserValidationException("Some of the passed parameters are empty!");
            }

            return string.Join(Environment.NewLine, this.dataBase.Projects);
        }
    }
}
