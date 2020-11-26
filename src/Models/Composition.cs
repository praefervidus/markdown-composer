using System.Linq;
using System.Text;

namespace markdown_composer.Models
{
    public class Composition
    {
        public static readonly string DefaultSeparator = "\n---\n";
        public ILine[] Lines { get; set; }
        public string Separator { get; set; } = DefaultSeparator;
        public bool ShouldMakeToc { get; set; }

        public string GetMarkdownString()
        {
            var builder = new StringBuilder();
            if(ShouldMakeToc)
            {
                // TODO: add table of contents
                builder.AppendLine();
            }
            builder.Append(Lines?.Select((line) => line.MarkdownText)
                .Aggregate((sum, next) => sum + Separator + next)
                ?? string.Empty);
            return builder.ToString();
        }
    }
}