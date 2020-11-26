using System.Linq;

namespace markdown_composer.Models
{
    public class Composition
    {
        public ILine[] Lines { get; set; }
        public string Separator { get; set; } = "\n---\n";

        public string GetMarkdownString()
        {
            return Lines?
                .Select((line) => line.MarkdownText)
                .Aggregate((sum, next) => sum + Separator + next)
                ?? string.Empty;
        }
    }
}