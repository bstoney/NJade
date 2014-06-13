namespace NJade.Lexer
{
    using NJade.Lexer.Tokenizer;

    /// <summary>
    /// Defines the JadeTokenType class.
    /// </summary>
    public static class JadeTokenType
    {
        /// <summary>
        /// The equals.
        /// </summary>
        public new static readonly TokenType Equals = new TokenType("Equals");

        /// <summary>
        /// The hash tag
        /// </summary>
        public static readonly TokenType HashTag = new TokenType("HashTag");

        /// <summary>
        /// The dot
        /// </summary>
        public static readonly TokenType Dot = new TokenType("Dot");

        /// <summary>
        /// The doctype
        /// </summary>
        public static readonly TokenType Doctype = new TokenType("DocType");

        /// <summary>
        /// The new line
        /// </summary>
        public static readonly TokenType NewLine = new TokenType("NewLine");

        /// <summary>
        /// The colon
        /// </summary>
        public static readonly TokenType Colon = new TokenType("Colon");

        /// <summary>
        /// The open parenth
        /// </summary>
        public static readonly TokenType OpenParenth = new TokenType("OpenParenth");

        /// <summary>
        /// The close parenth
        /// </summary>
        public static readonly TokenType CloseParenth = new TokenType("ClodeParenth");

        /// <summary>
        /// The comma
        /// </summary>
        public static readonly TokenType Comma = new TokenType("Comma");

        /// <summary>
        /// The minus
        /// </summary>
        public static readonly TokenType Minus = new TokenType("Minus");

        /// <summary>
        /// The pipe
        /// </summary>
        public static readonly TokenType Pipe = new TokenType("Pipe");

        /// <summary>
        /// The bang
        /// </summary>
        public static readonly TokenType Bang = new TokenType("Bang");

        /// <summary>
        /// The comment
        /// </summary>
        public static readonly TokenType Comment = new TokenType("Comment");

        /// <summary>
        /// If
        /// </summary>
        public static readonly TokenType If = new TokenType("If");

        /// <summary>
        /// The each
        /// </summary>
        public static readonly TokenType Each = new TokenType("Each");

        /// <summary>
        /// The variable
        /// </summary>
        public static readonly TokenType Var = new TokenType("Var");

        /// <summary>
        /// The else
        /// </summary>
        public static readonly TokenType Else = new TokenType("Else");

        /// <summary>
        /// The case
        /// </summary>
        public static readonly TokenType Case = new TokenType("Case");

        /// <summary>
        /// The when
        /// </summary>
        public static readonly TokenType When = new TokenType("When");

        /// <summary>
        /// The default
        /// </summary>
        public static readonly TokenType Default = new TokenType("Default");

        /// <summary>
        /// The mixin
        /// </summary>
        public static readonly TokenType Mixin = new TokenType("Mixin");

        /// <summary>
        /// The plus
        /// </summary>
        public static readonly TokenType Plus = new TokenType("Plus");

        /// <summary>
        /// The include
        /// </summary>
        public static readonly TokenType Include = new TokenType("Include");

        /// <summary>
        /// The open bracket
        /// </summary>
        public static readonly TokenType OpenBracket = new TokenType("OpenBracket");

        /// <summary>
        /// The close bracket
        /// </summary>
        public static readonly TokenType CloseBracket = new TokenType("CloseBracket");

        /// <summary>
        /// The class
        /// </summary>
        public static readonly TokenType Class = new TokenType("Class");

        /// <summary>
        /// The jade comment.
        /// </summary>
        public static TokenType JadeComment = new TokenType("JadeComment");

        /// <summary>
        /// The block
        /// </summary>
        public static TokenType Block = new TokenType("Block");

        /// <summary>
        /// The inline value
        /// </summary>
        public static TokenType InlineValue = new TokenType("InlineValue");
    }
}
