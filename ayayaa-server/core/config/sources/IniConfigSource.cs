using System;
using System.IO;
using IniParser;
using IniParser.Model;
using IniParser.Parser;


namespace ayayaa.core.config.sources
{
    /// <summary>
    /// This class provides config values by reading a
    /// ini document.
    /// </summary>
    public class IniConfigSource : IConfigSource
    {
        private IniData _data;

        /// <summary>
        /// Parses an *.ini file to construct a IniConfigSource
        /// </summary>
        public static IniConfigSource FromFile(string filename)
        {
            return new IniConfigSource()
            {
                _data = new FileIniDataParser().ReadFile(filename)
            };
        }

        /// <summary>
        /// Parse the content of a string as ini to construct a IniConfigSource.
        /// </summary>
        public static IniConfigSource FromString(string iniText)
        {
            return new IniConfigSource()
            {
                _data = new IniDataParser().Parse(iniText)
            };
        }

        public dynamic QueryValue(string Namespace, string key, Type fieldType)
        {
            return _data[Namespace][key];
        }

        // Use one of the static From* functions to create an IniConfigSource.
        private IniConfigSource() { }
    }
}
