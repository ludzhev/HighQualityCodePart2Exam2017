using System;
using Moq;
using NUnit.Framework;

using ProjectManager.Common.Constrants;
using ProjectManager.Core;
using ProjectManager.Tests.Extensions;

namespace ProjectManager.Tests.EngineTests
{
    [TestFixture]
    public class Start_Should
    {
        [TestCase("CreateProject DeathStar 2016-1-1 2018-05-04 Active")]
        [TestCase("CreateProject DeepSpaceNine 1800-1-1 2144-1-1 Active")]
        public void ReadsCommandTwice_WhenValidCommandAndExitArePassed(string validCommand)
        {
            // Arrange
            var terminationCommand = "Exit"; 

            var logger = new Mock<ILogger>();
            var processor = new Mock<ICommandProcessor>();
            var reader = new Mock<IReader>();
            var writer = new Mock<IWriter>();

            reader.SetupMany(r => r.ReadLine(), validCommand, terminationCommand);

            var sut = new Engine(logger.Object, processor.Object, reader.Object, writer.Object);

            // Act
            sut.Start();

            // Assert
            reader.Verify(r => r.ReadLine(), Times.Exactly(2));
        }

        [Test]
        public void WritesProperMessage_WhenTerminateCommandIsPassed()
        {
            // Arrange
            var properMessageOnExit = "Program terminated";

            var logger = new Mock<ILogger>();
            var processor = new Mock<ICommandProcessor>();
            var reader = new Mock<IReader>();
            var writer = new Mock<IWriter>();

            reader.Setup(r => r.ReadLine()).Returns("Exit");

            var sut = new Engine(logger.Object, processor.Object, reader.Object, writer.Object);

            // Act
            sut.Start();

            // Assert
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s.Contains(properMessageOnExit))), Times.Once());
        }

        [Test]
        public void LogMessage_WhenGenericExceptionOccurs()
        {
            // Arrange

            var terminationCommand = "Exit";
            var validCommand = "CreateProject DeepSpaceNine 1800-1-1 2144-1-1 Active";

            var logger = new Mock<ILogger>();
            var processor = new Mock<ICommandProcessor>();
            var reader = new Mock<IReader>();
            var writer = new Mock<IWriter>();

            processor.Setup(r => r.Process(It.IsAny<string>())).Throws(new Exception());
            reader.SetupMany(r => r.ReadLine(), validCommand, terminationCommand);

            var sut = new Engine(logger.Object, processor.Object, reader.Object, writer.Object);

            // Act
            sut.Start();

            // Assert
            logger.Verify(w => w.Error(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void WritesProperMessage_WhenGenericExceptionOccurs()
        {
            // Arrange
            var properMessageOnExit = "something happened";
            var terminationCommand = "Exit";
            var validCommand = "CreateProject DeepSpaceNine 1800-1-1 2144-1-1 Active";
            var logger = new Mock<ILogger>();
            var processor = new Mock<ICommandProcessor>();
            var reader = new Mock<IReader>();
            var writer = new Mock<IWriter>();

            processor.Setup(r => r.Process(It.IsAny<string>())).Throws(new Exception());
            reader.SetupMany(r => r.ReadLine(), validCommand, terminationCommand);

            var sut = new Engine(logger.Object, processor.Object, reader.Object, writer.Object);

            // Act
            sut.Start();

            // Assert
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s.Contains(properMessageOnExit))), Times.Once());
        }
    }
}
