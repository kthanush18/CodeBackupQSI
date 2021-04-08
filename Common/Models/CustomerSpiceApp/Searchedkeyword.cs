namespace Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp
{
    public class Searchedkeyword : Word
    {
        public string SelectedMeaning { get; set; }
        public string MeaningIndexOnWebPage { get; set; }
        public string MeaningIndex { get; set; }
        public string SelectedCircleColour { get; set; }

        private readonly string _colour = "rgba(255, 255, 255, 1)";
        public string CssColourForSelectedCircle
        {
            get
            {
                return _colour;
            }
        }
    }
}
