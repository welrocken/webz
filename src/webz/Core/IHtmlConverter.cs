using System.Xml;

namespace Webz.Core
{
    public interface IHtmlConverter
    {
        XmlDocument ToXmlDocument(string html);
    }
}