using Jigsaw_2.Abstracts;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Jigsaw_2.Games.LetterOnLetter
{
    public sealed class WordList : Generator
    {
        #region Public Methods

        public string GetWoWSeed()
        {
            //string query = "SELECT word FROM Words WHERE LEN(word) = 12"; //this would be a by the book way to write the query.
            string query = "SELECT word FROM TwelveLetterWords"; //but this query should be faster idk.

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();

                int dataLength = adapter.Fill(dataTable);

                return dataTable.Select()[new Random().Next(dataLength)][0].ToString().ToUpper();
            }
        }

        public bool Check(string s)
        {
            string query = "SELECT word FROM Words WHERE word = " + "'" + s.ToLower() + "'"; //looks a bit weird but it works.

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();

                return adapter.Fill(dataTable) != 0;
            }
        }

        #endregion Public Methods
    }
}