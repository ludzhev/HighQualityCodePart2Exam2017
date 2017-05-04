using System;
using Bytes2you.Validation;
using ProjectManager.Common.Constrants;
using ProjectManager.Common.Exceptions;

namespace ProjectManager.Core
{
    public class Engine
    {
        private readonly ILogger logger;
        private readonly ICommandProcessor processor;
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(ILogger logger, ICommandProcessor processor, IReader reader, IWriter writer)
        {
            Guard.WhenArgument(logger, "Engine Logger provider").IsNull().Throw();
            Guard.WhenArgument(processor, "Engine Processor provider").IsNull().Throw();
            Guard.WhenArgument(reader, "Engine reader provider").IsNull().Throw();
            Guard.WhenArgument(writer, "Engine writer provider").IsNull().Throw();

            this.logger = logger;
            this.processor = processor;
            this.reader = reader;
            this.writer = writer;
        }

        public void Start()
        {
            var commandAsString = this.reader.ReadLine();

            while (commandAsString.ToLower() != "exit")
            {
                try
                {
                    var executionResult = this.processor.Process(commandAsString);
                    this.writer.WriteLine(executionResult);
                }
                catch (UserValidationException ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    this.writer.WriteLine("Ops, something happened!");
                    this.logger.Error(ex.Message);
                }

                commandAsString = this.reader.ReadLine();
            }

            this.writer.WriteLine("Program terminated.");
        }
    }
}
