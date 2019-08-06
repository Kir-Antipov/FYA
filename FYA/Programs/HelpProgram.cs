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
            string programName = Arguments.FirstOrDefault();
            IProgram programToSeeHelp = string.IsNullOrEmpty(programName) ? null : Program.Programs.FirstOrDefault(x => x.Name.Equals(programName, StringComparison.InvariantCultureIgnoreCase));
            if (programToSeeHelp is null)
            {
                int maxNameLength = Program.Programs.Max(x => x.Name.Length);
                string format = $"{{0, -{maxNameLength + 10}}}{{1}}";
                Console.WriteLine();
                foreach (IProgram program in Program.Programs.OrderBy(x => x.Name))
                    Console.WriteLine(format, program.Name, program.Description);
            }
            else
                Console.WriteLine(programToSeeHelp.GetHelpText());
        }

        public override string GetHelpText() => string.Empty;
        #endregion
    }
}
