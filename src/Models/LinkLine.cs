using System;
using System.IO;
using System.Text;

namespace markdown_composer.Models
{
    public class LinkLine : ILine
    {
        public string MarkdownText { get => GetMarkdownText(); }
        public bool IsTocCompatible { get; set; }

        public string HeadingName { get; }
        private string _link;
        private int _level;

        public LinkLine(string headingName, string link, bool isTocCompatible, int level=0)
        {
            HeadingName = headingName;
            _link = link;
            IsTocCompatible = isTocCompatible; // used for telling if this link should be mentioned in the table of contents
            _level = level; // used for figuring out how many '#'s to have in heading
        }
        private string GetMarkdownText()
        {
            try
            {
                var builder = new StringBuilder();
                for(int i = 0; i < _level; ++i){ builder.Append('#'); }
                builder.Append($" {HeadingName}\n");

                using (var fs = new FileStream(_link, FileMode.Open, FileAccess.Read))
                using (var sr = new StreamReader(fs))
                {
                    builder.Append(sr.ReadToEnd());
                }
                return builder.ToString();
            }
            catch(FileNotFoundException)
            {
                Console.Error.WriteLine($"Error! Cannot find file: {_link}");
                return string.Empty;
            }
        }
    }
}