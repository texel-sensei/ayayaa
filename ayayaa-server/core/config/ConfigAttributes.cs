using System;

namespace ayayaa.core.config
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigNamespaceAttribute : Attribute
    {
        public ConfigNamespaceAttribute(string ns)
        {
            if (string.IsNullOrWhiteSpace(ns))
            {
                throw new ArgumentException(
                    $"Invalid configuration namespace |{ns}|"
                );
            }
            Namespace = ns;
        }

        public string Namespace { get; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class RequiredAttribute : Attribute{}

}
