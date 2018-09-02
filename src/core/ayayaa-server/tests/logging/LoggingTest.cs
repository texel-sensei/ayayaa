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
        private readonly Logger logger = new Logger(new ConsoleWriter(), LogPriority.Medium);

        [Fact]
        private void FormatMessage()
        {
            string message = "This is a test message.";
            string fMessage = string.Empty;

            fMessage = logger.FormatEntry(message, LogPriority.Low);
            Assert.Equal("[LOW] This is a test message.", fMessage);

            fMessage = logger.FormatEntry(message, LogPriority.Medium);
            Assert.Equal("[MED] This is a test message.", fMessage);

            fMessage = logger.FormatEntry(message, LogPriority.High);
            Assert.Equal("[HIGH] This is a test message.", fMessage);

            fMessage = logger.FormatEntry(message, LogPriority.Warning);
            Assert.Equal("[WAR] This is a test message.", fMessage);

            fMessage = logger.FormatEntry(message, LogPriority.Exception);
            Assert.Equal("[ERROR] This is a test message.", fMessage);

            fMessage = logger.FormatEntry(message, LogPriority.EverythingIsOnFire);
            Assert.Equal("[FIRE] This is a test message.", fMessage);
        }

        [Fact]
        private void WriteMessageLow()
        {
            // Assert setup
            Assert.Equal<LogPriority>(LogPriority.Medium, logger.MinimumPriority); // Did we assign the correct priority?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.Low;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.False(success);   // Did the write get cancelled as expected?
        }

        [Fact]
        private void WriteMessageMedium()
        {
            // Assert setup
            Assert.Equal<LogPriority>(LogPriority.Medium, logger.MinimumPriority); // Did we assign the correct priority?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.Medium;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.True(success);   // Did the write succeed?
        }

        [Fact]
        private void WriteMessageHigh()
        {
            // Assert setup
            Assert.Equal<LogPriority>(LogPriority.Medium, logger.MinimumPriority); // Did we assign the correct priority?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.High;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.True(success);   // Did the write succeed?
        }

        [Fact]
        private void WriteMessageWarning()
        {
            // Assert setup
            Assert.Equal<LogPriority>(LogPriority.Medium, logger.MinimumPriority); // Did we assign the correct priority?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.Warning;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.False(success);   // Did the write get canceled as expected?
        }

        [Fact]
        private void WriteMessageException()
        {
            // Assert setup
            Assert.Equal<LogPriority>(LogPriority.Medium, logger.MinimumPriority); // Did we assign the correct priority?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.Exception;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.True(success);   // Did the write succeed?
        }

        [Fact]
        private void WriteMessageFire()
        {
            // Assert setup
            Assert.Equal<LogPriority>(LogPriority.Medium, logger.MinimumPriority); // Did we assign the correct priority?
            Assert.IsType<ConsoleWriter>(logger.Writer);    // Did we assign the correct Writer to the logger?

            // Arrange
            string message = "Logging works as expected.";
            LogPriority priority = LogPriority.EverythingIsOnFire;

            // Act
            bool success = logger.WriteToLog(message, priority);

            // Assert
            Assert.True(success);   // Did the write succeed?
        }
    }

    public class ConsoleWriterTest
    {
        internal ConsoleWriter writer => new ConsoleWriter();

        [Fact]
        public void SerializeMessage()
        {
            // Arrange
            string message = "[ERROR] Failure to tag image #12.";
            string fMessage = string.Empty;

            // Act
            fMessage = writer.SerializeMessage(message) as string;

            // Assert
            Assert.Equal(message, fMessage);
        }

        // No need to test the Write-function for the ConsoleLogger as it is literally too simply to fail. 
        // Trust me, I tried.
    }
}
