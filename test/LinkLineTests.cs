using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using markdown_composer.Models;
using Xunit;

namespace markdown_composer.Tests
{
    public class LinkLineTests
    {
        private LinkLine _line;
        private string output;

        [Fact]
        public void VerifyLinkMarkdownIsCorrect()
        {
            GivenLine(new LinkLine("TestTitle", "resources/linkLineTests.md", true, 0));
            WhenLineIsGrabbed();
            ThenOutputIsEqualTo(
                "# TestTitle\n## My Life For Auir\r\n- Have you ever played StarCraft 2?\r\nI loved playing SC1 when I was a child. RTS games are fun!"
            );
        }
        private void GivenLine(LinkLine line)
        {
            _line = line;
        }
        private void WhenLineIsGrabbed()
        {
            output = _line.MarkdownText;
        }
        private void ThenOutputIsEqualTo(string expectedOutput)
        {
            output.Should().BeEquivalentTo(expectedOutput);
        }
    }
}