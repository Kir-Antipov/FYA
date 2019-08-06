using System;
using System.IO;
using CommandLine;
using FYA.Options;
using IWshRuntimeLibrary;

namespace FYA.Programs
{
    public class ShortcutProgram : BaseProgram<ShortcutOptions>
    {
        public override string Usage => "fya shortcut --path \"Foo.exe\" --name Intel";
        public override string Description => "Creates a shortcut to crutchrun program";

        public override void Run(ShortcutOptions Options)
        {
            string location = Path.GetDirectoryName(new Uri(typeof(Program).Assembly.CodeBase, UriKind.Absolute).LocalPath);

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Options.Save);
            shortcut.TargetPath = Path.Combine(location, $"{(Options.ShowCommandLine ? "FYA" : "HFYA")}.exe");
            shortcut.WorkingDirectory = location;
            shortcut.IconLocation = Options.Icon;
            shortcut.Arguments = $"crutchrun {Parser.Default.FormatCommandLine(new CrutchRunOptions(Options.ID, Options.Name, Options.Wait, Options.Path, Options.WorkingDirectory))}";
            shortcut.Save();
        }
    }
}
