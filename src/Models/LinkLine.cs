using System;
using System.IO;

namespace markdown_composer.Models
{
    public class LinkLine : ILine
    {
        public string MarkdownText { get => GetMarkdownText(); }
        public bool IsTocCompatible { get; set; }

        private string _headingName, _link;
        private int _level;

        public LinkLine(string headingName, string link, bool isTocCompatible, int level=0)
        {
            _headingName = headingName;
            _link = link;
            IsTocCompatible = isTocCompatible; // used for telling if this link should be mentioned in the table of contents
            _level = level; // used for figuring out how many '#'s to have in heading
        }
        private string GetMarkdownText()
        {
            if(File.Exists(_link))
            {
                // TODO: get markdown text from file
            }
            else if(Directory.Exists(_link))
            {
                // TODO: perform sub-directory composition
            }
            else
            {
                Console.Error.WriteLine($"Error! Cannot find link: {_link}");
            }
        }
    }
}