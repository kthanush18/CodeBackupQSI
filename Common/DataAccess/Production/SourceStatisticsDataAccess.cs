using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Quant.Spice.Test.UI.Common.DataAccess.Production
{
    public class SourceStatisticsDataAccess
    {
        readonly string spiceCustomersConnectionString = ConfigurationManager.ConnectionStrings["SpiceCustomersDB"].ToString().ToLower();
        readonly string spiceProdDBConnectionString = ConfigurationManager.ConnectionStrings["SpiceProdDB"].ToString().ToLower();

        public XmlDocument GetSourceStatisticsXML()
        {
            XmlDocument xmldoc = new XmlDocument();
            using (NpgsqlConnection connection = new NpgsqlConnection(spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT statisticsxml FROM SourcesStatistics", connection))
                {
                    using (NpgsqlDataReader XMLReader = command.ExecuteReader())
                    {
                        List<string> infoXML = new List<string>();
                        while (XMLReader.Read())
                        {
                            string InfoXML = null;
                            {
                                InfoXML = (string)XMLReader["statisticsxml"];
                            }
                            xmldoc.LoadXml(InfoXML);
                        }
                    }
                }
            }
            return xmldoc;
        }
        public List<string> ColumnNames()
        {
            List<string> columnNames = new List<string>();
            using (NpgsqlConnection connection = new NpgsqlConnection(spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select * from authorsstatistics limit 1", connection))
                {
                    using (NpgsqlDataReader columnReader = command.ExecuteReader())
                    {
                        while (columnReader.Read())
                        {
                            columnNames.Add(columnReader.GetName(1));
                            columnNames.Add(columnReader.GetName(2));
                            columnNames.Add(columnReader.GetName(3));
                        }
                    }
                }
            }
            return columnNames;
        }
        public string AuthorName(string columnName)
        {
            string SingleAuthorName = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT " + columnName + " FROM authorsstatistics WHERE sourcescount != 0 ORDER BY RANDOM() Limit 1", connection))
                {
                    using (NpgsqlDataReader nameReader = command.ExecuteReader())
                    {
                        if (nameReader.Read())
                        {
                            SingleAuthorName = (string)nameReader[columnName];
                        }
                    }
                }
            }
            return SingleAuthorName;
        }
        public int SourceStatisticsCount(string authorName)
        {
            int SumofSourcesCount = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT SUM(sourcescount) FROM authorsstatistics WHERE LastName ILIKE @singleAuthorName OR FirstName ILIKE @singleAuthorName OR FullName ILIKE @singleAuthorName", connection))
                {
                    NpgsqlParameter singleName = new NpgsqlParameter
                    {
                        ParameterName = "@singleAuthorName",
                        Value = authorName
                    };
                    command.Parameters.Add(singleName);

                    using (NpgsqlDataReader numberReader = command.ExecuteReader())
                    {
                        while (numberReader.Read())
                        {
                            SumofSourcesCount = Convert.ToInt32(numberReader["sum"]);
                        }
                    }
                }
            }
            return SumofSourcesCount;
        }
        public int RandomYear()
        {
            int randomYear = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT originalyear FROM PhrasesStatistics ORDER BY RANDOM() LIMIT 1", connection))
                {
                    using (NpgsqlDataReader yearReader = command.ExecuteReader())
                    {
                        while (yearReader.Read())
                        {
                            randomYear = Convert.ToInt32(yearReader["originalyear"]);
                        }
                    }
                }
            }
            return randomYear;
        }
        public int PhrasesFromYear(int randomYear)
        {
            int phrasesCountForYear = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(PhraseID) FROM PhrasesStatistics WHERE OriginalYear = @year", connection))
                {
                    NpgsqlParameter year = new NpgsqlParameter
                    {
                        ParameterName = "@year",
                        Value = randomYear
                    };
                    command.Parameters.Add(year);
                    using (NpgsqlDataReader numberReader = command.ExecuteReader())
                    {
                        while (numberReader.Read())
                        {
                            phrasesCountForYear = Convert.ToInt32(numberReader["count"]);
                        }
                    }
                }
            }
            return phrasesCountForYear;
        }
        public int PhrasesWithUniqueUses(int randomNumberOfUniqueUses)
        {
            int phrasesCount = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(PhraseID) FROM PhrasesStatistics WHERE Uniqueusescount = @number", connection))
                {
                    NpgsqlParameter number = new NpgsqlParameter
                    {
                        ParameterName = "@number",
                        Value = randomNumberOfUniqueUses
                    };
                    command.Parameters.Add(number);
                    using (NpgsqlDataReader numberReader = command.ExecuteReader())
                    {
                        while (numberReader.Read())
                        {
                            phrasesCount = Convert.ToInt32(numberReader["count"]);
                        }
                    }
                }
            }
            return phrasesCount;
        }
        public int PhrasesWithWordsCount(int randomNumberOfWordsCount)
        {
            int phrasesCount = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(PhraseID) FROM PhrasesStatistics WHERE WordCount = @number", connection))
                {
                    NpgsqlParameter number = new NpgsqlParameter
                    {
                        ParameterName = "@number",
                        Value = randomNumberOfWordsCount
                    };
                    command.Parameters.Add(number);
                    using (NpgsqlDataReader numberReader = command.ExecuteReader())
                    {
                        while (numberReader.Read())
                        {
                            phrasesCount = Convert.ToInt32(numberReader["count"]);
                        }
                    }
                }
            }
            return phrasesCount;
        }
        public int PhrasesWithUniqueUsesAndWordsCount(int randomNumberOfUniqueUses, int randomNumberOfWordsCount)
        {
            int phrasesCount = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(spiceProdDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(PhraseID) FROM PhrasesStatistics WHERE Uniqueusescount = @usesCount AND WordCount = @wordsCount", connection))
                {
                    NpgsqlParameter usesCount = new NpgsqlParameter
                    {
                        ParameterName = "@usesCount",
                        Value = randomNumberOfUniqueUses
                    };
                    command.Parameters.Add(usesCount);
                    NpgsqlParameter wordsCount = new NpgsqlParameter
                    {
                        ParameterName = "@wordsCount",
                        Value = randomNumberOfWordsCount
                    };
                    command.Parameters.Add(wordsCount);
                    using (NpgsqlDataReader numberReader = command.ExecuteReader())
                    {
                        while (numberReader.Read())
                        {
                            phrasesCount = Convert.ToInt32(numberReader["count"]);
                        }
                    }
                }
            }
            return phrasesCount;
        }
    }
}
