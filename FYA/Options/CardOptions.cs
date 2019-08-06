using CommandLine;

namespace FYA.Options
{
    public class CardOptions
    {
        #region Var
        [Option('i', "id", SetName = "cardID", HelpText = "Video card's instance id")]
        public string ID { get; }

        [Option('n', "name", SetName = "cardName", HelpText = "Video card's name")]
        public string Name { get; }
        #endregion

        #region Init
        public CardOptions(string ID, string Name)
        {
            this.ID = ID ?? string.Empty;
            this.Name = Name ?? string.Empty;
        }
        #endregion
    }
}
