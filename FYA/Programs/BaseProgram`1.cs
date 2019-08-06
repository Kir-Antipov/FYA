using System;
using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace FYA.Programs
{
    public abstract class BaseProgram<TOptions> : BaseProgram
    {
        #region Functions
        private static Parser CreateParser() => new Parser(settings =>
        {
            settings.AutoVersion = false;
            settings.AutoHelp = false;
            settings.CaseSensitive = false;
            settings.HelpWriter = null;
        });

        public override void Run(IEnumerable<string> Arguments)
        {
            ParserResult<TOptions> result = CreateParser().ParseArguments<TOptions>(Arguments);
            result
                .WithNotParsed(x => { Console.WriteLine(GetHelpText(result)); })
                .WithParsed(Run);
        }

        public abstract void Run(TOptions Options);

        public override string GetHelpText()
        {
            ParserResult<TOptions> result = CreateParser().ParseArguments<TOptions>(new string[0]);
            return GetHelpText(result);
        }

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
