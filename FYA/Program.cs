using System;
using System.Linq;
using FYA.Programs;

namespace FYA
{
    class Program
    {
        public static IProgram[] Programs { get; private set; }

        public static void Main(string[] args)
        {
            args = args ?? new string[0];
            if (args.Length < 1)
                args = new[] { "help" };

            Programs = typeof(IProgram).Assembly.GetTypes()
               .Where(typeof(IProgram).IsAssignableFrom)
               .Where(x => !x.IsInterface && !x.IsAbstract)
               .Where(x => x.GetConstructor(Type.EmptyTypes) != null)
               .Select(Activator.CreateInstance).Cast<IProgram>()
               .ToArray();

            IProgram program = Programs.FirstOrDefault(x => x.Name.Equals(args[0], StringComparison.InvariantCultureIgnoreCase)) ?? new HelpProgram();

            program.Run(args.Skip(1));
        }
    }
}