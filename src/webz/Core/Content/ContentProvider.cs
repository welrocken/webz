using System;
using System.Collections.Generic;

namespace Webz.Core
{
    public class ContentProvider : IContentProvider
    {
        private readonly Dictionary<Type, Dictionary<long, string>> _xpaths;
        private readonly Dictionary<Type, Func<string, object>> _parsers;

        public ContentProvider()
        {
            _xpaths = new Dictionary<Type, Dictionary<long, string>>();
            _parsers = new Dictionary<Type, Func<string, object>>();
        }

        public bool CanParse<T>()
        {
            return CanParse(typeof(T));
        }

        public bool CanParse(Type type)
        {
            return _parsers.ContainsKey(type);
        }

        public string GetXpath<T>(long id)
        {
            return GetXpath(typeof(T), id);
        }

        public string GetXpath(Type type, long id)
        {
            if (!_xpaths.ContainsKey(type))
                return null;

            var contents = _xpaths[type];
            if (!contents.ContainsKey(id))
                return null;

            return contents[id];
        }

        public T Parse<T>(string value)
        {
            return (T)Parse(typeof(T), value);
        }

        public object Parse(Type type, string value)
        {
            if (!_parsers.ContainsKey(type))
                return type.IsValueType 
                    ? Activator.CreateInstance(type)
                    : null;

            var parser = _parsers[type];

            var obj = parser(value);

            return obj;
        }

        public void RegisterType<T>(Func<string, T> parser)
        {
            RegisterType(typeof(T), new Func<string, object>(x => parser(x)));
        }

        public void RegisterType(Type type, Func<string, object> parser)
        {
            if (!_parsers.ContainsKey(type))
                _parsers.Add(type,
                             new Func<string, object>(x => parser(x)));
            else
                _parsers[type] = new Func<string, object>(x => parser(x));

            if (!_xpaths.ContainsKey(type))
                _xpaths.Add(type, new Dictionary<long, string>());
        }

        public void RegisterType<T>()
        {
            RegisterType(typeof(T));
        }

        public void RegisterType(Type type)
        {
            if (!_xpaths.ContainsKey(type))
                _xpaths.Add(type, new Dictionary<long, string>());
        }

        public void SetXpath<T>(long id, string xpath)
        {
            SetXpath(typeof(T), id, xpath);
        }

        public void SetXpath(Type type, long id, string xpath)
        {
            if (!_xpaths.ContainsKey(type))
                return;

            var contents = _xpaths[type];
            if (!contents.ContainsKey(id))
                contents.Add(id, xpath);
            else
                contents[id] = xpath;
        }
    }
}