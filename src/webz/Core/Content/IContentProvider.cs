using System;

namespace Webz.Core
{
    public interface IContentProvider
    {
        string GetXpath<T>(long id);
        string GetXpath(Type type, long id);
        void SetXpath<T>(long id, string xpath);
        void SetXpath(Type type, long id, string xpath);
        bool CanParse(Type type);
        bool CanParse<T>();
        object Parse(Type type, string value);
        T Parse<T>(string value);
        void RegisterType(Type type, Func<string, object> parser);
        void RegisterType<T>(Func<string, T> parser);
        void RegisterType(Type type);
        void RegisterType<T>();
    }
}