using System.Text;

namespace markdown_composer.Models
{
    public class TableOfContentsBuilder : ILine
    {
        private StringBuilder _builder = new StringBuilder();
        public string MarkdownText { get => _builder.ToString(); }
        public TableOfContentsBuilder AddLine(LinkLine line)
        {
            if(line.IsTocCompatible) _builder.AppendLine(line.HeadingName);
            return this;
        }
    }
}