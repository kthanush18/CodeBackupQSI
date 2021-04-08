using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Common.Models.UITest
{
    public class CommonCollections
    {
        public List<string> MeaningsListFromUI { get; set; }
        public List<string> MeaningsListFromDatabase { get; set; }
        public string MeaningOnLeftFromUI { get; set; }
        public string MeaningOnLeftFromDB { get; set; }
        public List<string> SynonymsListForSelectedMeaningFromUI { get; set; }
        public List<string> SynonymsListForSelectedMeaningFromDB { get; set; }
        public List<string> PhrasesListForSelectedMeaningFromUI { get; set; }
        public List<string> PhrasesListForSelectedMeaningFromDB { get; set; }
        public List<Meaning> SortedMeaningsAndMeaningIDsFromDB { get; set; }
        public List<Phrase> SortedPhrasesAndPhraseIDsFromDB { get; set; }
        public List<string> SourcesListForSelectedMeaningFromUI { get; set; }
        public List<string> SourcesListForSelectedMeaningFromDB { get; set; }
        public string FrequencyOfUseFromUI { get; set; }
        public string FrequencyOfUseFromDB { get; set; }
        public List<string> RelatedKeywordsListForSelectedPhraseFromUI { get; set; }
        public List<string> RelatedKeywordsListForSelectedPhraseFromDB { get; set; }
        public List<string> SeeAlsoWordsFromUI { get; set; }
        public List<string> SeeAlsoWordsFromDB { get; set; }
        public string PhraseTextFromTimelineWindow { get; set; }
        public string PhraseTextFromUsageGraphWindow { get; set; }
        public string FrequencyOfUseFromUsageGraphWindow { get; set; }
        public List<string> VerifiedSourcesFromDB { get; set; }
        public List<string> UnverifiedSourcesFromDB { get; set; }
        public string VerifiedSourceFromUI { get; set; }
        public string UnverifiedSourceFromUI { get; set; }
        public int TotalNumberOFSourcesFromDB { get;set; }

    }
}
