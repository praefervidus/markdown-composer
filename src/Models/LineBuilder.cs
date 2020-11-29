using System.Linq;
using System.Text.RegularExpressions;

namespace markdown_composer.Models
{
    public class LineBuilder
    {
        private const string Pattern = @"(.*)\[(.*)\]\((.*)\)";
        public ILine Line { get => _line; }
        private ILine _line;
        public string ProjectPath { get; }
        public LineBuilder(string projectPath = "")
        {
            ProjectPath = projectPath;
        }
        public LineBuilder FromUnParsedLine(string text)
        {
            var match = new Regex(Pattern).Match(text);
            if(match.Success && match.Groups.Count == 4)
            {
                var listCharacters = new char[]{'+', '-', '*'};
                var beforeText = match.Groups[1].ToString();
                var indentation = beforeText.Count((character) => listCharacters.Contains(character));
                var isTocCompatible = indentation > 0;
                var headingText = match.Groups[2].ToString();
                var linkText = match.Groups[3].ToString();
                _line = new LinkLine(ProjectPath, headingText, linkText, isTocCompatible, indentation);
            }
            else{
                _line = new MarkdownLine { MarkdownText = text };
            }
            return this;
        }
    }
}