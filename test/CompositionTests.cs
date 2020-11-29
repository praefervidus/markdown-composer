using System;
using FluentAssertions;
using markdown_composer.Models;
using Xunit;

namespace markdown_composer.Tests
{
    public class CompositionTests
    {
        private const string ProjectPath = "resources/composition-tests";
        private readonly CompositionBuilder _builder;
        private bool _makeToc;
        private readonly string ExpectedTitle = $"# Title{Environment.NewLine}front matter...{Environment.NewLine}---{Environment.NewLine}";
        private readonly string ExpectedToc = $"## Table of Contents{Environment.NewLine}\t* Part 1{Environment.NewLine}\t\t* Chapter 1{Environment.NewLine}\t\t* Chapter 2{Environment.NewLine}\t* Part 2{Environment.NewLine}\t\t* Chapter 1{Environment.NewLine}\t\t* Chapter 2{Environment.NewLine}\t* Epilogue{Environment.NewLine}\t* Glossary{Environment.NewLine}{Environment.NewLine}---{Environment.NewLine}";
        private readonly string ExpectedContents = @"## Part 1

---
### Chapter 1
Chapter 1 content...
---
### Chapter 2
Chapter 2 content...
---
## Part 2
part 2 intro text...
---
### Chapter 1
Chapter 1 content...
---
### Chapter 2
Chapter 2 content...
---
## Epilogue
Epilogue content...
---
## Glossary
glossary content...
---
# OptionalEndingText
end matter...
---
";
        private readonly string ExpectedWithoutToc, ExpectedWithToc;

        public CompositionTests()
        {
            _builder = new CompositionBuilder(ProjectPath);
            ExpectedWithToc = ExpectedTitle + ExpectedToc + ExpectedContents;
            ExpectedWithoutToc = ExpectedTitle + ExpectedContents;
        }

        [Fact]
        public void VerifyWithToc()
        {
            GivenShouldMakeToc();
            WhenBuilderIsRun();
            ThenResultsMetExpectations();
        }
        [Fact]
        public void VerifyWithoutToc()
        {
            GivenShouldNotMakeToc();
            WhenBuilderIsRun();
            ThenResultsMetExpectations();
        }

        private void GivenShouldMakeToc()
        {
            _builder.ShouldMakeToc(true);
            _makeToc = true;
        }

        private void GivenShouldNotMakeToc()
        {
            _builder.ShouldMakeToc(false);
            _makeToc = false;
        }
        private void WhenBuilderIsRun()
        {
            _builder.FromReferenceFile(Program.ReferenceFileName);
        }
        private void ThenResultsMetExpectations()
        {
            if(_makeToc)
            {
                _builder.Composition.MarkdownText.Should().BeEquivalentTo(ExpectedWithToc);
            }
            else
            {
                _builder.Composition.MarkdownText.Should().BeEquivalentTo(ExpectedWithoutToc);
            }
        }
    }
}