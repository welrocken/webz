using System;
using System.Xml;

using Webz.Core.Exception;

namespace Webz.Core
{
    public class HtmlParser : IHtmlParser
    {
        public IContentProvider ContentProvider { get; }
        public bool ThrowsOnNotFound { get; set; }
        public XmlDocument Context { get; set; }

        public HtmlParser(IContentProvider contentProvider)
        {
            ContentProvider = contentProvider ?? throw new ArgumentNullException(nameof(contentProvider));
        }

        public ContentResult<T> GetContent<T>(long id)
        {
            var xpath = ContentProvider.GetXpath<T>(id);
            if (xpath == null)
                return ThrowsOnNotFound 
                    ? (ContentResult<T>)null
                    : throw new XpathNotFoundException<T>(id);
            
            var contentNode = Context.SelectSingleNode(xpath);
            var contentText = contentNode.InnerText;

            if (!ContentProvider.CanParse<T>())
                return new ContentResult<T>(contentText);

            return new ContentResult<T>(ContentProvider.Parse<T>(contentText));
        }

        public ContentResult GetContent(Type type, long id)
        {
            var xpath = ContentProvider.GetXpath(type, id);
            if (xpath == null)
                return ThrowsOnNotFound
                    ? (ContentResult)null
                    : throw new XpathNotFoundException(type, id);

            var contentNode = Context.SelectSingleNode(xpath);
            var contentText = contentNode.InnerText;

            if (!ContentProvider.CanParse(type))
                return new ContentResult(contentText);

            return new ContentResult(ContentProvider.Parse(type, contentText));
        }
    }
}