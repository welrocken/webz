using System.Xml;

namespace Webz.Core
{
    public interface IXmlClient
    {
        XmlDocument Read(string address);
    }
}