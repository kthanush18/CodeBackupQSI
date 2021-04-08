using Npgsql;
using Quant.Spice.Security;
using Quant.Spice.Test.UI.Common.Models;
using Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp;
using Quant.Spice.Test.UI.Common.Models.UITest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Quant.Spice.Test.UI.Common.DataAccess.Production
{
    public class SearchKeywordDataAccess
    {
        readonly string _spiceCustomersConnectionString = ConfigurationManager.ConnectionStrings["SpiceCustomersDB"].ToString().ToLower();
        readonly string _spiceProdDBConnectionString = ConfigurationManager.ConnectionStrings["SpiceProdDB"].ToString().ToLower();
        readonly Random _random = new Random();
        readonly string _searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"].ToString();

        public string GetRandomWord()
        {
            string randomWord = string.Empty;
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT word FROM words ORDER BY RANDOM() LIMIT 1", connection))
                {
                    using (NpgsqlDataReader wordReader = command.ExecuteReader())
                    {
                        if (wordReader.Read())
                        {
                            randomWord = (string)wordReader["word"];
                        }
                    }
                }
            }
            return randomWord;
        }
        public string GetRandomMultipleLetters()
        {
            string randomWord = string.Empty;
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT word FROM words ORDER BY RANDOM() LIMIT 1", connection))
                {
                    using (NpgsqlDataReader wordReader = command.ExecuteReader())
                    {
                        if (wordReader.Read())
                        {
                            randomWord = (string)wordReader["word"];
                        }
                    }
                }
            }
            return randomWord;
        }

        public List<String> KeywordSuggestionsForSingleLetter(string RandomLetter)
        {
            List<string> SurroundingWordsList = new List<string>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();


                using (NpgsqlCommand command = new NpgsqlCommand("select word from words where word ILike @RandomWord", connection))
                {
                    NpgsqlParameter word = new NpgsqlParameter
                    {
                        ParameterName = "@RandomWord",
                        Value = RandomLetter + '%'
                    };
                    command.Parameters.Add(word);

                    using (NpgsqlDataReader wordReader = command.ExecuteReader())
                    {
                        string SearchWord = null;

                        while (wordReader.Read())
                        {
                            SearchWord = (string)wordReader["word"];
                            SurroundingWordsList.Add(SearchWord);
                        }
                    }
                }
            }
            return SurroundingWordsList;
        }
        public List<String> KeywordSuggestionsForMultipleLetters(string MultipleLetters)
        {
            List<string> SurroundingWordsList = new List<string>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();


                using (NpgsqlCommand command = new NpgsqlCommand("select word from words where word ILike @RandomWord", connection))
                {
                    NpgsqlParameter word = new NpgsqlParameter
                    {
                        ParameterName = "@RandomWord",
                        Value = MultipleLetters + '%'
                    };
                    command.Parameters.Add(word);

                    using (NpgsqlDataReader wordReader = command.ExecuteReader())
                    {
                        string SearchWord = null;

                        while (wordReader.Read())
                        {
                            SearchWord = (string)wordReader["word"];
                            SurroundingWordsList.Add(SearchWord);
                        }
                    }
                }
            }
            return SurroundingWordsList;
        }

        public List<string> MeaningsList(string RandomWord)
        {
            List<string> MeaningsList = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select infoxml from words AS W INNER JOIN xml_wordinfo_tablet_pc AS XWITP ON XWITP.wordid=W.id where lower(W.word)= lower(@RandomWord)", connection))
                {
                    NpgsqlParameter word = new NpgsqlParameter
                    {
                        ParameterName = "@RandomWord",
                        Value = RandomWord
                    };
                    command.Parameters.Add(word);
                    using (NpgsqlDataReader XMLReader = command.ExecuteReader())
                    {
                        List<string> infoXML = new List<string>();


                        while (XMLReader.Read())
                        {
                            string InfoXML = null;
                            {
                                InfoXML = (string)XMLReader["infoxml"];
                            }
                            infoXML.Add(InfoXML);
                            {
                                Encryption encryption = new Encryption();
                                string datainfoxml = encryption.GetDecryptedString(InfoXML);
                                XmlDocument xmldoc = new XmlDocument();
                                xmldoc.LoadXml(datainfoxml);

                                foreach (XmlNode Meanings in xmldoc.SelectNodes("//WORDINFO//GRP//MNGS//MNG"))
                                {
                                    MeaningsList.Add(Meanings.SelectSingleNode("TXT").InnerText.ToString());
                                }
                            }
                        }
                    }
                }
            }
            MeaningsList.Sort();
            return MeaningsList;
        }
        public List<XmlDocument> GetWordInfoXML(string RandomWord)
        {
            List<XmlDocument> CompletedecryptedXML = new List<XmlDocument>();

            Word _access = new Word();
            using (var connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select infoxml from words AS W INNER JOIN xml_wordinfo_tablet_pc AS XWITP ON XWITP.wordid=W.id where lower(W.word)= lower(@RandomWord)", connection))
                {
                    NpgsqlParameter word = new NpgsqlParameter
                    {
                        ParameterName = "@RandomWord",
                        Value = RandomWord
                    };
                    command.Parameters.Add(word);
                    using (NpgsqlDataReader XMLReader = command.ExecuteReader())
                    {
                        List<string> infoXML = new List<string>();
                        while (XMLReader.Read())
                        {
                            string InfoXML = null;
                            {
                                InfoXML = (string)XMLReader["infoxml"];
                            }
                            infoXML.Add(InfoXML);

                            Encryption encryption = new Encryption();
                            string datainfoxml = encryption.GetDecryptedString(InfoXML);
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.LoadXml(datainfoxml);
                            CompletedecryptedXML.Add(xmldoc);
                        }
                    }
                }
            }
            return CompletedecryptedXML;
        }
        public XmlDocument GetSourcesXML(int PhraseIDFromDB)
        {
            XmlDocument DecryptedSourcesXML = new XmlDocument();
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT sourcesxml FROM xml_phrasesources where phraseid = @PhraseID", connection))
                {
                    NpgsqlParameter word = new NpgsqlParameter
                    {
                        ParameterName = "@PhraseID",
                        Value = PhraseIDFromDB
                    };
                    command.Parameters.Add(word);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> infoXML = new List<string>();
                        while (reader.Read())
                        {
                            string InfoXML = null;
                            {
                                InfoXML = (string)reader["sourcesxml"];
                            }
                            infoXML.Add(InfoXML);
                            {
                                Encryption encryption = new Encryption();
                                string dataInfoXML = encryption.GetDecryptedString(InfoXML);
                                XmlDocument xmlDoc = new XmlDocument();
                                DecryptedSourcesXML.LoadXml(dataInfoXML);
                            }
                        }
                    }
                }
            }
            return DecryptedSourcesXML;
        }
        public string GetRedirectedKeyword(string randomRelatedKeyword)
        {
            RedirectedWordInfo redirectedKeyword = new RedirectedWordInfo();
            string keyword = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM	 Hook AS H INNER JOIN Word_Hooks AS WH ON WH.HookID = H.ID INNER JOIN Words AS W ON WH.WordID = W.ID WHERE	W.Word = @RelatedKeyword OR H.Text = @RelatedKeyword ", connection))
                {
                    NpgsqlParameter word = new NpgsqlParameter
                    {
                        ParameterName = "@RelatedKeyword",
                        Value = randomRelatedKeyword
                    };
                    command.Parameters.Add(word);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            {
                                redirectedKeyword.Hook = (string)reader["text"];
                                redirectedKeyword.RedirectedWord = (string)reader["word"];
                            }

                            keyword = redirectedKeyword.RedirectedWord;
                        }
                    }
                }
            }
            return keyword;
        }
        public string RandomPhrase()
        {
            string randomPhrase = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT text FROM Phrase ORDER BY RANDOM() LIMIT 1", connection))
                {
                    using (NpgsqlDataReader phraseReader = command.ExecuteReader())
                    {
                        while (phraseReader.Read())
                        {
                            randomPhrase = (string)phraseReader["text"];
                        }
                    }
                }
            }
            return randomPhrase;
        }
        public List<string> PhraseResults(XmlDocument containingWordsXML)
        {
            List<string> phraseResults = new List<string>();
            XmlDocument PhrasesOutputXML = new XmlDocument();
            string containingWords = containingWordsXML.OuterXml.ToString();
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT p_OutputXML FROM qsp_getprepackagedxml_phrasescontainingwords(CAST(@containingWords AS XML));", connection))
                {
                    NpgsqlParameter wordsXML = new NpgsqlParameter
                    {
                        ParameterName = "@containingWords",
                        Value = containingWords
                    };
                    command.Parameters.Add(wordsXML);
                    using (NpgsqlDataReader XMLReader = command.ExecuteReader())
                    {
                        string outputXML = "";
                        while (XMLReader.Read())
                        {
                            outputXML = (string)XMLReader["p_outputxml"];
                            PhrasesOutputXML.LoadXml(outputXML);
                        }
                        foreach (XmlNode phrase in PhrasesOutputXML.SelectNodes("//PHRASES//PHRASE"))
                        {
                            phraseResults.Add(phrase.SelectSingleNode("TEXT").InnerText.ToString());
                        }
                    }
                }
            }
            phraseResults.Sort();
            return phraseResults;
        }
        public List<Phrase> PhrasesListFromDB(List<XmlDocument> wordInfoXMLs)
        {
            List<Meaning> meanings = new List<Meaning>();
            foreach (XmlNode Meanings in wordInfoXMLs[0].SelectNodes("//WORDINFO//GRP//MNGS//MNG"))
            {
                Meaning meaning = new Meaning
                {
                    Text = Meanings.SelectSingleNode("TXT").InnerText.ToString(),
                    ID = Int32.Parse(Meanings.SelectSingleNode("ID").InnerText)
                };

                meanings.Add(meaning);
            }

            meanings.Sort();
            int meaningID = meanings[0].ID;

            List<Phrase> phrases = new List<Phrase>();
            foreach (XmlDocument wordInfoXML in wordInfoXMLs)
            {
                XmlNodeList phraseNodes = null;
                if (wordInfoXML.OuterXml.Contains("WORDINFO"))
                {
                    phraseNodes = wordInfoXML.SelectNodes("//WORDINFO/MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE");
                }
                else
                {
                    phraseNodes = wordInfoXML.SelectNodes("//MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE");
                }

                foreach (XmlNode Phrases in phraseNodes)
                {
                    Phrase phrase = new Phrase
                    {
                        Text = Phrases.SelectSingleNode("TEXT").InnerText.ToString(),
                        ID = Int32.Parse(Phrases.SelectSingleNode("ID").InnerText)
                    };

                    phrases.Add(phrase);
                }
            }
            phrases.Sort();
            return phrases;
        }
        public XmlDocument GetCumulativeUsageXML(int PhraseIDFromDB)
        {
            XmlDocument DecryptedSourcesXML = new XmlDocument();
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT InfoXML FROM XML_CumulativeUsage WHERE PhraseID = @PhraseID", connection))
                {
                    NpgsqlParameter word = new NpgsqlParameter
                    {
                        ParameterName = "@PhraseID",
                        Value = PhraseIDFromDB
                    };
                    command.Parameters.Add(word);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> infoXML = new List<string>();
                        while (reader.Read())
                        {
                            string InfoXML = null;
                            {
                                InfoXML = (string)reader["infoxml"];
                            }
                            infoXML.Add(InfoXML);
                            {
                                Encryption encryption = new Encryption();
                                string dataInfoXML = encryption.GetDecryptedString(InfoXML);
                                XmlDocument xmlDoc = new XmlDocument();
                                DecryptedSourcesXML.LoadXml(dataInfoXML);
                            }
                        }
                    }
                }
            }
            return DecryptedSourcesXML;
        }
        public XmlDocument GetUserAccountDetails()
        {
            string username = ConfigurationManager.AppSettings["Username"].ToString();
            XmlDocument UserAccountDetailsXML = new XmlDocument();
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                string activationCertificate = "";
                string deviceID = "";
                string cloudID = "";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("select HM.cloudid, M.activationcertificate, M.deviceid from subscription_devices AS M INNER JOIN subscriptions AS HM ON HM.id = M.subscriptionid INNER JOIN spice_users AS W ON W.spice_user_id = HM.spice_user_id INNER JOIN spice_users AS WHM ON W.spice_user_name = @Username LIMIT 1", connection))
                {
                    NpgsqlParameter Username = new NpgsqlParameter
                    {
                        ParameterName = "@Username",
                        Value = username
                    };
                    command.Parameters.Add(Username);

                    using (NpgsqlDataReader Reader = command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            activationCertificate = (string)Reader["activationcertificate"];
                            deviceID = (string)Reader["deviceid"];
                            cloudID = (string)Reader["cloudid"];
                        }
                    }
                }
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT p_AcctDetailsXML FROM QSP_GetUserAccountDetailsForClientApp(@activationcertificate, @deviceID, @cloudID);", connection))
                {
                    NpgsqlParameter ActivationCertificate = new NpgsqlParameter
                    {
                        ParameterName = "@activationcertificate",
                        Value = activationCertificate
                        
                    };
                    command.Parameters.Add(ActivationCertificate);
                    NpgsqlParameter DeviceID = new NpgsqlParameter
                    {
                        ParameterName = "@deviceID",
                        Value = deviceID

                    };
                    command.Parameters.Add(DeviceID);
                    NpgsqlParameter CloudID = new NpgsqlParameter
                    {
                        ParameterName = "@cloudID",
                        Value = cloudID

                    };
                    command.Parameters.Add(CloudID);
                    using (NpgsqlDataReader XMLReader = command.ExecuteReader())
                    {
                        string outputXML = "";
                        while (XMLReader.Read())
                        {
                            outputXML = (string)XMLReader["p_acctdetailsxml"];
                            UserAccountDetailsXML.LoadXml(outputXML);
                        }
                    }
                }
            }
            return UserAccountDetailsXML;
        }
        
        public List<Word> GetKeywordsList()
        {
            List<Word> keywords = new List<Word>();
            Word keyword = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceProdDBConnectionString))
            {
                connection.Open();
                if (_searchCriteria == "All")
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT ID, Word FROM words ORDER BY word ASC", connection))
                    {
                        using (NpgsqlDataReader wordReader = command.ExecuteReader())
                        {
                            while (wordReader.Read())
                            {
                                keyword = new Word();
                                keyword.ID = Convert.ToInt32(wordReader["ID"]);
                                keyword.Text = Convert.ToString(wordReader["Word"]);
                                keywords.Add(keyword);
                            }
                        }
                    }
                }
                else if (_searchCriteria == "Range")
                {
                    int minimumRange = Int32.Parse(ConfigurationManager.AppSettings["Range_Min"]);
                    int maximumRange = Int32.Parse(ConfigurationManager.AppSettings["Range_Max"]);
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT word,id FROM words where id between @minimumRange and @maximumRange", connection))
                    {
                        NpgsqlParameter MinimumRange = new NpgsqlParameter
                        {
                            ParameterName = "@minimumRange",
                            Value = minimumRange
                        };
                        command.Parameters.Add(MinimumRange);
                        NpgsqlParameter MaximumRange = new NpgsqlParameter
                        {
                            ParameterName = "@maximumRange",
                            Value = maximumRange
                        };
                        command.Parameters.Add(MaximumRange);
                        using (NpgsqlDataReader wordReader = command.ExecuteReader())
                        {
                            while (wordReader.Read())
                            {
                                keyword = new Word();
                                keyword.ID = Convert.ToInt32(wordReader["ID"]);
                                keyword.Text = Convert.ToString(wordReader["Word"]);
                                keywords.Add(keyword);
                            }
                        }
                    }
                }
                else if (_searchCriteria == "Random")
                {
                    int keywordsCount = Int32.Parse(ConfigurationManager.AppSettings["Random"]);
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT word,id FROM words ORDER BY RANDOM() LIMIT @keywordsCount", connection))
                    {
                        NpgsqlParameter KeywordsCount = new NpgsqlParameter
                        {
                            ParameterName = "@keywordsCount",
                            Value = keywordsCount
                        };
                        command.Parameters.Add(KeywordsCount);
                        using (NpgsqlDataReader wordReader = command.ExecuteReader())
                        {
                            while (wordReader.Read())
                            {
                                keyword = new Word();
                                keyword.ID = Convert.ToInt32(wordReader["ID"]);
                                keyword.Text = Convert.ToString(wordReader["Word"]);
                                keywords.Add(keyword);
                            }
                        }
                    }
                }
            }
            return keywords;
        }
        public int PhraseID(List<XmlDocument> wordInfoXMLs)
        {
            List<Meaning> meanings = new List<Meaning>();
            foreach (XmlNode Meanings in wordInfoXMLs[0].SelectNodes("//WORDINFO//GRP//MNGS//MNG"))
            {
                Meaning meaning = new Meaning
                {
                    Text = Meanings.SelectSingleNode("TXT").InnerText.ToString(),
                    ID = Int32.Parse(Meanings.SelectSingleNode("ID").InnerText)
                };
                meanings.Add(meaning);
            }
            meanings.Sort();
            int meaningID = meanings[0].ID;
            List<Phrase> phrases = new List<Phrase>();
            foreach (XmlDocument wordInfoXML in wordInfoXMLs)
            {
                XmlNodeList phraseNodes = null;
                if (wordInfoXML.OuterXml.Contains("WORDINFO"))
                {
                    phraseNodes = wordInfoXML.SelectNodes("//WORDINFO/MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE");
                }
                else
                {
                    phraseNodes = wordInfoXML.SelectNodes("//MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE");
                }
                foreach (XmlNode Phrases in phraseNodes)
                {
                    Phrase phrase = new Phrase
                    {
                        Text = Phrases.SelectSingleNode("TEXT").InnerText.ToString(),
                        ID = Int32.Parse(Phrases.SelectSingleNode("ID").InnerText)
                    };
                    phrases.Add(phrase);
                }
            }
            phrases.Sort();
            return phrases[0].ID;
        }

    }
}