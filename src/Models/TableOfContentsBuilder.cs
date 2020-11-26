using System.Text;

namespace markdown_composer.Models
{
    public class TableOfContentsBuilder : ILine
    {
        private readonly StringBuilder _builder = new StringBuilder();
        public string MarkdownText { get => _builder.ToString(); }
        public TableOfContentsBuilder AddLine(LinkLine line)
        {
            if(line.IsTocCompatible)
            {
                for(int i = 0; i <= line.Level; ++i)
                {
                    _builder.Append('\t');
                }
                _builder.Append(' ').AppendLine(line.HeadingName);
            }
            return this;
        }
    }
}