using System;
using FYA.Models;
using FYA.Options;
using FYA.PInvoke;
using System.Linq;

namespace FYA.Programs
{
    public class SwitchProgram : BaseProgram<SwitchOptions>
    {
        public override string Usage => "fya switch --name Intel -d";
        public override string Description => "Enables or disables the specified video card";

        public override void Run(SwitchOptions Options)
        {
            string id = Options.ID.ToLower();
            string name = Options.Name.ToLower();

            Func<VideoCard, bool> predicate = string.IsNullOrEmpty(id)
                ? new Func<VideoCard, bool>(x => x.Name.ToLower().Contains(name))
                : x => x.ID.ToLower().Contains(id) || id.Contains(x.ID.ToLower());

            VideoCard card = VideoCard.EnumerateVideoCards().FirstOrDefault(predicate);
            if (card is null)
            {
                if (!string.IsNullOrEmpty(Options.ID))
                    try
                    {
                        SetupAPI.ChangeDeviceState(VideoCard.VideoAdaptersClass, Options.ID, !Options.Disable);
                        return;
                    } catch { }
                PrintException("The specified video card was not found");
            }
            else
            {
                try
                {
                    if (Options.Disable)
                        card.Disable();
                    else
                        card.Enable();
                }
                catch (Exception e)
                {
                    PrintException(e);
                }
            }
        }
    }
}
