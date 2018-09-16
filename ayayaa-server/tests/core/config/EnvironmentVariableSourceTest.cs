using ayayaa.core.config;
using ayayaa.core.config.sources;
using Nancy;
using Xunit;

namespace tests.core.config
{
    public class EnvironmentVariableSourceTest
    {
        private readonly ConfigManager _manager = new ConfigManager();

        public EnvironmentVariableSourceTest()
        {
            _manager.AddConfigSource(new EnvironmentConfigSource("ayayaa"));
        }

        [Fact]
        private void TestEnvironmentSource()
        {
            System.Environment.SetEnvironmentVariable("AYAYAA_TEST_SOMEINT", "7");

            var config = _manager.GetConfigObject<TestConfig>();
            Assert.Equal(7, config.SomeInt);
        }

        [Theory]
        // Empty string should also be recognized as `true`,
        // but there is no way in C# to programmatically set an environment
        // variable to be empty string (passing "" to SetEnvironmentVariable
        // deletes the variable instead
        /*[InlineData("")] */
        [InlineData("true")]
        [InlineData("1")]
        [InlineData("yes")]
        private void TestBoolTrueParsing(string value)
        {
            System.Environment.SetEnvironmentVariable("AYAYAA_TEST_SOMEBOOL", value);
            var config = _manager.GetConfigObject<TestConfig>();
            Assert.True(config.SomeBool);
        }

        [Theory]
        [InlineData("false")]
        [InlineData("0")]
        [InlineData("no")]
        private void TestBoolFalseParsing(string value)
        {
            System.Environment.SetEnvironmentVariable("AYAYAA_TEST_SOMEBOOL", value);
            var config = _manager.GetConfigObject<TestConfig>();
            Assert.False(config.SomeBool);
        }
    }
}