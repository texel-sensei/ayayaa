using ayayaa.core.config;
using Xunit;

namespace tests.core.config
{
    public class ConfigManagerTest
    {
        private readonly ConfigManager _config = new ConfigManager();

        [Fact]
        private void TestDefaults()
        {
            _config.AddConfigSource(new TestSource());
            var cfg = _config.GetConfigObject<TestConfig>();

            Assert.NotNull(cfg);
            Assert.Equal(42, cfg.SomeInt);
            Assert.Equal("Hello", cfg.SomeString);
            Assert.False(cfg.SomeBool);
        }

        [Fact]
        private void TestFillObject()
        {
            _config.AddConfigSource(
                new TestSource()
                .Add("Test/SomeInt", 5)
                .Add("Test/SomeBool", false)
                .Add("Test/SomeString", "Bye")
            );
            var cfg = _config.GetConfigObject<TestConfig>();

            Assert.NotNull(cfg);
            Assert.Equal(5, cfg.SomeInt);
            Assert.Equal("Bye", cfg.SomeString);
            Assert.False(cfg.SomeBool);
        }

        [Fact]
        private void TestInvalidConfigObject()
        {
            Assert.Throws<ConfigException>(() => _config.GetConfigObject<int>());
        }
    }
}
