using CommandLine;

namespace FYA.Options
{
    public class CardOptions
    {
        #region Var
        [Option('d', "disable", HelpText = "Indicates turn on or off the specified video card")]
        public bool Disable { get; }

        [Option('i', "id", Required = true, SetName = "cardID", HelpText = "Video card's instance id")]
        public string ID { get; }

        [Option('n', "name", Required = true, SetName = "cardName", HelpText = "Video card's name")]
        public string Name { get; }
        #endregion

        #region Init
        public CardOptions(bool Disable, string ID, string Name)
        {
            this.Disable = Disable;
            this.ID = ID ?? string.Empty;
            this.Name = Name ?? string.Empty;
        }
        #endregion
    }
}
