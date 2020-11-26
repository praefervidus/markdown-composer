using System;
using System.IO;
using markdown_composer.Models;

namespace markdown_composer
{
    internal static class Program
    {
        public static readonly string ReferenceFileName = "SUMMARY.md";
        public static readonly string DefaultWritePath = "composition.md";

        private static void ComposeGivenRootFolder(string rootFolder)
        {
            var builder = new CompositionBuilder($"{rootFolder}/{ReferenceFileName}");
            // TODO: write output from builder to the output file
        }
        private static void Main(string[] args) // args[0] = project root folder
        {
            if(args.Length > 0) // args provided
            {
                if(Directory.Exists(args[0]))
                {
                    if(File.Exists($"{args[0]}/{ReferenceFileName}"))
                    {
                        ComposeGivenRootFolder(args[0]); // compose on given root folder
                    }
                    else
                    {
                        Console.Error.WriteLine($"Error! The file '{args[0]}/{ReferenceFileName}' does not exist!");
                    }
                }
                else{
                    Console.Error.WriteLine($"Error! The folder '{args[0]}' does not exist!");
                }
            }
            else // no arguments provided
            {
                if(File.Exists(ReferenceFileName))
                {
                    ComposeGivenRootFolder(Path.GetDirectoryName(ReferenceFileName)); // do composition from within current folder
                }
                else{
                    Console.Error.WriteLine($"Error! No project root directory provided, and no {ReferenceFileName} found in current directory.");
                }
            }
        }
    }
}
