using System;
using System.IO;
using FYA.Models;
using FYA.Options;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace FYA.Programs
{
    public class CrutchRunProgram : BaseProgram<CrutchRunOptions>
    {
        public override string Usage => "fya crutchrun --path \"Foo.exe\" --name Intel";
        public override string Description => "Runs your program with a temporary video card disabling";

        public override void Run(CrutchRunOptions Options)
        {
            if (File.Exists(Options.Path))
            {
                string id = Options.ID.ToLower();
                string name = Options.Name.ToLower();

                Func<VideoCard, bool> predicate = string.IsNullOrEmpty(id)
                    ? new Func<VideoCard, bool>(x => x.Name.ToLower().Contains(name))
                    : x => x.ID.ToLower().Contains(id) || id.Contains(x.ID.ToLower());

                VideoCard card = VideoCard.EnumerateVideoCards().FirstOrDefault(predicate);
                if (card is null)
                {
                    PrintException("The specified video card was not found");
                }
                else
                {
                    ProcessStartInfo info = new ProcessStartInfo {
                        FileName = Options.Path,
                        WorkingDirectory = Options.WorkingDirectory
                    };
                    try
                    {
                        card.Disable();
                        Thread.Sleep(100);
                        Process.Start(info);
                        Thread.Sleep(Options.Wait);
                        card.Enable();
                    }
                    catch (Exception e)
                    {
                        PrintException(e);
                    }
                }
            }
            else
                PrintException($"File doesn't exist: {Options.Path}");
        }
    }
}
