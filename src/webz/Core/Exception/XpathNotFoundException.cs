using System;

namespace Webz.Core.Exception
{
    public class XpathNotFoundException : System.Exception
    {
        public Type Type { get; }
        public long Id { get; }

        public XpathNotFoundException(Type type, long id)
        {
            Type = type;
            Id = id;
        }
    }

    public class XpathNotFoundException<T> : XpathNotFoundException
    {
        public XpathNotFoundException(long id)
            : base(typeof(T), id)
        { }
    }
}