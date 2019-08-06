using System;
using FYA.Models;
using FYA.Options;
using System.Linq;

namespace FYA.Programs
{
    public class CardProgram : BaseProgram<CardOptions>
    {
        public override string Usage => "fya card --name Intel";
        public override string Description => "Searches for a specified video card or displays all";

        public override void Run(CardOptions Options)
        {
            string id = Options.ID.ToLower();
            string name = Options.Name.ToLower();

            Func<VideoCard, bool> predicate = !string.IsNullOrEmpty(name)
                ? x => x.Name.ToLower().Contains(name)
                : !string.IsNullOrEmpty(id)
                ? x => x.ID.ToLower().Contains(id) || id.Contains(x.ID.ToLower())
                : new Func<VideoCard, bool>(x => true);

            Console.WriteLine();
            foreach (VideoCard card in VideoCard.EnumerateVideoCards().Where(predicate))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}:", card.Name);
                Console.ResetColor();
                Console.WriteLine("ID:      {0}", card.ID);
                Console.WriteLine("State:   {0}", card.StateFlags);
                Console.WriteLine("Devices: {0}", string.Join(", ", card.Devices.Select(x => $"\"{x}\"")));
                Console.WriteLine("Keys:    {0}", string.Join(", ", card.RegistryKeys.Select(x => $"\"{x}\"")));
                Console.WriteLine();
            }
        }
    }
}