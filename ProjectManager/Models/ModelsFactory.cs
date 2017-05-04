using System;

using Bytes2you.Validation;

using ProjectManager.Common.Constrants;
using ProjectManager.Common.Exceptions;
using ProjectManager.Models.Contracts;
using ProjectManager.Models.Enums;

namespace ProjectManager.Models
{
    public class ModelsFactory : IModelsFactory
    {
        private readonly IValidator validator;

        public ModelsFactory(IValidator validator)
        {
            Guard.WhenArgument(validator, "ModelsFactory Validator provider").IsNull().Throw();
            this.validator = validator;
        }

        public Project CreateProject(string name, string startingDate, string endingDate, ProjectState state)
        {
            DateTime starting;
            DateTime end;

            var startingDateSuccessful = DateTime.TryParse(startingDate, out starting);
            var endingDateSuccessful = DateTime.TryParse(endingDate, out end);

            if (!startingDateSuccessful)
            {
                throw new UserValidationException("Failed to parse the passed starting date!");
            }

            if (!endingDateSuccessful)
            {
                throw new UserValidationException("Failed to parse the passed ending date!");
            }

            var project = new Project(name, starting, end, state);

            this.validator.Validate(project);

            return project;
        }

        public Task CreateTask(User owner, string name, TaskState state)
        {
            var task = new Task(name, owner, state);
            this.validator.Validate(task);

            return task;
        }

        public User CreateUser(string username, string email)
        {
            var user = new User(email, username);
            this.validator.Validate(user);

            return user;
        }       
    }
}
