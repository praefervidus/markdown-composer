using System.Collections.Generic;
using System.IO;

namespace markdown_composer.Models
{
    public class CompositionBuilder
    {
        public Composition Composition
        {
            get => new Composition()
            {
                Lines = _lines.ToArray(),
                Separator = _separator,
                ShouldMakeToc = _makeToc
            };
        }
        private readonly List<ILine> _lines = new List<ILine>();
        private string _separator = Composition.DefaultSeparator;
        private readonly bool _makeToc;

        public CompositionBuilder(bool makeToc = false)
        {
            _makeToc = makeToc;
        }
        public CompositionBuilder FromReferenceFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs))
            {
                while(!sr.EndOfStream)
                {
                    AddLine(sr.ReadLine());
                }
            }
            return this;
        }
        public CompositionBuilder SetSeparator(string separator)
        {
            _separator = separator;
            return this;
        }
        public CompositionBuilder AddLine(ILine line)
        {
            _lines.Add(line);
            return this;
        }
        public CompositionBuilder AddLine(string unparsedText)
        {
            var line = new LineBuilder()
                .FromUnParsedLine(unparsedText)
                .Line;
            return AddLine(line);
        }
    }
}