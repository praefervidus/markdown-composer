using FluentAssertions;
using markdown_composer.Models;
using Xunit;

namespace markdown_composer.Tests
{
    public class LineBuilderTests
    {
        private ILine _line;

        [Theory]
        [InlineData("adsf [test](link)..")]
        [InlineData("* [James Cameron](imdb link)")]
        [InlineData("\t* [Huey](Tocalli)")]
        [InlineData("\t\t* [Grinder](Sandwich)")]
        public void ParseLinkLineTest(string unparsedText)
        {
            WhenBuilderParses(unparsedText);
            ThenLineIsLink();
        }

        [Theory]
        [InlineData("lkajsd;lkjfd")]
        [InlineData("## Heading 2")]
        [InlineData("*** Bold Text ***")]
        [InlineData("Regular normal text to read")]
        [InlineData("+ List text")]
        [InlineData("adsf (test)[123]..")]
        public void ParseMarkdownLineTest(string unparsedText)
        {
            WhenBuilderParses(unparsedText);
            ThenLineIsMarkdown();
        }

        private void WhenBuilderParses(string text)
        {
            _line = new LineBuilder().FromUnParsedLine(text).Line;
        }
        private void ThenLineIsMarkdown()
        {
            _line.Should().BeOfType<MarkdownLine>();
        }
        private void ThenLineIsLink()
        {
            _line.Should().BeOfType<LinkLine>();
        }
    }
}