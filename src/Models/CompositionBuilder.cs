namespace markdown_composer.Models
{
    public class CompositionBuilder : ILine
    {
        public string MarkdownText { get; private set; }
        public CompositionBuilder(string referenceFilePath)
        {
            // TODO: read in the file and parse
        }
    }
}