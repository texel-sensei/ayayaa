using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ayayaa.core.config
{
    public class ConfigException : Exception
    {
        public ConfigException(string message) : base(message) { }
        public ConfigException(string message, Exception innerException) : base(message, innerException) { }
    };

    public interface IConfigSource
    {
        /**
         * Query a configuration value.
         *
         * Return the configuration value of a specific key in
         * the namespace. Return `null` if either namespace or
         * key are invalid.
         *
         */
        dynamic QueryValue(string Namespace, string key, Type fieldType);
    }

    public class ConfigManager
    {
        private readonly List<IConfigSource> _sources = new List<IConfigSource>();

        /**
         * Add a new source of config values to this manager.
         * Sources added first have priority over later added ones
         * (i.e. if two or more sources provide a value for the
         * same key, then the one added first wins).
         */
        public void AddConfigSource(IConfigSource source)
        {
            _sources.Add(source);
        }

        /**
         * Get an config object. The class used as
         * GenericsParameter needs to have the
         * `ConfigNamespace` Attribute.
         */
        public T GetConfigObject<T>() where T : new()
        {
            var configObject = new T();
            FillObject(configObject);
            return configObject;
        }

        private void FillObject(object configObject)
        {
            var configObjectType = configObject.GetType();

            var fields =
                from field in configObjectType.GetFields(BindingFlags.Public | BindingFlags.Instance)
                where !field.IsInitOnly
                select field;

            var Namespace = GetNamespace(configObjectType);

            foreach(var field in fields)
            {
                var key = field.Name;
                var value = QueryOption(Namespace, key, field.FieldType);

                if (value == null)
                {
                    continue;
                }

                if (value.GetType() == field.FieldType)
                {
                    field.SetValue(configObject, value);
                }
                else
                {
                    field.SetValue(configObject, Convert.ChangeType(value, field.FieldType));
                }
            }
        }

        private string GetNamespace(Type configObjectType)
        {
            var attrs = configObjectType.GetCustomAttributes(typeof(ConfigNamespaceAttribute), true);
            if (attrs.Length != 1)
            {
                throw new ConfigException($"Class {configObjectType.Name} is missing the ConfigNamespace attribute.");
            }

            var configInfo = attrs[0] as ConfigNamespaceAttribute;
            return configInfo.Namespace;
        }

        private dynamic QueryOption(string Namespace, string key, Type fieldType)
        {
            foreach (var source in _sources)
            {
                var value = source.QueryValue(Namespace, key, fieldType);
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }
    }
}