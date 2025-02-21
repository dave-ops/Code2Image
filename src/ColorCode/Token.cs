// Token.cs
namespace ColorCode
{
    public class Token
    {
        public string Text { get; }
        public string Scope { get; }

        public Token(string text, string scope)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
    }
}