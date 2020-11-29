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
                new LinkLine("", "Part 2", "", true, 0)
            });
            WhenBuilderIsRun();
            ThenOutputIs(
                $"## Table of Contents{Environment.NewLine}* Part 1{Environment.NewLine}\t* Chapter 1{Environment.NewLine}\t\t* Scene 1{Environment.NewLine}\t* Chapter 2{Environment.NewLine}* Part 2{Environment.NewLine}"
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
                new LinkLine("", "Part 2", "", true, 0)
            });
            WhenBuilderIsRun();
            ThenOutputIs(
                $"## Table of Contents{Environment.NewLine}* Part 1{Environment.NewLine}\t* Chapter 1{Environment.NewLine}\t* Chapter 2{Environment.NewLine}* Part 2{Environment.NewLine}"
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
