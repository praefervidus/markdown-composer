using System.Text;

namespace markdown_composer.Models
{
    public class Composition : ILine
    {
        public static readonly string DefaultSeparator = $"{System.Environment.NewLine}---{System.Environment.NewLine}";
        public ILine[] Lines { get; set; }
        public string Separator { get; set; } = DefaultSeparator;
        public bool ShouldMakeToc { get; set; }

        public string MarkdownText { get => GetMarkdownString(); }

        public string GetMarkdownString()
        {
            if(Lines.Length < 1) return string.Empty;
            var builder = new StringBuilder();
            builder.Append(Lines[0].MarkdownText).Append(Separator);
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
            for(int i = 1; i < Lines.Length; ++i)
            {
                builder.Append(Lines[i].MarkdownText).Append(Separator);
            }
            return builder.ToString();
        }
    }
}