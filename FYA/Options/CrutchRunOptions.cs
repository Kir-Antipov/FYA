using System;
using CommandLine;
using Dir = System.IO.Path;

namespace FYA.Options
{
    public class CrutchRunOptions 
    {
        [Option('i', "id", Required = true, SetName = "cardID", HelpText = "Video card's instance id")]
        public string ID { get; }

        [Option('n', "name", Required = true, SetName = "cardName", HelpText = "Video card's name")]
        public string Name { get; }

        [Option('w', "wait", HelpText = "The time the video card is disabled")]
        public int Wait { get; }

        [Option('p', "path", Required = true, HelpText = "Application path")]
        public string Path { get; }

        [Option('d', "directory", HelpText = "Application working directory")]
        public string WorkingDirectory { get; }

        public CrutchRunOptions(string ID, string Name, int Wait, string Path, string WorkingDirectory)
        {
            if (!string.IsNullOrEmpty(Path))
            {
                this.ID = ID ?? string.Empty;
                this.Name = Name ?? string.Empty;
                this.Wait = Wait < 1 ? 5000 : Wait;

                Path = Dir.Combine(Environment.CurrentDirectory, Path);

                this.Path = Path;
                this.WorkingDirectory = string.IsNullOrWhiteSpace(WorkingDirectory) ? Dir.GetDirectoryName(Path) : WorkingDirectory;
            }
        }
    }
}
