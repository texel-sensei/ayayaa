
using ayayaa.core.config;
using ayayaa.core.config.sources;
using Xunit;

namespace tests.core.config
{
    public class IniSourceTest
    {
        private readonly ConfigManager _manager = new ConfigManager();

        [Fact]
        private void TestIniConfig()
        {
            var configString =
                @"
[Test]
SomeInt = 77
SomeString = Hello World!
; Some comment
; SomeBool = true ; commented out, to test missing values
";
            _manager.AddConfigSource(IniConfigSource.FromString(configString));

            var config = _manager.GetConfigObject<TestConfig>();

            Assert.Equal(77, config.SomeInt);
            Assert.Equal("Hello World!", config.SomeString);
            Assert.False(config.SomeBool);
        }
    }
}
