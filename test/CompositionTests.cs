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
        private const string ExpectedWithToc = @"# Title
front matter...
## Table of Contents
	* Part 1
		* Chapter 1
		* Chapter 2
	* Part 2
		* Chapter 1
		* Chapter 2
	* Epilogue
	* Glossary

## Part 1

### Chapter 1
Chapter 1 content...
### Chapter 2
Chapter 2 content...
## Part 2
part 2 intro text...
### Chapter 1
Chapter 1 content...
### Chapter 2
Chapter 2 content...
## Epilogue
Epilogue content...
## Glossary
glossary content...
# OptionalEndingText
end matter...
";
        private const string ExpectedWithoutToc = @"# Title
front matter...
## Part 1

### Chapter 1
Chapter 1 content...
### Chapter 2
Chapter 2 content...
## Part 2
part 2 intro text...
### Chapter 1
Chapter 1 content...
### Chapter 2
Chapter 2 content...
## Epilogue
Epilogue content...
## Glossary
glossary content...
# OptionalEndingText
end matter...
";
        public CompositionTests()
        {
            _builder = new CompositionBuilder(ProjectPath);
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
            const string path = "SUMMARY.md";
            _builder
                .SetSeparator("\r\n")
                .FromReferenceFile(path);
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