using System;
using CommandLine;
using Dir = System.IO.Path;

namespace FYA.Options
{
    public class ShortcutOptions
    {
        #region Var
        [Option('p', "path", Required = true, HelpText = "Application path")]
        public string Path { get; }

        [Option('i', "id", Required = true, SetName = "cardID", HelpText = "Video card's instance id")]
        public string ID { get; }

        [Option('n', "name", Required = true, SetName = "cardName", HelpText = "Video card's name")]
        public string Name { get; }

        [Option('w', "wait", HelpText = "The time the video card is disabled")]
        public int Wait { get; }

        [Option('d', "directory", HelpText = "Application working directory")]
        public string WorkingDirectory { get; }

        [Option('s', "save", HelpText = "Shortcut save location")]
        public string Save { get; }

        [Option("icon", HelpText = "Icon for shortcut")]
        public string Icon { get; }

        [Option('c', "cmd", HelpText = "Indicates whether to use the command line interface")]
        public bool ShowCommandLine { get; }
        #endregion

        #region Init
        public ShortcutOptions(string Path, string ID, string Name, int Wait, string WorkingDirectory, string Save, string Icon, bool ShowCommandLine)
        {
            if (!string.IsNullOrEmpty(Path))
            {
                this.ID = ID;
                this.Name = Name;
                this.Wait = Wait;
                this.Path = Path;
                this.WorkingDirectory = string.IsNullOrWhiteSpace(WorkingDirectory) ? Dir.GetDirectoryName(Dir.Combine(Environment.CurrentDirectory, Path)) : WorkingDirectory;
                Save = string.IsNullOrEmpty(Save) ? Dir.Combine(Environment.CurrentDirectory, Dir.GetFileNameWithoutExtension(Path)) : Save;
                this.Save = Dir.HasExtension(Save) ? Save : $"{Save}.lnk";
                this.Icon = string.IsNullOrEmpty(Icon) ? $"{this.Path},0" : Icon;
                this.ShowCommandLine = ShowCommandLine;
            }
        }
        #endregion
    }
}
