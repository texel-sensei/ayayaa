using System;
using System.Collections;

namespace ayayaa.core.config.sources
{
    /**
     * A class to read config entries based on the current environment variables.
     * All variables are of the form <SYSTEM_PREFIX>_<NAMESPACE>_<KEY> in full
     * caps.
     */
    public class EnvironmentConfigSource : IConfigSource
    {
        public readonly string Prefix;

        public EnvironmentConfigSource(string prefix)
        {
            Prefix = prefix.EndsWith('_') ? prefix : prefix + "_";
        }

        public dynamic QueryValue(string Namespace, string key, Type fieldType)
        {
            var value = ReadEnvironmentVariable($"{Namespace}_{key}");
            if (value == null) return null;

            if (fieldType == typeof(bool))
            {
                return NaturalParseBool(value);
            }

            return value;
        }

        /**
         * Read an variable from the System environment,
         * ensuring that the key is in full caps.
         */
        private string ReadEnvironmentVariable(string key)
        {
            return Environment.GetEnvironmentVariable((Prefix + key).ToUpper());
        }

        private bool? NaturalParseBool(string text)
        {
            var trueish = new ArrayList {"true", "on", "yes", "1", ""};
            var falseish = new ArrayList { "false", "off", "no", "0" };
            if (trueish.Contains(text))
            {
                return true;
            }
            else if(falseish.Contains(text))
            {
                return false;
            }
            else
            {
                return null;
            }
        }
    }
}
