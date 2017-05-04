using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using ProjectManager.Commands.Contracts;
using ProjectManager.Common.Exceptions;
using ProjectManager.Data;

namespace ProjectManager.Commands
{
    public sealed class ListProjectDetails : ICommand
    {
        private readonly IDatabase dataBase;

        public ListProjectDetails(IDatabase dataBase)
        {
            Guard.WhenArgument(dataBase, "ListProjectDetails Database").IsNull().Throw();

            this.dataBase = dataBase;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new UserValidationException("Invalid command parameters count!");
            }

            int projectID;
             
            if (!int.TryParse(parameters.Single(), out projectID))
            {
                throw new UserValidationException("The parameter projectID is not a valid number!");
            }

            return string.Join(Environment.NewLine, this.dataBase.Projects[projectID]);
        }
    }
}
