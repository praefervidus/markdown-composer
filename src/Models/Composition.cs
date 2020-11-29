using System.Linq;
using System.Text;

namespace markdown_composer.Models
{
    public class Composition : ILine
    {
        public static readonly string DefaultSeparator = "\n---\n";
        public ILine[] Lines { get; set; }
        public string Separator { get; set; } = DefaultSeparator;
        public bool ShouldMakeToc { get; set; }

        public string MarkdownText { get => GetMarkdownString(); }

        public string GetMarkdownString()
        {
            var builder = new StringBuilder();
            if(ShouldMakeToc)
            {
                var tocBuilder = new TableOfContentsBuilder();
                for(int i = 0; i < Lines.Length; ++i)
                {
                    if(Lines[i] is LinkLine line) tocBuilder.AddLine(line);
                }
                builder.Append(tocBuilder.MarkdownText);
                builder.Append(Separator);
            }
            builder.Append(Lines?.Select((line) => line.MarkdownText)
                .Aggregate((sum, next) => sum + Separator + next)
                ?? string.Empty);
            return builder.ToString();
        }
    }
}