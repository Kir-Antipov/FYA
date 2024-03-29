﻿using CommandLine;

namespace FYA.Options
{
    public class SwitchOptions
    {
        #region Var
        [Option('i', "id", Required = true, SetName = "cardID", HelpText = "Video card's instance id")]
        public string ID { get; }

        [Option('n', "name", Required = true, SetName = "cardName", HelpText = "Video card's name")]
        public string Name { get; }

        [Option('d', "disable", HelpText = "Indicates turn on or off the specified video card")]
        public bool Disable { get; }
        #endregion

        #region Init
        public SwitchOptions(string ID, string Name, bool Disable)
        {
            this.ID = ID ?? string.Empty;
            this.Name = Name ?? string.Empty;
            this.Disable = Disable;
        }
        #endregion
    }
}
