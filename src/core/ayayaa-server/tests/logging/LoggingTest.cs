using ayayaa.logging;
using ayayaa.logging.Enums;
using ayayaa.logging.Interfaces;
using ayayaa.logging.Writers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace tests.logging
{
    public class ConsoleLoggerTest
    {
        private readonly Logger logger = new Logger();

        [Fact]
        private void FormatMessage()
        {
            string message = "This is a test message.";
            string fMessage = string.Empty;

            fMessage = logger.FormatEntry(message, LogPriority.Trace);
            Assert.Equal("[TRACE] This is a test message.", fMessage);

            fMessage = logger.FormatEntry(message, LogPriority.Debug);
            Assert.Equal("[DEBUG] This is a test message.", fMessage);

            fMessage = logger.FormatEntry(message, LogPriority.Info);
            Assert.Equal("[INFO] This is a test message.", fMessage);

            fMessage = logger.FormatEntry(message, LogPriority.Warning);
            Assert.Equal("[WARNING] This is a test message.", fMessage);

            fMessage = logger.FormatEntry(message, LogPriority.Error);
            Assert.Equal("[ERROR] This is a test message.", fMessage);

            fMessage = logger.FormatEntry(message, LogPriority.FIRE);
            Assert.Equal("[FIRE] This is a test message.", fMessage);
        }

        [Fact]
        private void WriteToLogs()
        {
            // Arrange
            string message = "Logging works as expected.";

            LogPriority priority1 = LogPriority.Trace;
            LogPriority priority2 = LogPriority.Debug;
            LogPriority priority3 = LogPriority.Info;
            LogPriority priority4 = LogPriority.Warning;
            LogPriority priority5 = LogPriority.Error;
            LogPriority priority6 = LogPriority.FIRE;

            ConsoleWriter writer1 = new ConsoleWriter();
            ConsoleWriter writer2 = new ConsoleWriter();
            ConsoleWriter writer3 = new ConsoleWriter();

            logger.AddWriter(writer1, LogPriority.Trace);
            logger.AddWriter(writer2, LogPriority.Info);
            logger.AddWriter(writer3, LogPriority.Error);

            // Act
            Dictionary<IWriter, bool> result1 = logger.WriteToLogs(message, priority1);
            Dictionary<IWriter, bool> result2 = logger.WriteToLogs(message, priority2);
            Dictionary<IWriter, bool> result3 = logger.WriteToLogs(message, priority3);
            Dictionary<IWriter, bool> result4 = logger.WriteToLogs(message, priority4);
            Dictionary<IWriter, bool> result5 = logger.WriteToLogs(message, priority5);
            Dictionary<IWriter, bool> result6 = logger.WriteToLogs(message, priority6);

            // Assert
            Assert.True(result1[writer1]);
            Assert.False(result1[writer2]);
            Assert.False(result1[writer3]);

            Assert.True(result2[writer1]);
            Assert.False(result2[writer2]);
            Assert.False(result2[writer3]);

            Assert.True(result3[writer1]);
            Assert.True(result3[writer2]);
            Assert.False(result3[writer3]);

            Assert.True(result4[writer1]);
            Assert.True(result4[writer2]);
            Assert.False(result4[writer3]);

            Assert.True(result5[writer1]);
            Assert.True(result5[writer2]);
            Assert.True(result5[writer3]);

            Assert.True(result6[writer1]);
            Assert.True(result6[writer2]);
            Assert.True(result6[writer3]);
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
