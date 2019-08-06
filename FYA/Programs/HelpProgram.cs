using System;
using System.Linq;
using System.Collections.Generic;

namespace FYA.Programs
{
    public class HelpProgram : BaseProgram
    {
        #region Var
        public override string Description => "Displays this help page";
        #endregion

        #region Functions
        public override void Run(IEnumerable<string> Arguments)
        {
            int maxNameLength = Program.Programs.Max(x => x.Name.Length);
            string format = $"{{0, -{maxNameLength + 10}}}{{1}}";
            Console.WriteLine();
            foreach (IProgram program in Program.Programs.OrderBy(x => x.Name))
                Console.WriteLine(format, program.Name, program.Description);
        }
        #endregion
    }
}
