using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Quant.CardsGame.UITests.Web.CardsGame.DataAccess
{
    public class CardsGameDataAccess
    {
        readonly string _cardsGameHandsDBConnectionString = ConfigurationManager.ConnectionStrings["cardsgamehandsDB"].ToString().ToLower();

        public string GetLinFileUsingSegmentID(int randomSegmentID)
        {
            string linFile = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_cardsGameHandsDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select linfilecontents from segments_linfiles where segmentid = @segmentID", connection))
                {
                    NpgsqlParameter ID = new NpgsqlParameter
                    {
                        ParameterName = "@segmentID",
                        Value = randomSegmentID

                    };
                    command.Parameters.Add(ID);

                    using (NpgsqlDataReader linFileReader = command.ExecuteReader())
                    {
                        if (linFileReader.Read())
                        {
                            linFile = (string)linFileReader["linfilecontents"];
                        }
                    }
                }
            }
            return linFile;
        }
        public int GetLinFileIDUsingSegmentID(int randomSegmentID)
        {
            int linFileID = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(_cardsGameHandsDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select linfileid from segments_linfiles where segmentid = @segmentID", connection))
                {
                    NpgsqlParameter ID = new NpgsqlParameter
                    {
                        ParameterName = "@segmentID",
                        Value = randomSegmentID

                    };
                    command.Parameters.Add(ID);

                    using (NpgsqlDataReader linFileReader = command.ExecuteReader())
                    {
                        if (linFileReader.Read())
                        {
                            linFileID = Convert.ToInt32(linFileReader["linfileid"]);
                        }
                    }
                }
            }
            return linFileID;
        }
        public string GetLinFileUsingBoardID(int boardID)
        {
            string linFile = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_cardsGameHandsDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select linfilecontents from boards_linfiles where boardid = @boardID", connection))
                {
                    NpgsqlParameter ID = new NpgsqlParameter
                    {
                        ParameterName = "@boardID",
                        Value = boardID

                    };
                    command.Parameters.Add(ID);

                    using (NpgsqlDataReader linFileReader = command.ExecuteReader())
                    {
                        if (linFileReader.Read())
                        {
                            linFile = (string) linFileReader["linfilecontents"];
                        }
                    }
                }
            }
            return linFile;
        }
        public List<string> GetPlayerFullNames(List<string> playersNames)
        {
            List<string> playersFullNames = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_cardsGameHandsDBConnectionString))
            {
                connection.Open();
                foreach(string playerName in playersNames)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("select firstname,lastname from players as p inner join players_savedhands as s on s.playerid = p.id where s.username = @playerName", connection))
                    {
                        NpgsqlParameter ID = new NpgsqlParameter
                        {
                            ParameterName = "@playerName",
                            Value = playerName

                        };
                        command.Parameters.Add(ID);

                        using (NpgsqlDataReader nameReader = command.ExecuteReader())
                        {
                            while (nameReader.Read())
                            {
                                playersFullNames.Add($"{(string)nameReader["firstname"]} {(string)nameReader["lastname"]}".Trim());
                            }
                        }
                    }
                }
            }
            return playersFullNames;
        }
        public int GetNumberOfBoardsUsingSegmentID(int randomSegmentID)
        {
            int numberOfBoards = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(_cardsGameHandsDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select boardid from segments_boards where segmentid = @segmentID", connection))
                {
                    NpgsqlParameter ID = new NpgsqlParameter
                    {
                        ParameterName = "@segmentID",
                        Value = randomSegmentID

                    };
                    command.Parameters.Add(ID);

                    using (NpgsqlDataReader boardIDReader = command.ExecuteReader())
                    {
                        List<int> boardIDs = new List<int>();
                        while (boardIDReader.Read())
                        {
                            boardIDs.Add((int)boardIDReader["boardid"]);
                        }
                        numberOfBoards = boardIDs.Count;
                    }
                }
            }
            return numberOfBoards;
        }
        public string GetOvercallMadeAtUsingID(int overcallID)
        {
            string overcallOption = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_cardsGameHandsDBConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select option from overcallmadeatoptions where id =  @overcallID", connection))
                {
                    NpgsqlParameter ID = new NpgsqlParameter
                    {
                        ParameterName = "@overcallID",
                        Value = overcallID

                    };
                    command.Parameters.Add(ID);

                    using (NpgsqlDataReader overcallReader = command.ExecuteReader())
                    {
                        if (overcallReader.Read())
                        {
                            overcallOption = (string)overcallReader["option"];
                        }
                    }
                }
            }
            return overcallOption;
        }
    }
}
