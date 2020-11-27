using System.Linq;
using System.Text.RegularExpressions;

namespace markdown_composer.Models
{
    public class LineBuilder
    {
        private const string Pattern = @"(.*)\[(.+)\]\((.+)\)";
        public ILine Line { get => _line; }
        private ILine _line;
        public LineBuilder FromUnParsedLine(string text)
        {
            var match = new Regex(Pattern).Match(text);
            if(match.Success && match.Groups.Count == 4)
            {
                var listCharacters = new char[]{'+', '-', '*'};
                var beforeText = match.Groups[1].ToString();
                bool isTocCompatible = beforeText.Count((character) => listCharacters.Contains(character)) > 0;
                var indendation = beforeText.Count((character) => character == '\t');
                var headingText = match.Groups[2].ToString();
                var linkText = match.Groups[3].ToString();
                _line = new LinkLine(headingText, linkText, isTocCompatible, indendation);
            }
            else{
                _line = new MarkdownLine { MarkdownText = text };
            }
            return this;
        }
    }
}