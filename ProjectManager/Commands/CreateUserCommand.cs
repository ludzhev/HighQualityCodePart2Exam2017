﻿using System.Collections.Generic;
using System.Linq;
using ProjectManager.Commands.Contracts;
using ProjectManager.Common.Exceptions;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager.Commands
{
    public class CreateUserCommand : ICommand
    {
        private readonly Database dataBase;
        private readonly ModelsFactory modelsFactory;

        public CreateUserCommand(Database dataBase, ModelsFactory modelsFactory)
        {
            this.dataBase = dataBase;
            this.modelsFactory = modelsFactory;
        }

        public string Execute(List<string> parameters)
        {
            if (parameters.Count != 3)
            {
                throw new UserValidationException("Invalid command parameters count!");
            }

            if (parameters.Any(x => x == string.Empty))
            {
                throw new UserValidationException("Some of the passed parameters are empty!");
            }

            if (this.dataBase.Projects[int.Parse(parameters[0])].Users.Any() && this.dataBase.Projects[int.Parse(parameters[0])]
                    .Users.Any(x => x.UserName == parameters[1]))
            {
                throw new UserValidationException("A user with that username already exists!");
            }

            this.dataBase.Projects[int.Parse(parameters[0])].Users.Add(this.modelsFactory.CreateUser(parameters[1], parameters[2]));

            return "Successfully created a new user!";
        }
    }
}
