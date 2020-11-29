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
        private bool _makeToc;
        public string ProjectPath { get; } = string.Empty;
        private readonly LineBuilder _lineBuilder;

        public CompositionBuilder(string projectPath, bool makeToc = false)
        {
            ProjectPath = projectPath;
            _makeToc = makeToc;
            _lineBuilder = new LineBuilder(projectPath);
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
        public CompositionBuilder ShouldMakeToc(bool makeToc)
        {
            _makeToc = makeToc;
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
            return AddLine(_lineBuilder.FromUnParsedLine(unparsedText).Line);
        }
    }
}