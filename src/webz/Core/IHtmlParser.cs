using System;
using System.Xml;

namespace Webz.Core
{
    public interface IHtmlParser
    {
        IContentProvider ContentProvider { get; }
        XmlDocument Context { get; set; }

        ContentResult GetContent(Type type, long id);
        ContentResult<T> GetContent<T>(long id);
    }
}