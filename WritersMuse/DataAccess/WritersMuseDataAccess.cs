using Npgsql;
using Quant.Spice.Security;
using Quant.Spice.Test.UI.Web.WritersMuse.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml;

namespace Quant.Spice.Test.UI.Web.WritersMuse.DataAccess
{
    public class WritersMuseDataAccess
    {
        readonly string _spiceCustomersConnectionString = ConfigurationManager.ConnectionStrings["SpiceCustomersDB"].ToString().ToLower();
        readonly string _spiceProdDBConnectionString = ConfigurationManager.ConnectionStrings["SpiceProdDB"].ToString().ToLower();
        Random _random = new Random();

        public ProductPrices GetProductPricesForSpiceMobile(int productCode, int monthlyDuration, int yearlyDuration, bool isGiftProduct)
        {
            ProductPrices productPrices = new ProductPrices();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT price FROM QSP_GetAllPrices(@productCode) where isgiftproduct = @isGiftProduct and duration in (@monthlyDuration,@yearlyDuration)", connection))
                {
                    NpgsqlParameter ProductCode = new NpgsqlParameter
                    {
                        ParameterName = "@productCode",
                        Value = productCode

                    };
                    command.Parameters.Add(ProductCode);
                    NpgsqlParameter MonthlyDuration = new NpgsqlParameter
                    {
                        ParameterName = "@monthlyDuration",
                        Value = monthlyDuration

                    };
                    command.Parameters.Add(MonthlyDuration);
                    NpgsqlParameter YearlyDuration = new NpgsqlParameter
                    {
                        ParameterName = "@yearlyDuration",
                        Value = yearlyDuration

                    };
                    command.Parameters.Add(YearlyDuration);
                    NpgsqlParameter IsGiftProduct = new NpgsqlParameter
                    {
                        ParameterName = "@isGiftProduct",
                        Value = isGiftProduct

                    };
                    command.Parameters.Add(IsGiftProduct);

                    using (NpgsqlDataReader PriceReader = command.ExecuteReader())
                    {
                        List<string> encryptedPrices = new List<string>();
                        List<string> prices = new List<string>();

                        while (PriceReader.Read())
                        {
                            string price = null;
                            {
                                price = (string)PriceReader["price"];
                            }
                            encryptedPrices.Add(price);
                            {
                                Encryption encryption = new Encryption();
                                string priceData = encryption.GetDecryptedString(price);
                                prices.Add(priceData);
                            }
                        }
                        productPrices.SpiceMobilePerMonthDB = prices[0];
                        productPrices.SpiceMobilePerYearDB = prices[1];
                    }
                }
            }
            return productPrices;
        }
        public void DeleteUserAccountInDB(string newUsername)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("Delete from spice_users where spice_user_name = @newUsername", connection))
                {
                    NpgsqlParameter Username = new NpgsqlParameter
                    {
                        ParameterName = "@newUsername",
                        Value = newUsername

                    };
                    command.Parameters.Add(Username);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void ActivateUserAccountInDB(string newUsername, bool isAccountActivated)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("update spice_users set isaccountactivated = @isAccountActivated where spice_user_name = @newUsername", connection))
                {
                    NpgsqlParameter Username = new NpgsqlParameter
                    {
                        ParameterName = "@newUsername",
                        Value = newUsername

                    };
                    command.Parameters.Add(Username);
                    NpgsqlParameter IsAccountActivated = new NpgsqlParameter
                    {
                        ParameterName = "@isAccountActivated",
                        Value = isAccountActivated

                    };
                    command.Parameters.Add(IsAccountActivated);
                    command.ExecuteNonQuery();
                }
            }
        }
        public string GetCurrentPasswordOfUser(string userName)
        {
            string password = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select password from spice_users where spice_user_name = @userName", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = userName

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader PasswordReader = command.ExecuteReader())
                    {
                        if (PasswordReader.Read())
                        {
                            password = (string)PasswordReader["password"];
                        }
                    }
                }
            }
            return password;
        }
        public void ChangePasswordInDB(string hashedPassword, string userName)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("update spice_users set password = @hashedPassword where spice_user_name = @userName", connection))
                {
                    NpgsqlParameter Password = new NpgsqlParameter
                    {
                        ParameterName = "@hashedPassword",
                        Value = hashedPassword

                    };
                    command.Parameters.Add(Password);
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = userName

                    };
                    command.Parameters.Add(Name);
                    command.ExecuteNonQuery();
                }
            }
        }
        public ResetPasswordFields GetResetpasswordFieldsData(string userName)
        {
            ResetPasswordFields passwordFields = new ResetPasswordFields();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select email,question1_id,answer1,question2_id,answer2 from spice_users where spice_user_name = @userName", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = userName

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader commonReader = command.ExecuteReader())
                    {
                        if (commonReader.Read())
                        {
                            passwordFields.Email = (string)commonReader["email"];
                            passwordFields.QuestionID1 = (int)commonReader["question1_id"];
                            passwordFields.Answer1 = (string)commonReader["answer1"];
                            passwordFields.QuestionID2 = (int)commonReader["question2_id"];
                            passwordFields.Answer2 = (string)commonReader["answer2"];
                        }
                    }
                }
            }
            return passwordFields;
        }
        public float GetSpiceUserIdForUserName(string userName)
        {
            float spiceUserID = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select spice_user_id from spice_users where spice_user_name = @userName", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = userName

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader SpiceIDReader = command.ExecuteReader())
                    {
                        if (SpiceIDReader.Read())
                        {
                            spiceUserID = (Int64)SpiceIDReader["spice_user_id"];
                        }
                    }
                }
            }
            return spiceUserID;
        }
        public void DeletePayPalInvoiceInDB(float spiceUserID)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("delete from paypalinvoices where spice_user_id = @spiceUserID", connection))
                {
                    NpgsqlParameter SpiceUserID = new NpgsqlParameter
                    {
                        ParameterName = "@spiceUserID",
                        Value = spiceUserID

                    };
                    command.Parameters.Add(SpiceUserID);
                    command.ExecuteNonQuery();
                }
            }
        }
        public ProductPrices GetGiftAndRegularProductPricesForProductCode(int productCode, int yearlyDuration)
        {
            ProductPrices productPrices = new ProductPrices();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT price FROM QSP_GetAllPrices(@productCode) where duration = @yearlyDuration", connection))
                {
                    NpgsqlParameter ProductCode = new NpgsqlParameter
                    {
                        ParameterName = "@productCode",
                        Value = productCode

                    };
                    command.Parameters.Add(ProductCode);
                    NpgsqlParameter YearlyDuration = new NpgsqlParameter
                    {
                        ParameterName = "@yearlyDuration",
                        Value = yearlyDuration

                    };
                    command.Parameters.Add(YearlyDuration);

                    using (NpgsqlDataReader PriceReader = command.ExecuteReader())
                    {
                        List<string> encryptedPrices = new List<string>();
                        List<string> prices = new List<string>();

                        while (PriceReader.Read())
                        {
                            string price = null;
                            {
                                price = (string)PriceReader["price"];
                            }
                            encryptedPrices.Add(price);
                            {
                                Encryption encryption = new Encryption();
                                string priceData = encryption.GetDecryptedString(price);
                                prices.Add(priceData);
                            }
                        }
                        if(productCode == 1)
                        {
                            productPrices.SpiceMobilePerYearDB = prices[0];
                            productPrices.SpiceMobilePerYearDB_Gift = prices[1];
                        }
                        else if(productCode == 3)
                        {
                            productPrices.SpiceProfessionalPerYearDB = prices[0];
                            productPrices.SpiceProfessionalPerYearDB_Gift = prices[1];
                        }
                    }
                }
            }
            return productPrices;
        }

        public string GenerateGiftCodesForProduct(int productID)
        {
            string giftCode = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select p_GiftCode from QSP_GenerateGiftAccessCode(@ProductID)", connection))
                {
                    NpgsqlParameter ProdID = new NpgsqlParameter
                    {
                        ParameterName = "@ProductID",
                        Value = productID

                    };
                    command.Parameters.Add(ProdID);

                    using (NpgsqlDataReader SpiceIDReader = command.ExecuteReader())
                    {
                        if (SpiceIDReader.Read())
                        {
                            giftCode = (string)SpiceIDReader["p_GiftCode"];
                        }
                    }
                }

                using (NpgsqlCommand Command = new NpgsqlCommand("INSERT INTO GiftSubscriptionCodes (GiftCode,RecipientID) VALUES (@giftCode,(select id from giftrecipients limit 1))", connection))
                {
                    NpgsqlParameter Code = new NpgsqlParameter
                    {
                        ParameterName = "@giftCode",
                        Value = giftCode

                    };
                    Command.Parameters.Add(Code);
                    Command.ExecuteNonQuery();
                }
                
                using (NpgsqlCommand Command = new NpgsqlCommand("INSERT INTO GiftSubscriptions VALUES ((SELECT p_id FROM QSP_CreateSubscription(@SpiceUserID,CAST(@DateTime AS TIMESTAMP),@ProductID,@PayPalRecieptID,@InVoiceNumber,@TransctionTypeID)),@giftCode)", connection))
                {

                    NpgsqlParameter userID = new NpgsqlParameter
                    {
                        ParameterName = "@SpiceUserID",
                        Value = DBNull.Value
                    };
                    Command.Parameters.Add(userID);
                    NpgsqlParameter dateTime = new NpgsqlParameter
                    {
                        ParameterName = "@DateTime",
                        Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

                    };
                    Command.Parameters.Add(dateTime);
                    NpgsqlParameter ID = new NpgsqlParameter
                    {
                        ParameterName = "@ProductID",
                        Value = productID

                    };
                    Command.Parameters.Add(ID);
                    NpgsqlParameter PayPalID = new NpgsqlParameter
                    {
                        ParameterName = "@PayPalRecieptID",
                        Value = 1977
                    };
                    Command.Parameters.Add(PayPalID);
                    NpgsqlParameter inVoiceNumber = new NpgsqlParameter
                    {
                        ParameterName = "@InVoiceNumber",
                        Value = "ShanonaWriter_2015_8_21_20_24_33_422"
                    };
                    Command.Parameters.Add(inVoiceNumber);
                    NpgsqlParameter TransctionID = new NpgsqlParameter
                    {
                        ParameterName = "@TransctionTypeID",
                        Value = 17
                    };
                    Command.Parameters.Add(TransctionID);
                    NpgsqlParameter Code = new NpgsqlParameter
                    {
                        ParameterName = "@giftCode",
                        Value = giftCode

                    };
                    Command.Parameters.Add(Code);
                    Command.ExecuteNonQuery();
                }
            }
            return giftCode;
        }
        public void DeleteRedeemedSubscriptionFromDB(string giftCode)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM giftsubscriptions WHERE subscriptionid IN (SELECT subscriptionid FROM giftsubscriptions WHERE giftcode = @GiftCode)", connection))
                {
                    NpgsqlParameter code = new NpgsqlParameter
                    {
                        ParameterName = "@GiftCode",
                        Value = giftCode

                    };
                    command.Parameters.Add(code);
                    command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM subscriptions WHERE spice_user_id IN (SELECT spice_user_id FROM spice_users WHERE spice_user_name = @Username)", connection))
                {
                    NpgsqlParameter username = new NpgsqlParameter
                    {
                        ParameterName = "@Username",
                        Value = ConfigurationManager.AppSettings["Username"].ToString()

                    };
                    command.Parameters.Add(username);
                    command.ExecuteNonQuery();
                }
            }
        }
        public string GetAboutYourselfTextOfUser(string username)
        {
            string aboutYourselfText = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select aboutyou from spice_users where spice_user_name = @userName", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader SpiceTextReader = command.ExecuteReader())
                    {
                        if (SpiceTextReader.Read())
                        {
                            aboutYourselfText = (string)SpiceTextReader["aboutyou"];
                        }
                    }
                }
            }
            return aboutYourselfText;
        }
        public PhraseSummaryDB GetPhrasesSummaryFromDB(string username, int unassignedPendingPhrases, int assignedPendingPhrases, int acceptedPhrases, int rejectedPhrases)
        {
            PhraseSummaryDB phraseSummary = new PhraseSummaryDB();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select phrase_status from users_submitted_phrases where spice_user_id IN (select spice_user_id from spice_users where spice_user_name = @userName)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader StatusCodeReader = command.ExecuteReader())
                    {
                        List<int> allStatusCodes = new List<int>();

                        while (StatusCodeReader.Read())
                        {
                            allStatusCodes.Add((int)StatusCodeReader["phrase_status"]);
                        }
                        List<int> pendingPhrasesDB = new List<int>();
                        List<int> acceptedPhrasesDB = new List<int>();
                        List<int> rejectedPhrasesDB = new List<int>();
                        foreach (int code in allStatusCodes)
                        {
                            if(code == unassignedPendingPhrases || code == assignedPendingPhrases)
                            {
                                pendingPhrasesDB.Add(code);
                            }
                            else if(code == acceptedPhrases)
                            {
                                acceptedPhrasesDB.Add(code);
                            }
                            else
                            {
                                rejectedPhrasesDB.Add(code);
                            }
                        }
                        phraseSummary.PendingPhrases = $"[{pendingPhrasesDB.Count}]";
                        phraseSummary.AcceptedPhrases = $"[{acceptedPhrasesDB.Count}]";
                        phraseSummary.SubmittedPhrases = $"[{allStatusCodes.Count}]";
                    }
                }
                using (NpgsqlCommand command = new NpgsqlCommand("select balance, totalearned from usersmoneybalances where spiceuserid IN (select spice_user_id from spice_users where spice_user_name = @userName)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader SpiceTextReader = command.ExecuteReader())
                    {
                        if (SpiceTextReader.Read())
                        {
                            phraseSummary.Balance = $"[${Convert.ToDecimal(SpiceTextReader["balance"])}]";
                            phraseSummary.TotalEarned = $"[${Convert.ToDecimal(SpiceTextReader["totalearned"])}]";
                        }
                    }
                }
            }
            return phraseSummary;
        }
        public List<string> GetPendingPhrasesFromDB(string username, int unassignedPendingPhrases, int assignedPendingPhrases)
        {
            List<string> pendingPhrases = new List<string>();
            List<int> phraseIDs = new List<int>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select sub_phrase_id from users_submitted_phrases where spice_user_id IN (select spice_user_id from spice_users where spice_user_name = @userName) and phrase_status IN (@unassignedStatusCode,@assignedStatusCode)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);
                    NpgsqlParameter UnassignedStatusCode = new NpgsqlParameter
                    {
                        ParameterName = "@unassignedStatusCode",
                        Value = unassignedPendingPhrases

                    };
                    command.Parameters.Add(UnassignedStatusCode);
                    NpgsqlParameter AssignedStatusCode = new NpgsqlParameter
                    {
                        ParameterName = "@assignedStatusCode",
                        Value = assignedPendingPhrases

                    };
                    command.Parameters.Add(AssignedStatusCode);

                    using (NpgsqlDataReader PhraseIDReader = command.ExecuteReader())
                    {
                        while (PhraseIDReader.Read())
                        {
                            phraseIDs.Add((int)PhraseIDReader["sub_phrase_id"]);
                        }
                    }
                }
                string pendingPhraseIDs = string.Join(",", phraseIDs);
               
                using (NpgsqlCommand command = new NpgsqlCommand("select phrase from submitted_phrases where phrase_id IN ("+pendingPhraseIDs+") ORDER BY phrase_id DESC", connection))
                {  
                    using (NpgsqlDataReader PhraseTextReader = command.ExecuteReader())
                    {
                        while (PhraseTextReader.Read())
                        {
                            pendingPhrases.Add((string)PhraseTextReader["phrase"]);
                        }
                    }
                }
            }
            return pendingPhrases;
        }
        public List<string> GetAcceptedPhrasesFromDB(string username, int acceptedPhrasesStatusCode)
        {
            List<string> acceptedPhrases = new List<string>();
            List<int> phraseIDs = new List<int>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select sub_phrase_id from users_submitted_phrases where spice_user_id IN (select spice_user_id from spice_users where spice_user_name = @userName) and phrase_status = @acceptedPhrasesStatusCode", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);
                    NpgsqlParameter AcceptedStatusCode = new NpgsqlParameter
                    {
                        ParameterName = "@acceptedPhrasesStatusCode",
                        Value = acceptedPhrasesStatusCode

                    };
                    command.Parameters.Add(AcceptedStatusCode);

                    using (NpgsqlDataReader PhraseIDReader = command.ExecuteReader())
                    {
                        while (PhraseIDReader.Read())
                        {
                            phraseIDs.Add((int)PhraseIDReader["sub_phrase_id"]);
                        }
                    }
                }
                string acceptedPhraseIDs = string.Join(",", phraseIDs);

                using (NpgsqlCommand command = new NpgsqlCommand("select phrase from submitted_phrases where phrase_id IN (" + acceptedPhraseIDs + ") ORDER BY phrase_id DESC", connection))
                {
                    using (NpgsqlDataReader PhraseTextReader = command.ExecuteReader())
                    {
                        while (PhraseTextReader.Read())
                        {
                            acceptedPhrases.Add((string)PhraseTextReader["phrase"]);
                        }
                    }
                }
            }
            return acceptedPhrases;
        }
        public string GetUploadedPhotoInfoFromDB(string username)
        {
            string userImageStatus = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select profileimage from spice_users where spice_user_name = @userName", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader SpiceTextReader = command.ExecuteReader())
                    {
                        if (SpiceTextReader.Read())
                        {
                            userImageStatus = Convert.ToString(SpiceTextReader["profileimage"]);
                        }
                    }
                }
            }
            return userImageStatus;
        }
        public void CreateSubscriptionForUser(string username, int randomProductCode)
        {
            float spiceUserID = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select spice_user_id from spice_users where spice_user_name = @userName", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader SpiceIDReader = command.ExecuteReader())
                    {
                        if (SpiceIDReader.Read())
                        {
                            spiceUserID = (Int64)SpiceIDReader["spice_user_id"];
                        }
                    }
                }
                using (NpgsqlCommand Command = new NpgsqlCommand("SELECT p_id FROM QSP_CreateSubscription(CAST(@SpiceUserID AS INTEGER),CAST(@DateTime AS TIMESTAMP),@ProductID,@PayPalRecieptID,@InVoiceNumber,@TransctionTypeID)", connection))
                {

                    NpgsqlParameter userID = new NpgsqlParameter
                    {
                        ParameterName = "@SpiceUserID",
                        Value = spiceUserID
                    };
                    Command.Parameters.Add(userID);
                    NpgsqlParameter dateTime = new NpgsqlParameter
                    {
                        ParameterName = "@DateTime",
                        Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

                    };
                    Command.Parameters.Add(dateTime);
                    NpgsqlParameter ID = new NpgsqlParameter
                    {
                        ParameterName = "@ProductID",
                        Value = randomProductCode

                    };
                    Command.Parameters.Add(ID);
                    NpgsqlParameter PayPalID = new NpgsqlParameter
                    {
                        ParameterName = "@PayPalRecieptID",
                        Value = 1977
                    };
                    Command.Parameters.Add(PayPalID);
                    NpgsqlParameter inVoiceNumber = new NpgsqlParameter
                    {
                        ParameterName = "@InVoiceNumber",
                        Value = "ShanonaWriter_2015_8_21_20_24_33_422"
                    };
                    Command.Parameters.Add(inVoiceNumber);
                    NpgsqlParameter TransctionID = new NpgsqlParameter
                    {
                        ParameterName = "@TransctionTypeID",
                        Value = 17
                    };
                    Command.Parameters.Add(TransctionID);
                    Command.ExecuteNonQuery();
                }
            }
        }
        public SubscriptionTypeAndDuration GetSubscriptionDetails(string username)
        {
            SubscriptionTypeAndDuration subscriptionDetails = new SubscriptionTypeAndDuration();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select expiryon,clientproductid from subscriptions where spice_user_id IN (select spice_user_id from spice_users where spice_user_name = @userName)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader SpiceTextReader = command.ExecuteReader())
                    {
                        if (SpiceTextReader.Read())
                        {
                            DateTime expiryOn_utc = (DateTime)SpiceTextReader["expiryon"];
                            int productID = (int)SpiceTextReader["clientproductid"];

                            DateTime expiryOn_ist = TimeZoneInfo.ConvertTimeFromUtc(expiryOn_utc,TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                            subscriptionDetails.SubscriptionDurationFromDB = string.Format("{0:M/d/yyyy}", expiryOn_ist);
                            if (productID == 1)
                            {
                                subscriptionDetails.SubscriptionTypeFromDB = "Spice Mobile";
                            }
                            else
                            {
                                subscriptionDetails.SubscriptionTypeFromDB = "Spice Professional";
                            }
                        }
                    }
                }
            }
            return subscriptionDetails;
        }
        public void DeleteSubscriptionForUser(string username)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("delete from subscriptions where spice_user_id IN (select spice_user_id from spice_users where spice_user_name = @userName)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);
                    command.ExecuteNonQuery();
                }
            }
        }
        public string GetRandomStringOfRequiredLength(int length)
        {
            StringBuilder randomString = new StringBuilder();
            char alphabet;
            for (int i = 0; i < length; i++)
            {
                alphabet = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65)));
                randomString.Append(alphabet);
            }
            return randomString.ToString().ToLower();
        }
        public void AddRandomDevicesToSubscription(string username, string hashedPassword)
        {
            
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                int productID = 0;
                int subscriptionID = 0;
                int addDevices = 0;
                string expiryOn = null;
                bool isFreeTrail = false;
                string activationCertificate = null;
                string deviceType = null;

                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select clientproductid,id,expiryon,isappleinappfreetrial from subscriptions where spice_user_id IN (select spice_user_id from spice_users where spice_user_name = @userName)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader SpiceTextReader = command.ExecuteReader())
                    {
                        if (SpiceTextReader.Read())
                        {
                            productID = (int)SpiceTextReader["clientproductid"];
                            subscriptionID = Convert.ToInt32(SpiceTextReader["id"]);
                            expiryOn = Convert.ToString(SpiceTextReader["expiryon"]);
                            isFreeTrail = (bool)SpiceTextReader["isappleinappfreetrial"];
                        }
                    }
                }
                
                if (productID == 1)
                {
                    addDevices = _random.Next(2, 3);
                    deviceType = "testmobile";
                }
                else
                {
                    addDevices = _random.Next(2, 6);
                    deviceType = "testprofessional";
                }

                for(int i =1; i<= addDevices; i++)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT p_ActCertValue FROM QSP_GetRandomValueForActivationCertificate(CAST(@ExpiryOn AS TIMESTAMP), @ProdID, true);", connection))
                    {
                        NpgsqlParameter DateTime = new NpgsqlParameter
                        {
                            ParameterName = "@ExpiryOn",
                            Value = expiryOn

                        };
                        command.Parameters.Add(DateTime);
                        NpgsqlParameter ProductID = new NpgsqlParameter
                        {
                            ParameterName = "@ProdID",
                            Value = productID

                        };
                        command.Parameters.Add(ProductID);
                        NpgsqlParameter IsFreeTrail = new NpgsqlParameter
                        {
                            ParameterName = "@IsFreeTrail",
                            Value = isFreeTrail

                        };
                        command.Parameters.Add(IsFreeTrail);

                        using (NpgsqlDataReader SpiceTextReader = command.ExecuteReader())
                        {
                            if (SpiceTextReader.Read())
                            {
                                activationCertificate = (string)SpiceTextReader["p_ActCertValue"];
                            }
                        }
                    }

                    string deviceID = $"{GetRandomStringOfRequiredLength(4)}-{GetRandomStringOfRequiredLength(4)}-{GetRandomStringOfRequiredLength(4)}-{GetRandomStringOfRequiredLength(4)}";

                    using (NpgsqlCommand command = new NpgsqlCommand("insert into subscription_devices (subscriptionid,accesscode,activationcertificate,deviceid,devicetype,isdeviceactive) values(@subscriptionID,@accessCode,@ActivationCertificate,@DeviceID,@DeviceType,@IsDeviceActive)", connection))
                    {
                        NpgsqlParameter ID = new NpgsqlParameter
                        {
                            ParameterName = "@subscriptionID",
                            Value = subscriptionID

                        };
                        command.Parameters.Add(ID);
                        NpgsqlParameter code = new NpgsqlParameter
                        {
                            ParameterName = "@accessCode",
                            Value = DBNull.Value

                        };
                        command.Parameters.Add(code);
                        NpgsqlParameter certificate = new NpgsqlParameter
                        {
                            ParameterName = "@ActivationCertificate",
                            Value = activationCertificate

                        };
                        command.Parameters.Add(certificate);
                        NpgsqlParameter DeviceID = new NpgsqlParameter
                        {
                            ParameterName = "@DeviceID",
                            Value = deviceID

                        };
                        command.Parameters.Add(DeviceID);
                        NpgsqlParameter DeviceType = new NpgsqlParameter
                        {
                            ParameterName = "@DeviceType",
                            Value = deviceType

                        };
                        command.Parameters.Add(DeviceType);
                        NpgsqlParameter IsActive = new NpgsqlParameter
                        {
                            ParameterName = "@IsDeviceActive",
                            Value = true

                        };
                        command.Parameters.Add(IsActive);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        public List<string> GetSubscriptionDevices(string username)
        {
            List<string> devicesList = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select devicetype from subscription_devices AS D INNER JOIN subscriptions AS S ON D.subscriptionid = S.id INNER JOIN spice_users AS SU ON SU.spice_user_id = S.spice_user_id where SU.spice_user_name = @userName", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader deviceReader = command.ExecuteReader())
                    {
                        while (deviceReader.Read())
                        {
                            devicesList.Add($"Remove {(string)deviceReader["devicetype"]}");
                        }
                    }
                }
            }
            return devicesList;
        }
        public void DeleteDevicesForSubscription(string username)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("delete from subscription_devices where devicetype IN (select devicetype from subscription_devices AS D INNER JOIN subscriptions AS S ON D.subscriptionid = S.id INNER JOIN spice_users AS SU ON SU.spice_user_id = S.spice_user_id where SU.spice_user_name = @userName)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);
                    command.ExecuteNonQuery();
                }
            }
        }
        public AccountDetailsDB GetAccountDetails(string username)
        {
            AccountDetailsDB accountDetails = new AccountDetailsDB();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select first_name,last_name,email,phone from spice_users where spice_user_name = @userName", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader SpiceTextReader = command.ExecuteReader())
                    {
                        if (SpiceTextReader.Read())
                        {
                            accountDetails.FirstName = (string)SpiceTextReader["first_name"];
                            accountDetails.LastName = (string)SpiceTextReader["last_name"];
                            accountDetails.Email = (string)SpiceTextReader["email"];
                            accountDetails.Phone = (string)SpiceTextReader["phone"];
                        }
                    }
                }
            }
            return accountDetails;
        }
        public void UpdateAccountDetails(string username, string firstName, string lastName, string email, string phone)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("update spice_users set first_name = @firstName, last_name = @lastName, email = @email, phone = @phone where spice_user_name = @userName", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);
                    NpgsqlParameter FirstName = new NpgsqlParameter
                    {
                        ParameterName = "@firstName",
                        Value = firstName

                    };
                    command.Parameters.Add(FirstName);
                    NpgsqlParameter LastName = new NpgsqlParameter
                    {
                        ParameterName = "@lastName",
                        Value = username

                    };
                    command.Parameters.Add(LastName);
                    NpgsqlParameter Email = new NpgsqlParameter
                    {
                        ParameterName = "@email",
                        Value = email

                    };
                    command.Parameters.Add(Email);
                    NpgsqlParameter Phone = new NpgsqlParameter
                    {
                        ParameterName = "@phone",
                        Value = phone

                    };
                    command.Parameters.Add(Phone);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeletePaymentRequests(string username)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("delete from userrequestedphrases where paymentrequestid IN (select id from userpaymentrequests AS U INNER JOIN spice_users AS S ON S.spice_user_id = U.spiceuserid where S.spice_user_name = @userName)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);
                    command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand("delete from checkpaymentrequests_address where paymentrequestid IN (select id from userpaymentrequests AS U INNER JOIN spice_users AS S ON S.spice_user_id = U.spiceuserid where S.spice_user_name = @userName)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);
                    command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand("delete from userpaymentrequests where spiceuserid in (select spice_user_id from spice_users where spice_user_name = @userName)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);
                    command.ExecuteNonQuery();
                }
            }
        }
        public string GetPayPalEmailFromPaymentRequest(string username)
        {
            string paypalEmailID = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select paypalemailid from userpaymentrequests where spiceuserid in (select spice_user_id from spice_users where spice_user_name = @userName)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader SpiceTextReader = command.ExecuteReader())
                    {
                        if (SpiceTextReader.Read())
                        {
                            paypalEmailID = Convert.ToString(SpiceTextReader["paypalemailid"]);
                        }
                    }
                }
            }
            return paypalEmailID;
        }
        public List<string> GetSourceDetailsForNewlySubmittedPhrase(string username)
        {
            List<string> sourceInfo = new List<string>();
            int phraseID = 0; 

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select sub_phrase_id from users_submitted_phrases where spice_user_id IN (select spice_user_id from spice_users where spice_user_name = @userName) order by sub_phrase_id desc limit 1", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    using (NpgsqlDataReader phraseIdReader = command.ExecuteReader())
                    {
                        if (phraseIdReader.Read())
                        {
                            phraseID = Convert.ToInt32(phraseIdReader["sub_phrase_id"]);
                        }
                    }
                }
                using (NpgsqlCommand command = new NpgsqlCommand("select * from qsp_get_submitted_phrase_details (@phraseID)", connection))
                {
                    NpgsqlParameter ID = new NpgsqlParameter
                    {
                        ParameterName = "@phraseID",
                        Value = phraseID

                    };
                    command.Parameters.Add(ID);

                    using (NpgsqlDataReader sourceInfoReader = command.ExecuteReader())
                    {
                        XmlDocument xmldoc = new XmlDocument();

                        if (sourceInfoReader.Read())
                        {
                            sourceInfo.Add(Convert.ToString(sourceInfoReader["title"]));
                            sourceInfo.Add(Convert.ToString(sourceInfoReader["referenceurl"]));
                            sourceInfo.Add(Convert.ToString(sourceInfoReader["year"]));

                            string authorDataInfoXML = Convert.ToString(sourceInfoReader["authors"]);
                            xmldoc.LoadXml(authorDataInfoXML);
                            foreach (XmlNode authorNode in xmldoc.SelectNodes("//Authors//Author//Author"))
                            {
                                string author = authorNode.InnerText.ToString();
                                if (author != null && author != "Unknown")
                                    sourceInfo.Add(author);
                            }


                            string actorsDataInfoXML = Convert.ToString(sourceInfoReader["actors"]);
                            xmldoc.LoadXml(actorsDataInfoXML);
                            foreach (XmlNode actorNode in xmldoc.SelectNodes("//Actors//Actor//Actor"))
                            {
                                string actor = actorNode.InnerText.ToString();
                                if (actor != null && actor != "Unknown")
                                    sourceInfo.Add(actor);
                            }

                            string otherTitle = Convert.ToString(sourceInfoReader["other_title"]);
                            if (otherTitle != "")
                                sourceInfo.Add(otherTitle);


                            string director = Convert.ToString(sourceInfoReader["director"]);
                            if (director != "")
                                sourceInfo.Add(director);


                            string publisher = Convert.ToString(sourceInfoReader["publisher"]);
                            if (publisher != "" && publisher != "unknown")
                                sourceInfo.Add(publisher);


                            string editorsDataInfoXML = Convert.ToString(sourceInfoReader["editors"]);
                            xmldoc.LoadXml(editorsDataInfoXML);
                            foreach (XmlNode editorNode in xmldoc.SelectNodes("//Editors//Editor//Editor"))
                            {
                                string editor = editorNode.InnerText.ToString();
                                if (editor != null && editor != "Unknown")
                                    sourceInfo.Add(editor);
                            }


                            try
                            {
                                DateTime dateTime = Convert.ToDateTime(sourceInfoReader["date"]);
                                string date = dateTime.ToString("MM/dd/yyyy");
                                if (date != "")
                                    sourceInfo.Add(date);
                            }
                            catch (Exception)
                            {

                            }

                            string volume = Convert.ToString(sourceInfoReader["volume"]);
                            if (volume != "")
                                sourceInfo.Add(volume);
                            

                            string city = Convert.ToString(sourceInfoReader["city"]);
                            if (city != "")
                                sourceInfo.Add(city);

                            string isbn = Convert.ToString(sourceInfoReader["isbn"]);
                            if (isbn != "")
                                sourceInfo.Add(isbn);

                        }
                    }
                }
            }
            return sourceInfo;
        }
        public void DeleteNewlySubmittedPhrase(string username)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("delete from users_submitted_phrases where sub_phrase_id IN (select sub_phrase_id from users_submitted_phrases where spice_user_id IN (select spice_user_id from spice_users where spice_user_name = @userName) order by sub_phrase_id desc limit 1)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<string> GetPhrasesListForSubmittedPhrasesCount(string username,int phrasesCount)
        {
            List<string> phrasesList = new List<string>();
            List<int> phraseIDs = new List<int>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select sub_phrase_id from users_submitted_phrases where spice_user_id IN (select spice_user_id from spice_users where spice_user_name = @userName) order by sub_phrase_id desc limit @phraseCount", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    NpgsqlParameter Count = new NpgsqlParameter
                    {
                        ParameterName = "@phraseCount",
                        Value = phrasesCount

                    };
                    command.Parameters.Add(Count);

                    using (NpgsqlDataReader phraseIdReader = command.ExecuteReader())
                    {
                        while (phraseIdReader.Read())
                        {
                            phraseIDs.Add(Convert.ToInt32(phraseIdReader["sub_phrase_id"]));
                        }
                    }
                }
                foreach(int phraseID in phraseIDs)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("select phrase from qsp_get_submitted_phrase_details (@phraseID)", connection))
                    {
                        NpgsqlParameter ID = new NpgsqlParameter
                        {
                            ParameterName = "@phraseID",
                            Value = phraseID

                        };
                        command.Parameters.Add(ID);

                        using (NpgsqlDataReader phraseReader = command.ExecuteReader())
                        {
                            if (phraseReader.Read())
                            {
                                phrasesList.Add(Convert.ToString(phraseReader["phrase"]));
                            }
                        }
                    }
                }
                
            }
            return phrasesList;
        }
        public void DeleteMultiplePhrasesSubmitted(string username,int phrasesCount)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("delete from users_submitted_phrases where sub_phrase_id IN (select sub_phrase_id from users_submitted_phrases where spice_user_id IN (select spice_user_id from spice_users where spice_user_name = @userName) order by sub_phrase_id desc limit @phraseCount)", connection))
                {
                    NpgsqlParameter Name = new NpgsqlParameter
                    {
                        ParameterName = "@userName",
                        Value = username

                    };
                    command.Parameters.Add(Name);

                    NpgsqlParameter Count = new NpgsqlParameter
                    {
                        ParameterName = "@phraseCount",
                        Value = phrasesCount

                    };
                    command.Parameters.Add(Count);
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<string> GetRecentlySubmittedContactUsInfo()
        {
            List<string> contactInfo = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                
                using (NpgsqlCommand command = new NpgsqlCommand("select * from spicecontacts order by contactid desc limit 1", connection))
                {
                    using (NpgsqlDataReader contactInfoReader = command.ExecuteReader())
                    {

                        if (contactInfoReader.Read())
                        {
                            string phone = Convert.ToString(contactInfoReader["phone"]);

                            if(phone != "")
                            {
                                contactInfo.Add(phone);
                            }
                            else
                            {
                                contactInfo.Add(Convert.ToString(contactInfoReader["username"]));
                            }
                            contactInfo.Add(Convert.ToString(contactInfoReader["firstname"]));
                            contactInfo.Add(Convert.ToString(contactInfoReader["lastname"]));
                            contactInfo.Add(Convert.ToString(contactInfoReader["helptopicid"]));
                            contactInfo.Add(Convert.ToString(contactInfoReader["content"]));
                        }
                    }
                }
            }
            return contactInfo;
        }
        public void DeleteNewlySubmittedContactUsEntry()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("delete from spicecontacts where contactid IN (select contactid from spicecontacts order by contactid desc limit 1)", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<string> GetRecentlySubmittedThoughtsInfo()
        {
            List<string> thoughts = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("select * from thoughts order by thoughtid desc limit 1", connection))
                {
                    using (NpgsqlDataReader thoughtInfoReader = command.ExecuteReader())
                    {

                        if (thoughtInfoReader.Read())
                        {
                            thoughts.Add(Convert.ToString(thoughtInfoReader["isvisitedbefore"]));
                            thoughts.Add(Convert.ToString(thoughtInfoReader["newsletter"]));
                            thoughts.Add(Convert.ToString(thoughtInfoReader["rating"]));
                            thoughts.Add(Convert.ToString(thoughtInfoReader["emailid"]));
                        }
                    }
                }
            }
            return thoughts;
        }
        public void DeleteNewlySubmittedThoughtsEntry()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("delete from thoughts where thoughtid IN (select thoughtid from thoughts order by thoughtid desc limit 1)", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        public string GetCurrentPhraseRate()
        {
            string phraseRate = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_spiceCustomersConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select current_price from phrases_currentprice order by current_price_id desc limit 1", connection))
                {
                    using (NpgsqlDataReader priceReader = command.ExecuteReader())
                    {
                        if (priceReader.Read())
                        {
                            phraseRate = Convert.ToString(priceReader["current_price"]);
                        }
                    }
                }
            }
            return phraseRate;
        }
    }
}
