using System;
using FluentAssertions;
using markdown_composer.Models;
using Xunit;

namespace markdown_composer.Tests
{
    public class TableOfContentsBuilderTests
    {
        private ILine[] _lines;
        private readonly TableOfContentsBuilder _builder;

        public TableOfContentsBuilderTests()
        {
            _builder = new TableOfContentsBuilder();
        }

        [Fact]
        public void VerifyCorrectTocMade()
        {
            GivenLines(new ILine[]
            {
                new LinkLine("", "Part 1", "", true, 0),
                new LinkLine("", "Chapter 1", "", true, 1),
                new LinkLine("", "Scene 1", "", true, 2),
                new LinkLine("", "Chapter 2", "", true, 1),
                new LinkLine("", "Part 2", "", true, 0),
                new LinkLine("", "Chapter 1", "", true, 1),
                new LinkLine("", "Chapter 2", "", true, 1)
            });
            WhenBuilderIsRun();
            ThenOutputIs(
                "## Table of Contents\r\n* Part 1\r\n\t* Chapter 1\r\n\t\t* Scene 1\r\n\t* Chapter 2\r\n* Part 2\r\n\t* Chapter 1\r\n\t* Chapter 2\r\n"
            );
        }

        [Fact]
        public void VerifyNonTocFiltered()
        {
            GivenLines(new ILine[]
            {
                new LinkLine("", "Part 1", "", true, 0),
                new LinkLine("", "Chapter 1", "", true, 1),
                new LinkLine("", "Scene 1", "", false, 2),
                new LinkLine("", "Chapter 2", "", true, 1),
                new LinkLine("", "Part 2", "", true, 0),
                new LinkLine("", "Chapter 1", "", true, 1),
                new LinkLine("", "Chapter 2", "", false, 1)
            });
            WhenBuilderIsRun();
            ThenOutputIs(
                "## Table of Contents\r\n* Part 1\r\n\t* Chapter 1\r\n\t* Chapter 2\r\n* Part 2\r\n\t* Chapter 1\r\n"
            );
        }

        private void GivenLines(ILine[] lines)
        {
            _lines = lines;
        }
        private void WhenBuilderIsRun()
        {
            foreach(ILine line in _lines)
            {
                if(line is LinkLine linkLine) _builder.AddLine(linkLine);
            }
        }

        private void ThenOutputIs(string expectedOutput)
        {
            _builder.MarkdownText.Should().BeEquivalentTo(expectedOutput);
        }
    }
}
