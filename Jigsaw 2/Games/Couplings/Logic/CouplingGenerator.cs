using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Jigsaw_2.Games.Couplings
{
    internal class CouplingGenerator : Generator
    {
        #region Private Fields

        private Dictionary<string, string> couplings;
        private string couplingText;

        #endregion Private Fields

        #region Constructors

        public CouplingGenerator()
        {
            couplings = new Dictionary<string, string>();

            setCouplings();

            foreach (KeyValuePair<string, string> kvp in couplings)
            {
                Console.WriteLine(kvp.ToString());
            }
        }

        #endregion Constructors

        #region Public Methods

        public Dictionary<string, string> GetCouplings()
        {
            return couplings;
        }

        public string GetCouplingText()
        {
            return couplingText;
        }

        #endregion Public Methods

        private void setCouplings()
        {
            string query = "SELECT cc1.Couple1, cc1.Couple2, cc1.Couple3, cc1.Couple4, cc1.Couple5, cc1.Couple6, cc1.Couple7, cc1.Couple8, cc2.Couple1, cc2.Couple2, cc2.Couple3, cc2.Couple4, cc2.Couple5, cc2.Couple6, cc2.Couple7, cc2.Couple8 FROM Couplings c INNER JOIN CouplesColumn1 cc1 ON c.CoupleColumn1Id = cc1.Id INNER JOIN CouplesColumn2 cc2 ON c.CoupleColumn2Id = cc2.Id;";

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();

                int couplingsIndex = new Random().Next(adapter.Fill(dataTable));

                setCouplingsText(couplingsIndex);

                DataRow dataRow = dataTable.Select()[couplingsIndex];

                int rowHalfLength = dataRow.ItemArray.Length / 2;

                for (int i = 0; i < rowHalfLength; i++)
                {
                    couplings.Add(dataRow[i] as string, dataRow[i + rowHalfLength] as string);
                }
            }
        }

        private void setCouplingsText(int couplingsIndex)
        {
            string query = "SELECT CoupleText FROM Couplings;";

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                couplingText = dataTable.Select()[couplingsIndex][0] as string;
            }
        }
    }
}