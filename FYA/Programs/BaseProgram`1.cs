using System;
using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace FYA.Programs
{
    public abstract class BaseProgram<TOptions> : BaseProgram
    {
        #region Functions
        public override void Run(IEnumerable<string> Arguments)
        {
            Parser parser = new Parser(settings =>
            {
                settings.AutoVersion = false;
                settings.AutoHelp = false;
                settings.CaseSensitive = false;
                settings.HelpWriter = null;
            });

            ParserResult<TOptions> result = parser.ParseArguments<TOptions>(Arguments);
            result
                .WithNotParsed(x => { Console.WriteLine(GetHelpText(result)); })
                .WithParsed(Run);
        }

        public abstract void Run(TOptions Options);

        public virtual string GetHelpText(ParserResult<TOptions> Result)
        {
            HelpText help = new HelpText
            {
                AddDashesToOption = true,
                AutoHelp = false,
                AutoVersion = false,
                AdditionalNewLineAfterOption = false,
            };
            if (!string.IsNullOrEmpty(Usage))
                help.AddPreOptionsLine($" Usage: {Usage}");
            help.AddOptions(Result);
            return help;
        }
        #endregion
    }
}
