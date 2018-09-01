using ayayaa.logging;
using ayayaa.logging.Enums;
using ayayaa.logging.Writers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace tests.logging
{
    public class ConsoleLoggerTest
    {
        private readonly Logger logger = new Logger(LogType.Console);

        [Fact]
        private void WriteMessageLow()
        {
            // Assert setup
            Assert.Equal<LogType>(LogType.Console, logger.LoggingType); // Did we create the Logger with the correct type?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.Low;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.True(success);   // Did the write succeed?
            Assert.Equal("LOW - Logging works as expected.", logger.Writer.Message);    // Did we actually write what we expected?
        }

        [Fact]
        private void WriteMessageMedium()
        {
            // Assert setup
            Assert.Equal<LogType>(LogType.Console, logger.LoggingType); // Did we create the Logger with the correct type?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.Medium;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.True(success);   // Did the write succeed?
            Assert.Equal("MED - Logging works as expected.", logger.Writer.Message);    // Did we actually write what we expected?
        }

        [Fact]
        private void WriteMessageHigh()
        {
            // Assert setup
            Assert.Equal<LogType>(LogType.Console, logger.LoggingType); // Did we create the Logger with the correct type?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.High;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.True(success);   // Did the write succeed?
            Assert.Equal("HIGH - Logging works as expected.", logger.Writer.Message);    // Did we actually write what we expected?
        }

        [Fact]
        private void WriteMessageWarning()
        {
            // Assert setup
            Assert.Equal<LogType>(LogType.Console, logger.LoggingType); // Did we create the Logger with the correct type?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.Warning;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.True(success);   // Did the write succeed?
            Assert.Equal("WAR - Logging works as expected.", logger.Writer.Message);    // Did we actually write what we expected?
        }

        [Fact]
        private void WriteMessageException()
        {
            // Assert setup
            Assert.Equal<LogType>(LogType.Console, logger.LoggingType); // Did we create the Logger with the correct type?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.Exception;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.True(success);   // Did the write succeed?
            Assert.Equal("ERROR - Logging works as expected.", logger.Writer.Message);    // Did we actually write what we expected?
        }

        [Fact]
        private void WriteMessageFire()
        {
            // Assert setup
            Assert.Equal<LogType>(LogType.Console, logger.LoggingType); // Did we create the Logger with the correct type?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.EverythingIsOnFire;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.True(success);   // Did the write succeed?
            Assert.Equal("FIRE - Logging works as expected.", logger.Writer.Message);    // Did we actually write what we expected?
        }
    }
}
