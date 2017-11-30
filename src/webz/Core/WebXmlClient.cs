using System;
using System.Net;
using System.Xml;

namespace Webz.Core
{
    public class WebXmlClient : IXmlClient
    {
        private readonly IHtmlConverter _htmlConverter;
        private readonly WebClient _webClient;

        public WebXmlClient(IHtmlConverter htmlConverter)
        {
            _htmlConverter = htmlConverter ?? throw new ArgumentNullException(nameof(htmlConverter));
            _webClient = new WebClient();

        }

        public XmlDocument Read(string address)
        {
            string html = _webClient.DownloadString(address);
            return _htmlConverter.ToXmlDocument(html);
        }
    }
}