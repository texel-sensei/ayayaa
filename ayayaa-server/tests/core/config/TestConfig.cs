using System;
using System.Collections.Generic;
using ayayaa.core.config;

namespace tests.core.config
{
    internal class TestSource : IConfigSource
    {
        public Dictionary<string, Dictionary<string, string>> Values =
            new Dictionary<string, Dictionary<string, string>>();

        private bool HasNamespace(string Namespace)
        {
            return Values.ContainsKey(Namespace);
        }

        public dynamic QueryValue(string Namespace, string key, Type fieldType)
        {
            if (!HasNamespace(Namespace)) return null;
            return Values[Namespace].GetValueOrDefault(key);
        }

        public TestSource Add(string key, object value)
        {
            var parts = key.Split('/');
            if (!Values.ContainsKey(parts[0]))
            {
                Values[parts[0]] = new Dictionary<string, string>();
            }

            Values[parts[0]][parts[1]] = value.ToString();
            return this;
        }
    }

    [ConfigNamespace("Test")]
    internal class TestConfig
    {
        public int SomeInt = 42;
        public string SomeString = "Hello";
        public bool SomeBool = false;
    }
}