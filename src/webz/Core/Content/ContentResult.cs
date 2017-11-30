namespace Webz.Core
{
    public class ContentResult<T>
    {
        public string Text { get; }
        public T Value { get; }

        public ContentResult(string text)
        {
            Text = text;
        }

        public ContentResult(T value)
        {
            Value = value;
        }
    }

    public class ContentResult : ContentResult<object>
    {
        public ContentResult(object value)
            : base(value)
        { }

        public ContentResult(string text) 
            : base(text)
        { }
    }
}